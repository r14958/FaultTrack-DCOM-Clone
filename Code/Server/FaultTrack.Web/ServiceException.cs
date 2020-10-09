namespace FaultTrack.Web
{
    using System;
    using System.Net;

    public class ServiceException : Exception
    {
        public ServiceException(HttpStatusCode statusCode, string reasonPhrase, string message) : base(message)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public HttpStatusCode StatusCode
        {
            get;
        }

        public string ReasonPhrase
        {
            get;
        }
    }
}