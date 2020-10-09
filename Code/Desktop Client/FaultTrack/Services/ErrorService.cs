namespace FaultTrack.Windows.Services
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using Shell;
    using ViewModels;
    using Views;

    public class ErrorService : IErrorService
    {
        public void Handle(Exception ex)
        {
            var view = new ErrorWindow
                       {
                           DataContext = new ErrorViewModel(ex),
                           Owner = Application.Current.MainWindow
                       };

            view.ShowDialog();

            var proc = Process.GetCurrentProcess();

            proc.Kill();
        }
    }
}