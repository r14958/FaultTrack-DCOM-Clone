namespace FaultTrack.Windows.SolutionExplorer
{
    using FaultTrack.Shell;
    using FaultTrack.Windows;
    using FaultTrack.Windows.SolutionExplorer.Views;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;
    using System.Windows;

    [Export(typeof(IPackage))]
    public class Package : IUIPackage
    {
        private readonly SolutionExplorer viewModel;

        [ImportingConstructor]
        public Package([Import(typeof(IShell))] IShell shell)
        {
            Shell = shell;
            viewModel = new SolutionExplorer(Shell);
        }
        
        public IShell Shell
        {
            get;
        }

        public string ExtensionPoint
        {
            get
            {
                return ExtensionPoints.SolutionExplorer;
            }
        }

        public dynamic DataContext
        {
            get
            {
                return viewModel;
            }
        }

        public dynamic Template
        {
            get
            {
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof (SolutionExplorerView));
                
                return new DataTemplate
                       {
                           VisualTree = factory
                       };
            }
        }

        public async Task LoadAsync()
        {
            await ((TaskCommand)viewModel.RefreshCommand).ExecuteAsync();
        }
    }
}