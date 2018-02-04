using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LoginKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string Login
        {
            get { return AppSettings.GetValueOrDefault(LoginKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(LoginKey, value); }
        }

    }
}
