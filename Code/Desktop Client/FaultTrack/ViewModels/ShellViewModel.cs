namespace FaultTrack.Windows.ViewModels
{
    using System;
    using Shell;

    public class ShellViewModel : ViewModel
    {
        private readonly IShell shell;
        private string status;
        private object workspace;

        public ShellViewModel(IShell shell)
        {
            this.shell = shell;

            Menu = new ShellMenuViewModel(shell);

            Status = "Ready";
        }

        public ShellMenuViewModel Menu
        {
            get;
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }
    }
}