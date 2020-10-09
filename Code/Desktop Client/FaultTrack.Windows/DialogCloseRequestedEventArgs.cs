namespace FaultTrack.Windows
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="IDialogRequestClose.CloseRequested"/> event.
    /// </summary>
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogCloseRequestedEventArgs"/> class.
        /// </summary>
        /// <param name="dialogResult">The value indicating the dialog's result.</param>
        public DialogCloseRequestedEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        /// <summary>
        /// Gets the dialog's result.
        /// </summary>
        public bool? DialogResult
        {
            get;
            private set;
        }
    }
}