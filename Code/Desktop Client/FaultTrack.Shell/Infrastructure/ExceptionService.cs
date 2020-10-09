namespace FaultTrack.Shell.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Text;

    internal sealed class ExceptionService : IExceptionService
    {
        private const string Source = "FaultTrack";

        public ExceptionService()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        public void LogException(Exception ex)
        {
            string message = CreateEntryMessage(ex);
            EventLog.WriteEntry(Source, message, EventLogEntryType.Error);
        }

        private string CreateEntryMessage(Exception exception)
        {
            StringBuilder sb = new StringBuilder("An unhandled exception was thrown in FaultTrack.");

            sb.AppendLine();
            sb.AppendLine(exception.ToString());

            return sb.ToString();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception))
            {
                return;
            }

            LogException((Exception)e.ExceptionObject);
        }
    }
}