namespace FaultTrack.Windows
{
    using System;
    using System.Windows.Input;

    public class DialogViewModel : ViewModel, IDialogRequestClose
    {
        private readonly ICommand closeCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogViewModel"/> class.
        /// </summary>
        protected DialogViewModel()
        {
            this.closeCommand = new ActionCommand(param => OnCloseRequested((bool?)param));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Gets the command that, when invoked, attempts to close this workspace.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return this.closeCommand;
            }
        }

        protected virtual void OnCloseRequested(bool? dialogResult)
        {
            EventHandler<DialogCloseRequestedEventArgs> handler = this.CloseRequested;

            if (handler != null)
            {
                handler(this, new DialogCloseRequestedEventArgs(dialogResult));
            }
        }
    }
}