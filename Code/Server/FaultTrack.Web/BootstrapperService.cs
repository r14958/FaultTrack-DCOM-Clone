namespace FaultTrack.Web
{
    using System;
    using System.Threading.Tasks;

    public class BootstrapperService : ApiService
    {
        public BootstrapperService(Uri serviceUri) : base(serviceUri)
        {
        }

        public virtual async Task<GetServerVersionResponse> GetServerVersion()
        {
            return await Get<GetServerVersionResponse>("api/bootstrapper/getserverversion");
        }
    }
}