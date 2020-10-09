namespace FaultTrack.Windows.ViewModels
{
    using System;
    using System.Collections.Specialized;
    using Shell;

    public class MainViewModel : ViewModel
    {
        private readonly IShell shell;
        private dynamic projectExplorer;
        private dynamic solutionExplorer;

        public MainViewModel(IShell shell)
        {
            this.shell = shell;
            this.shell.Tabs.CollectionChanged += OnTabsCollectionChanged;
            this.shell.Loaded += OnShellLoaded; ;
        }

        public ITabs Tabs
        {
            get
            {
                return shell.Tabs;
            }
        }

        public dynamic ProjectExplorer
        {
            get => projectExplorer;
            private set
            {
                projectExplorer = value;
                NotifyPropertyChanged();
            }
        }

        public dynamic SolutionExplorer
        {
            get => solutionExplorer;
            private set
            {
                solutionExplorer = value;
                NotifyPropertyChanged();
            }
        }

        private void OnTabsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        IRequestClose closeRequest = (IRequestClose)e.NewItems[0];
                        closeRequest.CloseRequested += OnTabCloseRequested;
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        IRequestClose closeRequest = (IRequestClose)e.OldItems[0];
                        closeRequest.CloseRequested -= OnTabCloseRequested;
                        break;
                    }
            }
        }

        private void OnShellLoaded(object sender, EventArgs e)
        {
            ProjectExplorer = shell.ExtensionPoints[ExtensionPoints.ProjectExplorer];
            SolutionExplorer = shell.ExtensionPoints[ExtensionPoints.SolutionExplorer];

            ProjectExplorer.LoadAsync();
            SolutionExplorer.LoadAsync();
        }

        private void OnTabCloseRequested(object sender, EventArgs e)
        {
            Tabs.Remove(sender as ITab);
        }
    }
}