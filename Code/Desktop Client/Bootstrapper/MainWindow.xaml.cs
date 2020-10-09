namespace Bootstrapper
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            viewModel.RequestClose += OnCloseRequested;
            viewModel.Load();
        }

        private void OnCloseRequested(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}