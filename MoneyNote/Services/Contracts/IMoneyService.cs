namespace MoneyNote.Services.Contracts
{
    public interface IMoneyService
    {
        decimal GetCurrentCash();
        void SetCurrentCash(decimal value);
        decimal GetCurrentCard();
        void SetCurrentCard(decimal value);
        decimal GetCurrentCredit();
        void SetCurrentCredit(decimal value);
        decimal GetAllOutlay();
        void SetAllOutlay(decimal value);
        decimal GetAllIncome();
        void SetAllIncome(decimal value);
        decimal GetAllSavings();
        void SetAllSavings(decimal value);
        decimal GetCurrentIncome();
        void SetCurrentIncome(decimal value);
        decimal GetCurrentOutlay();
        void SetCurrentOutlay(decimal value);
        decimal GetCurrentSavings();
        void SetCurrentSavings(decimal value);
        void DeleteAllMoneyNotes();
    }
}
