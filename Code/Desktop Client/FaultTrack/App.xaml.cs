namespace FaultTrack.Windows
{
    using System.Windows;
    using ViewModels;
    using Views;

    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            var shell = new Shell();

            using (var shellStartup = shell.Start())
            {

            }
            var shell  = new Shell();

            window.ShowDialog();
        }
    }
}