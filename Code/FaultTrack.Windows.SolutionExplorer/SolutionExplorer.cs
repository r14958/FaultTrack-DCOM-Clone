namespace FaultTrack.Windows.SolutionExplorer
{
    using FaultTrack.Shell;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class SolutionExplorer : WorkspaceViewModel
    {
        private readonly IShell shell;
        private readonly ICollection<Project> projects;
        private bool isBusy;
        private string searchPath;
        private const string LoadDirectory = @"F:\";

        public SolutionExplorer(IShell shell)
        {
            this.shell = shell;

            RefreshCommand = new TaskCommand(p => GetSolutions());
            projects       = new ObservableCollection<Project>();
            SearchPath     = @"";
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        public string SearchPath
        {
            get
            {
                return searchPath;
            }
            set
            {
                searchPath = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<Project> Projects
        {
            get
            {
                return projects;
            }
        }

        private async Task GetSolutions()
        {
            IsBusy = true;

            this.projects.Clear();

            IEnumerable<Solution> solutions;

            try
            {
                solutions = await GetSolutionsAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }

            foreach (var group in solutions.GroupBy(p => p.GroupName))
            {
                var project = new Project
                              {
                                  Baseline = group.FirstOrDefault(p => p.Name == "Baseline"),
                                  Branches = group.Where(p => p.Name != "Baseline").OrderBy(p => p.Name).ToList(),
                                  Name     = group.Key
                              };

                projects.Add(project);
            }
        }

        class SafeDirectory
        {
            public static IEnumerable<String> EnumerateFiles(string path, string searchPattern, SearchOption option)
            {
                try
                {
                    var files = Enumerable.Empty<String>();

                    if (option == SearchOption.AllDirectories)
                    {
                        files = Directory.EnumerateDirectories(path).SelectMany(p => EnumerateFiles(p, searchPattern, option));
                    }

                    return files.Concat(Directory.EnumerateFiles(path, searchPattern));
                }
                catch (UnauthorizedAccessException)
                {
                    return Enumerable.Empty<String>();
                }
            }
        }

        private async Task<IEnumerable<Solution>> GetSolutionsAsync()
        {
            return await Task.Run(() =>
            {
                var strategies = new[] {
                    new SolutionStrategy(@"[A-Z]:\\([\w\-.() ]*\\)*(Development|Branches)\\([\w\-.() ]*\\)Code\\[\w\-.() ]*\.sln", 4, 2),
                    new SolutionStrategy(@"[A-Z]:\\([\w\-.() ]*\\)*(Main|Trunk)\\Code\\[\w\-.() ]*\.sln", 3),


                    new SolutionStrategy(@"[A-Z]:\\([\w\-.() ]*\\)*(Development|Branches)\\([\w\-.() ]*\\)[\w\-.() ]*\.sln", 3, 1),
                    new SolutionStrategy(@"[A-Z]:\\([\w\-.() ]*\\)*(Main|Trunk)\\[\w\-.() ]*\.sln", 2),
                };

                var files = SafeDirectory.EnumerateFiles(LoadDirectory, "*.sln", SearchOption.AllDirectories)
                                         .Select(p => new FileInfo(p))
                                         .Where(p => !p.FullName.Contains("Tags"));

                var solutions = new List<Solution>();

                foreach (var file in files)
                {
                    try
                    {
                        foreach (var strategy in strategies)
                        {
                            if (strategy.IsMatch(file))
                            {
                                solutions.Add(new Solution(file.FullName)
                                {
                                    GroupName = strategy.GetGroupName(file),
                                    Name = strategy.GetName(file)
                                });

                                break;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                return solutions;
            });
        }
    }
}