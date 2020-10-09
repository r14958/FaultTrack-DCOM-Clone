namespace FaultTrack.Windows
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base view-model class that represents a closable workspace.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModel, IRequestClose, IRequestFocus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceViewModel"/> class.
        /// </summary>
        protected WorkspaceViewModel()
        {
            CloseCommand = new ActionCommand(p => OnCloseRequested());
            FocusCommand = new ActionCommand(p => OnFocusRequested(p.ToString()));
        }

        /// <summary>
        /// Occurs when this workspace should be closed.
        /// </summary>
        public event EventHandler CloseRequested;

        /// <summary>
        /// Occurs when this workspace requests focus for a property.
        /// </summary>
        public event EventHandler<FocusRequestedEventArgs> FocusRequested;

        /// <summary>
        /// Gets the command that, when invoked, attempts to close this workspace.
        /// </summary>
        public ICommand CloseCommand
        {
            get;
        }

        /// <summary>
        /// Gets the command that, when invoked, attempts to request focus for a property.
        /// </summary>
        public ICommand FocusCommand
        {
            get;
        }

        protected void OnCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        protected void OnFocusRequested(string propertyName)
        {
            FocusRequested?.Invoke(this, new FocusRequestedEventArgs(propertyName));
        }
    }
}