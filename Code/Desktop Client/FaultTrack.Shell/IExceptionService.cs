namespace FaultTrack.Shell
{
    using System;

    public interface IExceptionService
    {
        void LogException(Exception ex);
    }
}