namespace FaultTrack.Web
{
    public sealed class AuthenticationToken
    {
        public AuthenticationToken()
        {
        }

        public AuthenticationToken(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public string UserName
        {
            get;
             set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}