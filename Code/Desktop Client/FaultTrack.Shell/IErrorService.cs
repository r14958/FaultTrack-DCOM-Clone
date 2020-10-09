namespace FaultTrack.Shell
{
    using System;

    public interface IErrorService
    {
        void Handle(Exception ex);
    }
}