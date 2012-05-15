using System.IO.IsolatedStorage;

namespace Ad2_Dashboard
{
    public class AppSettings
    {
        private static readonly IsolatedStorageSettings settings = (!System.ComponentModel.DesignerProperties.IsInDesignTool) ? IsolatedStorageSettings.ApplicationSettings : null;

        private const string _Username = "username";
        private const string _SavePassword = "savePassword";
        private const string _Password = "password";

        public static T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            return (settings != null && settings.Contains(key)) ? (T)settings[key] : defaultValue;
        }

        public static void SetValue<T>(string key, T value)
        {
            if (settings != null) settings[key] = value;
        }

        public static void Save()
        {
            if (!SavePassword) Password = string.Empty;

            if (settings != null) settings.Save();
        }

        public static string Username
        {
            get { return GetValueOrDefault(_Username, string.Empty); }
            set { SetValue(_Username, value); }
        }

        public static bool SavePassword
        {
            get { return GetValueOrDefault(_SavePassword, false); }
            set { SetValue(_SavePassword, value); }
        }

        public static string Password
        {
            get { return GetValueOrDefault(_Password, string.Empty); }
            set { SetValue(_Password, value); }
        }
    }
}
