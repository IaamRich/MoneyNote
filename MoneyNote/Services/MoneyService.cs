﻿using MoneyNote.Services.Contracts;
using Plugin.Settings;

namespace MoneyNote.Services
{
    public class MoneyService : IMoneyService
    {
        public decimal GetCurrentCash()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentCash", 0.0m);
        }
        public void SetCurrentCash(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentCash", value);
        }
        public decimal GetCurrentCard()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentCard", 0.0m);
        }
        public void SetCurrentCard(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentCard", value);
        }
        public decimal GetCurrentCredit()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentCredit", 0.0m);
        }
        public void SetCurrentCredit(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentCredit", value);
        }
        public decimal GetAllOutlay()
        {
            return CrossSettings.Current.GetValueOrDefault("AllOutlay", 0.0m);
        }
        public void SetAllOutlay(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllOutlay", value);
        }
        public decimal GetAllIncome()
        {
            return CrossSettings.Current.GetValueOrDefault("AllIncome", 0.0m);
        }
        public void SetAllIncome(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllIncome", value);
        }
        public decimal GetAllSavings()
        {
            return CrossSettings.Current.GetValueOrDefault("AllSavings", 0.0m);
        }
        public void SetAllSavings(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("AllSavings", value);
        }
        //uneditable
        public decimal GetCurrentIncome()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentIncome", 0.0m);
        }
        public void SetCurrentIncome(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentIncome", value);
        }
        public decimal GetCurrentOutlay()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentOutlay", 0.0m);
        }
        public void SetCurrentOutlay(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentOutlay", value);
        }
        public decimal GetCurrentSavings()
        {
            return CrossSettings.Current.GetValueOrDefault("CurrentSavings", 0.0m);
        }
        public void SetCurrentSavings(decimal value)
        {
            CrossSettings.Current.AddOrUpdateValue("CurrentSavings", value);
        }
        //deleting
        public void DeleteAllMoneyNotes()
        {
            CrossSettings.Current.Clear();
        }
    }
}
