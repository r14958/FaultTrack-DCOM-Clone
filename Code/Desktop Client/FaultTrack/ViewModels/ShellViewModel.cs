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
            this.shell.PrincipalChanged += OnShellPrincipalChanged;

            Menu = new ShellMenuViewModel(shell);

            Status = "Ready";

            Workspace = new PrincipalViewModel(shell)
                        {
                            Account  = shell.Settings.Account,
                            UserName = shell.Settings.UserName,
                            Mode     = shell.Settings.Account != null ? PrincipalViewModel.EditMode.EditPrincipal : PrincipalViewModel.EditMode.EditAccount
                        };
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

        public object Workspace
        {
            get
            {
                return workspace;
            }
            set
            {
                workspace = value;
                NotifyPropertyChanged();
            }
        }

        private void OnShellPrincipalChanged(object sender, EventArgs e)
        {
            if (shell.Principal == null)
            {
                Workspace = new PrincipalViewModel(shell)
                            {
                                Account  = shell.Settings.Account,
                                UserName = shell.Settings.UserName,
                                Mode     = PrincipalViewModel.EditMode.EditPrincipal
                            };
            }
            else
            {
                Workspace = new MainViewModel(shell);
            }
        }
    }
}