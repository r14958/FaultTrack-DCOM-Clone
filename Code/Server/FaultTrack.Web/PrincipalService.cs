namespace FaultTrack.Web
{
    using System;
    using System.Threading.Tasks;

    public class PrincipalService : ApiService
    {
        public PrincipalService(Uri account) : base(account)
        {
        }

        public virtual async Task AuthenticateAsync(AuthenticateRequest request)
        {
            await Post("api/Principal/Authenticate", request);
        }

        public virtual async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            return await Post<SignInRequest, SignInResponse>("api/Principal/SignIn", request);
        }

        public virtual async Task SignOutAsync(SignOutRequest request)
        {
            await Post("api/Principal/SignOut", request);
        }
    }
}