namespace FaultTrack.Web.Api.Controllers
{
    using System.IO;
    using System.Net.Http;
    using System.Web.Http;

    public class BootstrapperController : ApiController
    {
        [HttpGet]
        public GetServerVersionResponse GetServerVersion()
        {
            return new GetServerVersionResponse
                   {
                       Version = "2.0.0.0"
                   };
        }

        [HttpGet]
        public HttpResponseMessage DownloadClient()
        {
#if DEBUG
            System.Threading.Thread.Sleep(2000);
#endif
            string path = @"F:\DCOM\FaultTrack\Main\Build\Client\FaultTrack.exe";

            return new HttpResponseMessage
                   {
                       Content = new StreamContent(File.OpenRead(path))
                   };
        }
    }
}