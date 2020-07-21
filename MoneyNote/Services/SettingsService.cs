﻿using MoneyNote.Services.Contracts;
using Plugin.Settings;

namespace MoneyNote.Services
{
    public class SettingsService : ISettingsService
    {
        public int GetCurrentLanguage()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentLanguage", 0);
        }
        public void SetCurrentLanguage(int langId)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentLanguage", langId);
        }
        public bool GetSoundsSettings()
        {
            return CrossSettings.Current.GetValueOrDefault("IsSoundsOn", true);
        }
        public void SetSoundsSettings(bool isSoundsOn)
        {
            CrossSettings.Current.AddOrUpdateValue("IsSoundsOn", isSoundsOn);
        }
        public bool GetAdsSetings()
        {
            return CrossSettings.Current.GetValueOrDefault("IsHideAds", false);
        }
        public void SetAdsSetings(bool IsAdsOn)
        {
            CrossSettings.Current.AddOrUpdateValue("IsHideAds", IsAdsOn);
        }
    }
}
