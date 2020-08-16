using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoneyNote.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MoneyNote.Helpers
{
    public static class Settings
    {
        private const string TransactionDataKey = "transaction_data";
        //private static readonly string SettingsDefault = string.Empty;
        private static ISettings AppSettings => CrossSettings.Current;

        public static List<Transaction> TransactionData
        {
            get => AppSettings.GetJsonValueOrDefault(TransactionDataKey, default(List<Transaction>));
            set => AppSettings.AddOrUpdateJsonValue(TransactionDataKey, value);
        }

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
