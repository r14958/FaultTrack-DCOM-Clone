namespace FaultTrack.Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.IdentityModel.Tokens;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Web;
    using System.Web.Http.Filters;
    using Business;

    internal class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, HttpStatusCode> mappings;

        public ApiExceptionFilterAttribute()
        {
            mappings = new Dictionary<Type, HttpStatusCode>
                       {
                           { typeof(ArgumentException), HttpStatusCode.BadRequest },
                           { typeof(ArgumentNullException), HttpStatusCode.BadRequest },
                           { typeof(ArgumentOutOfRangeException), HttpStatusCode.BadRequest },
                           { typeof(BusinessException), HttpStatusCode.BadRequest },
                           { typeof(DbEntityValidationException), HttpStatusCode.InternalServerError },
                           { typeof(DbUpdateException), HttpStatusCode.InternalServerError },
                           { typeof(InvalidCredentialException), HttpStatusCode.Unauthorized },
                           { typeof(SecurityTokenException), HttpStatusCode.Unauthorized }
                       };
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is HttpException)
                {
                    HttpException httpException = (HttpException)context.Exception;
                    HttpStatusCode statusCode = (HttpStatusCode)httpException.GetHttpCode();

                    context.Response = context.Request.CreateErrorResponse(statusCode, httpException);
                }
                else if (mappings.ContainsKey(context.Exception.GetType()))
                {
                    context.Response = context.Request.CreateErrorResponse(mappings[context.Exception.GetType()],
                                                                           context.Exception);
                }
                else
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                                           context.Exception);
                }
            }
        }
    }
}