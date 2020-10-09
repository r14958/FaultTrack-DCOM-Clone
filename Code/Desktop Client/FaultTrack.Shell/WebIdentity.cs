namespace FaultTrack.Shell
{
    using System.Security.Principal;

    internal sealed class WebIdentity : IIdentity
    {
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}