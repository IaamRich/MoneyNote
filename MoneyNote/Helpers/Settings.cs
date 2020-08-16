using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MoneyNote.Helpers
{
    public static class Settings
    {
        //private const string LoginInfoKey = "login_info";
        //private const string UserInfoKey = "user_info";
        private static readonly string SettingsDefault = string.Empty;
        private static ISettings AppSettings => CrossSettings.Current;

        //public static LoginInfo LoginInfo
        //{
        //    get => AppSettings.GetJsonValueOrDefault(LoginInfoKey, default(LoginInfo));
        //    set => AppSettings.AddOrUpdateJsonValue(LoginInfoKey, value);
        //}

        //public static UserInfo UserInfo
        //{
        //    get => AppSettings.GetJsonValueOrDefault(UserInfoKey, default(UserInfo));
        //    set => AppSettings.AddOrUpdateJsonValue(UserInfoKey, value);
        //}

        public static T GetJsonValueOrDefault<T>(this ISettings self, string key, T defaultValue = default)
        {
            var json = self.GetValueOrDefault(key, "");

            if (string.IsNullOrEmpty(json))
                return defaultValue;

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to parse json:\n{0}\n{1}", json, ex);
                return defaultValue;
            }
        }

        public static void AddOrUpdateJsonValue(this ISettings self, string key, object value)
        {
            var json = JsonConvert.SerializeObject(value);
            self.AddOrUpdateValue(key, json);
        }
    }
}
