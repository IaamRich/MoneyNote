using Plugin.Settings;

namespace MoneyNote.Services
{
    public class MoneyService
    {
        public decimal GetCurrentCash()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentCash", 0);
        }
        public void SetCurrentCash(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentCash", value);
        }
        public decimal GetCurrentCard()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentCard", 0);
        }
        public void SetCurrentCard(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentCard", value);
        }
        public decimal GetAllOutlay()
        {
            return CrossSettings.Current.GetValueOrDefault("AllOutlay", 0);
        }
        public void SetAllOutlay(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllOutlay", value);
        }
        public decimal GetAllIncome()
        {
            return CrossSettings.Current.GetValueOrDefault("AllIncome", 0);
        }
        public void SetAllIncome(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllIncome", value);
        }
        public decimal GetAllSavings()
        {
            return CrossSettings.Current.GetValueOrDefault("AllSavings", 0);
        }
        public void SetAllSavings(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllSavings", value);
        }
        //uneditable
        public decimal GetCurrentIncome()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentIncome", 0);
        }
        public void SetCurrentIncome(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentIncome", value);
        }
        public decimal GetCurrentOutlay()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentOutlay", 0);
        }
        public void SetCurrentOutlay(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentOutlay", value);
        }
        public decimal GetCurrentSavings()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentSavings", 0);
        }
        public void SetCurrentSavings(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentSavings", value);
        }
        //deleting
        public void DeleteAllMoneyNotes()
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentSavings", 0);
            CrossSettings.Current.AddOrUpdateValue("CurrentOutlay", 0);
            CrossSettings.Current.AddOrUpdateValue("CurrentIncome", 0);
            CrossSettings.Current.AddOrUpdateValue("AllSavings", 0);
            CrossSettings.Current.AddOrUpdateValue("AllIncome", 0);
            CrossSettings.Current.AddOrUpdateValue("AllOutlay", 0);
            CrossSettings.Current.AddOrUpdateValue("CurrentCard", 0);
            CrossSettings.Current.AddOrUpdateValue("CurrentCash", 0);
        }
    }
}
