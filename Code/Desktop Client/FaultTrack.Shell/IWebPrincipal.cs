namespace FaultTrack.Shell
{
    using System;
    using System.Security.Principal;
    using Web;

    public interface IWebPrincipal : IPrincipal
    {
        Uri Account { get; }
        AuthorizationToken Token { get; }
    }
}