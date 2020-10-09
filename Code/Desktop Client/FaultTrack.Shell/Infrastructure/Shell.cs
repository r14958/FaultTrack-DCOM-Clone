namespace FaultTrack.Shell.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Web;

    [Export(typeof(IShell))]
    internal sealed class Shell : IShell, INotifyPropertyChanged
    {
        [ImportMany(typeof(IPackage))]
        private readonly ICollection<IPackage> packages;
        private readonly IDictionary<String, IUIPackage> extensionPoints;
        private readonly ICollection<Object> services;
        private IPrincipal principal;
        
        public event EventHandler CloseRequested;
        public event EventHandler Loaded;
        public event EventHandler Unloaded;
        public event EventHandler PrincipalChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            extensionPoints    = new Dictionary<String, IUIPackage>();
            packages           = new Collection<IPackage>();
            services           = new Collection<Object>();
            ProjectCollections = new ProjectCollections();
            Settings           = new Settings();
            Tabs               = new Tabs();
        }

        public IEnumerable<IPackage> Packages
        {
            get
            {
                return packages;
            }
        }

        public IPrincipal Principal
        {
            get
            {
                return principal;
            }
            private set
            {
                principal = value;
                NotifyPropertyChanged();
            }
        }

        public IProjectCollections ProjectCollections
        {
            get; 
        }

        public IEnumerable<Object> Services
        {
            get
            {
                return services;
            }
        }

        public ISettings Settings
        {
            get;
        }

        public ITabs Tabs
        {
            get;
        }

        public IDictionary<String, IUIPackage> ExtensionPoints
        {
            get
            {
                return extensionPoints;
            }
        } 

        public T GetService<T>()
        {
            return services.OfType<T>().SingleOrDefault();
        }

        private async Task LoadPackagesAsync()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            string packagesDirectory = GetPackagesDirectory();

            catalog.Catalogs.Add(new DirectoryCatalog(packagesDirectory));

            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            foreach (var package in Packages)
            {
                if (package is IUIPackage)
                {
                    var ui = (IUIPackage)package;

                    switch (ui.ExtensionPoint)
                    {
                        case FaultTrack.Shell.ExtensionPoints.ProjectExplorer:
                            extensionPoints[FaultTrack.Shell.ExtensionPoints.ProjectExplorer] = ui;
                            break;
                        case FaultTrack.Shell.ExtensionPoints.SolutionExplorer:
                            extensionPoints[FaultTrack.Shell.ExtensionPoints.SolutionExplorer] = ui;
                            break;
                    }

                    //await ui.LoadAsync();
                }
            }
        }

        public void Quit()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        public void RegisterService<TServiceContract>(TServiceContract serviceImplementation)
        {
            if (this.services.Any(service => service is TServiceContract))
            {
                throw new NotSupportedException();
            }

            services.Add(serviceImplementation);
        }

        public async Task SignInAsync(Uri account, string username, string password)
        {
            using (PrincipalService service = new PrincipalService(account))
            {
                AuthorizationToken token;

                if (!String.IsNullOrWhiteSpace(Settings.UserToken))
                {
                    token = new AuthorizationToken
                            {
                                UserName = username,
                                Token = Settings.UserToken
                            };
                }
                else
                {
                    var response = await service.SignInAsync(new SignInRequest
                                                             {
                                                                 Token = new AuthenticationToken(username, password)
                                                             });
                    token = response.Token;
                }

                try
                {
                    await service.AuthenticateAsync(new AuthenticateRequest
                                                    {
                                                        Token = token
                                                    });
                }
                catch
                {
                    ((Settings)Settings).UserToken = null; throw;
                }

                Principal = new WebPrincipal(account,
                                             token,
                                             new WebIdentity
                                             {
                                                 AuthenticationType = "Token",
                                                 IsAuthenticated = true,
                                                 Name = token.UserName
                                             });

                ((Settings)Settings).Account = account.ToString();
                ((Settings)Settings).UserName = token.UserName;
                ((Settings)Settings).UserToken = token.Token;

                OnPrincipalChanged();

                await LoadAsync();
            }
        }

        public async Task SignOutAsync()
        {
            IWebPrincipal principal = Principal as IWebPrincipal;

            if (principal == null)
            {
                throw new NotImplementedException();
            }

            using (PrincipalService service = new PrincipalService(principal.Account))
            {
                await service.SignOutAsync(new SignOutRequest
                                           {
                                               Token = principal.Token
                                           });
            }

            ((Settings)Settings).UserToken = null;

            Principal = null;

            OnPrincipalChanged();

            Unload();
        }

        private string GetPackagesDirectory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string directory = Path.GetDirectoryName(assembly.Location);

            return Path.Combine(directory, @"Packages");
        }

        private async Task LoadAsync()
        {
            RegisterExtensionPoints();
            RegisterServices();

            await LoadPackagesAsync();
            
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        private void Unload()
        {
            extensionPoints.Clear();
            packages.Clear();
            services.Clear();
            Unloaded?.Invoke(this, EventArgs.Empty);
        }

        private void RegisterExtensionPoints()
        {
            extensionPoints.Add(FaultTrack.Shell.ExtensionPoints.ProjectExplorer, null);
            extensionPoints.Add(FaultTrack.Shell.ExtensionPoints.SolutionExplorer, null);
        }

        private void RegisterServices()
        {
            IWebPrincipal principal = (IWebPrincipal)Principal;

            RegisterService<IExceptionService>(new ExceptionService());
            RegisterService(new ProjectService(principal.Account));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPrincipalChanged()
        {
            PrincipalChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}