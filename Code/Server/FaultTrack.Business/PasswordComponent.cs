namespace FaultTrack.Business
{
    using System.Security.Cryptography;
    using System.Text;

    public sealed class PasswordComponent
    {
        public string Hash(string password, string salt)
        {
            var saltedVal = $"{salt}{password}";
            var algorithm = new SHA512Managed();
            var input     = Encoding.UTF8.GetBytes(saltedVal);
            var output    = algorithm.ComputeHash(input);
            var hash      = new StringBuilder();

            foreach (byte b in output)
            {
                hash.Append(b.ToString("X2"));
            }

            return hash.ToString();
        }

        public string GetCryptographicSalt()
        {
            var bytes = new byte[128];

            using (RNGCryptoServiceProvider algorithm = new RNGCryptoServiceProvider())
            {
                algorithm.GetBytes(bytes);
            }

            var sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}