namespace FaultTrack.Windows.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Shell;

    public class PrincipalViewModel : WorkspaceViewModel
    {
        private readonly IShell shell;
        private string account;
        private string error;
        private bool isBusy;
        private EditMode mode;
        private string username;
        private string userToken;
        private string password;

        public PrincipalViewModel(IShell shell)
        {
            this.shell = shell;
        }

        public string Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanAutoConnect
        {
            get
            {
                return shell.Settings.UserToken != null;
            }
        }

        public ICommand ChangeAccountCommand
        {
            get
            {
                return new ActionCommand(p => ChangeAccount());
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                return new ActionCommand(p => Connect(Account));
            }
        }

        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                NotifyPropertyChanged();
            }
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

        public EditMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return new TaskCommand(p => SignIn(Account, UserName, Password),
                                       p => CanSignIn());
            }
        }

        private bool CanSignIn()
        {
            return !String.IsNullOrWhiteSpace(Account) &&
                   !String.IsNullOrWhiteSpace(UserName) &&
                   !String.IsNullOrWhiteSpace(Password);
        }

        private void Connect(string account)
        {
            Mode = EditMode.EditPrincipal;
        }

        private async Task SignIn(string account, string userName, string password)
        {
            IsBusy = true;

            try
            {
                await shell.SignInAsync(new Uri($"{account}"), userName, password);

                if (Error != null)
                {
                    Error = null;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                
                // necessary to ensure focus works, because it won't work 
                // if the controls are still disabled based on this property being true
                IsBusy = false; 

                OnFocusRequested(nameof(Password));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ChangeAccount()
        {
            Mode = EditMode.EditAccount;
            Error = null;
        }

        public enum EditMode
        {
            None,
            EditAccount,
            EditPrincipal
        }
    }
}