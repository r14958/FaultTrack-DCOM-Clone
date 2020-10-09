namespace FaultTrack.Web.Api.Controllers
{
    using System.Web.Http;
    using Business;
    using Data;

    public class PrincipalController : ApiController
    {
        private readonly AuthenticationContext context;

        public PrincipalController()
        {
            context = new AuthenticationContext(new DataContext());
        }

        [HttpPost]
        public void Authenticate(AuthenticateRequest request)
        {
            context.Authenticate(request.Token);
        }

        [HttpPost]
        public SignInResponse SignIn(SignInRequest request)
        {
            return new SignInResponse
                   {
                       Token = context.SignIn(request.Token)
                   };
        }

        [HttpPost]
        public void SignOut(SignOutRequest request)
        {
            context.SignOut(request.Token);
        }
    }
}