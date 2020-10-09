namespace FaultTrack.Shell
{
    using System;
    using System.Security.Principal;
    using Web;

    internal sealed class WebPrincipal : IWebPrincipal
    {
        internal WebPrincipal(Uri account, AuthorizationToken token, IIdentity identity)
        {
            Account = account;
            Token = token;
            Identity = identity;
        }

        public Uri Account
        {
            get;
        }

        public bool IsInRole(string role)
        {
            throw new System.NotImplementedException();
        }

        public IIdentity Identity
        {
            get;
        }

        public AuthorizationToken Token
        {
            get;
        }
    }
}