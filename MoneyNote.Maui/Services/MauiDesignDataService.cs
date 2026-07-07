namespace MoneyNote.Maui;

public sealed class MauiDesignDataService
{
    public FinanceDesignSnapshot GetSnapshot()
    {
        var today = DateTime.Today;

        // TODO: replace this in-memory seed data with migrated services once commands and persistence are ready.
        var transactions = new List<Transaction>
        {
            CreateTransaction(108, TransactionType.Add, 1800m, '+', today.AddDays(-1), TransactionBill.Card, "Monthly salary", CategoryType.Salary, "salary.png"),
            CreateTransaction(107, TransactionType.Spend, 42m, '-', today.AddDays(-1), TransactionBill.Card, "Dinner with friends", CategoryType.Restaurant, "restaurant_bar_bistro.png"),
            CreateTransaction(106, TransactionType.Spend, 28m, '-', today.AddDays(-2), TransactionBill.Cash, "City transport", CategoryType.Transport, "transport.png"),
            CreateTransaction(105, TransactionType.Save, 120m, '-', today.AddDays(-3), TransactionBill.Card, "Moved to monthly savings", CategoryType.Save, "save.png"),
            CreateTransaction(104, TransactionType.Spend, 76m, '-', today.AddDays(-4), TransactionBill.Card, "Groceries", CategoryType.Market, "market_shop.png"),
            CreateTransaction(103, TransactionType.Add, 160m, '+', today.AddDays(-7), TransactionBill.Cash, "Freelance payment", CategoryType.Earnings, "additional_earnings.png"),
            CreateTransaction(102, TransactionType.Spend, 18m, '-', today.AddDays(-9), TransactionBill.Cash, "Medicine", CategoryType.Medicine, "medicine.png"),
            CreateTransaction(101, TransactionType.Spend, 54m, '-', today.AddMonths(-1).AddDays(2), TransactionBill.Card, "Home internet", CategoryType.Network, "network_products.png"),
            CreateTransaction(100, TransactionType.Add, 250m, '+', today.AddMonths(-1).AddDays(4), TransactionBill.Card, "Birthday gift", CategoryType.Gift, "gift.png"),
            CreateTransaction(99, TransactionType.Spend, 95m, '-', today.AddMonths(-2).AddDays(6), TransactionBill.Card, "Team lunch", CategoryType.Business, "business.png")
        };

        return new FinanceDesignSnapshot(
            CurrentCash: 680m,
            CurrentCard: 2140m,
            CurrentSavings: 920m,
            CurrentCredit: 120m,
            ShowSavingsInBalance: true,
            AreaCash: false,
            AutoCredit: true,
            LanguageId: 0,
            Transactions: transactions);
    }

    private static Transaction CreateTransaction(
        int id,
        TransactionType type,
        decimal value,
        char mathSymbol,
        DateTime date,
        TransactionBill bill,
        string note,
        CategoryType categoryType,
        string icon)
    {
        return new Transaction
        {
            Id = id,
            Type = type,
            Value = value,
            Date = date,
            Bill = bill,
            MathSymbol = mathSymbol,
            Note = note,
            Category = new CategoryDto
            {
                Id = id,
                Name = categoryType.ToString(),
                Type = categoryType,
                Image = icon
            }
        };
    }
}
