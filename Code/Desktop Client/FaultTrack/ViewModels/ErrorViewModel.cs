namespace FaultTrack.Windows.ViewModels
{
    using System;

    public class ErrorViewModel : WorkspaceViewModel, IDialogRequestClose
    { 
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public ErrorViewModel(Exception ex)
        {
            Exception = ex;
        }

        public Exception Exception
        {
            get;
            private set;
        }

        public string ExceptionType
        {
            get
            {
                Type t = this.Exception.GetType();

                return t.Name;
            }
        }
    }
}