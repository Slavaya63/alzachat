using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AlzaMobile.Core
{
    public class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string BaseUrlKey = "base_url_key";
        private static readonly string BaseUrlKeyDefault = @"http://127.0.0.1:5000";

        private const string CurrentTokenKey = "current_token";
        private static readonly string CurrentTokenDefault = string.Empty;

        private const string ProfileTypeKey = "profile_type";
        private static readonly string ProfileTypeDefault = string.Empty;

        #endregion


        public static string BaseUrl
        {
            get => AppSettings.GetValueOrDefault(BaseUrlKey, BaseUrlKeyDefault);
            set => AppSettings.AddOrUpdateValue(BaseUrlKey, value);
        }

        public static string CurrentToken
        {
            get => AppSettings.GetValueOrDefault(CurrentTokenKey, CurrentTokenDefault);
            set => AppSettings.AddOrUpdateValue(CurrentTokenKey, value);
        }

        public static string ProfileType
        {
            get => AppSettings.GetValueOrDefault(ProfileTypeKey, ProfileTypeDefault);
            set => AppSettings.AddOrUpdateValue(ProfileTypeKey, value);
        }
    }
}
