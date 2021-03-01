namespace MoneyNote.Services.Contracts
{
    public interface ISettingsService
    {
        int GetCurrentLanguageSettings();
        void SetCurrentLanguageSettings(int langId);
        bool GetSoundsSettings();
        void SetSoundsSettings(bool isSoundsOn);
        int GetDefaultSpendingAreaSettings();
        void SetDefaultSpendingAreaSettings(int idSpendFromWhere);
        bool GetAutoCreditSettings();
        void SetAutoCreditSettings(bool isMinus);
        bool GetAdsSetings();
        void SetAdsSetings(bool IsAdsOn);
        bool GetBalanceSettings();
        void SetBalanceSettings(bool IsSavingsOn);
    }
}
