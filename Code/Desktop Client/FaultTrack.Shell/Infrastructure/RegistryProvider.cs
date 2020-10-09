namespace FaultTrack.Shell.Infrastructure
{
    using System;
    using Microsoft.Win32;

    internal sealed class RegistryProvider
    {
        private const string Key = @"SOFTWARE\DCOM Engineering\FaultTrack";

        public void DeleteValue(string setting)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Key, true);

            if (key == null)
            {
                return;
            }

            try
            {
                key.DeleteValue(setting, false);
            }
            finally
            {
                key.Dispose();
            }
        }

        public T GetValue<T>(string setting) where T : class
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Key);

            if (key == null)
            {
                return null;
            }

            try
            {
                return (T)Convert.ChangeType(key.GetValue(setting), typeof(T));
            }
            finally
            {
                key.Dispose();
            }
        }

        public void SetValue<T>(string setting, T value)
        {
            SetValue(setting, value, RegistryValueKind.String);
        }

        public void SetValue<T>(string setting, T value, RegistryValueKind kind)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(Key, true);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(Key, true);
            }

            try
            {
                key.SetValue(setting, value, kind);
            }
            finally
            {
                key.Dispose();
            }
        }
    }
}