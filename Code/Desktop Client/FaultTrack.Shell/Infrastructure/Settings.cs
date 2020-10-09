namespace FaultTrack.Shell.Infrastructure
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.Win32;

    internal sealed class Settings : ISettings
    {
        private readonly RegistryProvider provider;

        internal Settings()
        {
            provider = new RegistryProvider();
        }

        public string Account
        {
            get { return provider.GetValue<String>(nameof(Account)); }
            internal set { provider.SetValue(nameof(Account), value); }
        }

        public string UserName
        {
            get { return provider.GetValue<String>(nameof(UserName)); }
            internal set { provider.SetValue(nameof(UserName), value); }
        }

        public string UserToken
        {
            get
            {
                byte[] protectedData = provider.GetValue<byte[]>(nameof(UserToken));

                if (protectedData == null)
                {
                    return null;
                }

                byte[] data = ProtectedData.Unprotect(protectedData, null, DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(data);
            }
            internal set
            {
                if (value == null)
                {
                    provider.DeleteValue(nameof(UserToken));
                    return;
                }

                byte[] data = Encoding.UTF8.GetBytes(value);

                byte[] protectedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

                provider.SetValue(nameof(UserToken), protectedData, RegistryValueKind.Binary);
            }
        }
    }
}