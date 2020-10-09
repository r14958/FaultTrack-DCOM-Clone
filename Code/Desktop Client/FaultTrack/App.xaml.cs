namespace FaultTrack.Windows
{
    using System;
    using System.Windows;
    using Services;
    using Shell;
    using ViewModels;
    using Views;

    public partial class App
    {
        private readonly IShell shell;

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            shell = ShellFactory.GetInstance();
            shell.CloseRequested += OnShellCloseRequested;
            shell.Loaded += OnShellLoaded;
        }

        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            var window = new ShellWindow
                         {
                             DataContext = new ShellViewModel(shell)
                         };

            window.ShowDialog();
        }

        private void OnShellCloseRequested(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void OnShellLoaded(object sender, EventArgs e)
        {
            shell.RegisterService<IDialogService>(new DialogService(App.Current.MainWindow));
            shell.RegisterService<IErrorService>(new ErrorService());
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            IErrorService service = shell.GetService<IErrorService>();

            service.Handle(e.ExceptionObject as Exception);
        }
    }
}