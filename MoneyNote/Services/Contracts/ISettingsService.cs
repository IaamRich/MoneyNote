namespace MoneyNote.Services.Contracts
{
    public interface ISettingsService
    {
        int GetCurrentLanguage();
        void SetCurrentLanguage(int langId);
        bool GetSoundsSettings();
        void SetSoundsSettings(bool isSoundsOn);
        int GetDefaultSpendingAreaSettings();
        void SetDefaultSpendingAreaSettings(int idSpendFromWhere);
        bool GetAdsSetings();
        void SetAdsSetings(bool IsAdsOn);
    }
}
