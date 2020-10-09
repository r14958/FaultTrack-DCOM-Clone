namespace FaultTrack.Shell
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Web;

    public interface IShell
    {
        event EventHandler CloseRequested;
        event EventHandler Loaded;
        event EventHandler PrincipalChanged;
        IPrincipal Principal { get; }
        IEnumerable<IPackage> Packages { get; }
        IProjectCollections ProjectCollections { get; }
        IEnumerable<Object> Services { get; }
        IDictionary<String, IUIPackage> ExtensionPoints { get; }
        ISettings Settings { get; }
        ITabs Tabs { get; }
        T GetService<T>();
        void Quit();
        void RegisterService<TServiceContract>(TServiceContract serviceImplementation);
        Task SignInAsync(Uri account, string username, string password);
        Task SignOutAsync();
    }
}