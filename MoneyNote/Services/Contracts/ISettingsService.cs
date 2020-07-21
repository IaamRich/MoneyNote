namespace MoneyNote.Services.Contracts
{
    public interface ISettingsService
    {
        int GetCurrentLanguage();
        void SetCurrentLanguage(int langId);
        bool GetSoundsSettings();
        void SetSoundsSettings(bool langId);
        bool GetAdsSetings();
        void SetAdsSetings(bool IsAdsOn);
    }
}
