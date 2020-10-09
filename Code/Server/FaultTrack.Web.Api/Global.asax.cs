namespace FaultTrack.Web.Api
{
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Filters.Add(new ApiExceptionFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ValidationActionFilterAttribute());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}