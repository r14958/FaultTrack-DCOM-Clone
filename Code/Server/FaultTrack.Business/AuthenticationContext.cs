namespace FaultTrack.Business
{
    using System;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Authentication;
    using Data;
    using Web;

    public class AuthenticationContext
    {
        private readonly IDataContext context;

        public AuthenticationContext(IDataContext context)
        {
            this.context = context;
        }

        public void Authenticate(AuthorizationToken authorizationToken)
        {
            UserToken token = context.UserTokens.SingleOrDefault(p => p.User.UserName.Equals(authorizationToken.UserName, StringComparison.OrdinalIgnoreCase) &&
                                                                 p.Token == authorizationToken.Token);

            if (token == null || token.ExpirationDate < DateTime.Now)
            {
                if (token != null)
                {
                    context.Delete(token);
                    context.Commit();
                }

                throw new SecurityTokenException("You have been signed out due to inactivity. Please sign in again.");
            }

            token.ExpirationDate = token.ExpirationDate.AddDays(1);

            context.Update(token);
            context.Commit();
        }

        public AuthorizationToken SignIn(AuthenticationToken token)
        {
            User user;

            try
            {
                user = context.Users.Single(p => p.UserName.Equals(token.UserName, StringComparison.OrdinalIgnoreCase));
            }
            catch (InvalidOperationException)
            {
                throw new InvalidCredentialException("Invalid username or password.");
            }

            var passwordComponent = new PasswordComponent();
            var password = passwordComponent.Hash(token.Password, user.PasswordSalt);

            if (user.Password != password)
            {
                throw new InvalidCredentialException("Invalid username or password.");
            }

            var authToken = new AuthorizationToken
                            {
                                UserName = user.UserName
                            };

            var userToken = new UserToken
                            {
                                UserId = user.UserId,
                                ExpirationDate = DateTime.Now.AddDays(3),
                                Token = authToken.Token
                            };

            context.Add(userToken);
            context.Commit();

            return authToken;
        }

        public void SignOut(AuthorizationToken authorizationToken)
        {
            UserToken token;

            try
            {
                token = context.UserTokens.Single(p => p.Token == authorizationToken.Token);
            }
            catch (InvalidOperationException)
            {
                throw new SecurityTokenException("User is not signed in.");
            }

            context.Delete(token);
            context.Commit();
        }
    }
}