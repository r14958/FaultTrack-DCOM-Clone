namespace FaultTrack.Windows.ViewModels
{
    using System.Windows.Input;
    using Shell;

    public class ShellMenuViewModel
    {
        private readonly IShell shell;

        public ShellMenuViewModel(IShell shell)
        {
            this.shell = shell;
        }

        public ICommand ExitCommand 
        {
            get 
            {
                return new ActionCommand(p => shell.Quit());
            }
        }

        public ICommand SignOutCommand
        {
            get
            {
                return new ActionCommand(async p => await shell.SignOutAsync(),
                                               p => shell.Principal != null);
            }
        }
    }
}