namespace FaultTrack.Web
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class AuthorizationToken
    {
        public AuthorizationToken()
        {
            Token = CreateToken();
        }

        public string UserName
        {
            get;
            set;
        }

        public string Token
        {
            get; set;
        }

        private string CreateToken()
        {
            var time      = DateTime.Now.ToString("G");
            var guid      = Guid.NewGuid().ToString("N").ToUpper();
            var seed      = GetSeed().ToString();
            var algorithm = new SHA512Managed();
            var input     = Encoding.UTF8.GetBytes($"{time}{guid}{seed}");
            var output    = algorithm.ComputeHash(input);
            var hash      = new StringBuilder();

            foreach (byte b in output)
            {
                hash.Append(b.ToString("X2"));
            }

            return hash.ToString();
        }

        private int GetSeed()
        {
            using (var algorithm = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[4];

                algorithm.GetBytes(buffer);

                return BitConverter.ToInt32(buffer, 0);
            }
        }
    }
}