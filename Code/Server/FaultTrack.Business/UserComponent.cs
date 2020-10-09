namespace FaultTrack.Business
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data;

    public class UserComponent
    {
        private readonly IDataContext context;

        public UserComponent(IDataContext context)
        {
            this.context = context;
        }

        public User CreateUser(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (password.Length < 6)
            {
                throw new BusinessException("Password must be atleast 6 characters in length.");
            }

            var passwordComponent = new PasswordComponent();
            var salt              = passwordComponent.GetCryptographicSalt();
            var user              = new User
                                    {
                                        UserName     = email,
                                        Password     = passwordComponent.Hash(password, salt),
                                        PasswordSalt = salt,
                                        Email        = email
                                    };

            ValidateObject(user);

            context.Add(user);
            context.Commit();

            return user;
        }

        private void ValidateObject(object obj)
        {
            var context = new ValidationContext(obj);

            try
            {
                Validator.ValidateObject(obj, context);
            }
            catch (ValidationException ex)
            {
                throw new BusinessException($"Validation failed for type {obj.GetType().Name}. See the inner exception details.", ex);
            }
        }

        public void DeleteUser(string email)
        {
            User user;

            try
            {
                user = context.Users.Single(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"User with email {email} does not exist.");
            }

            context.Delete(user);
            context.Commit();
        }
    }
}