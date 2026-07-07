using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MoneyNote.Maui;

public sealed class LegacyDashboardService
{
    private const string TransactionDataKey = "transactiondto";
    private readonly LegacySettingsReader _settingsReader;

    public LegacyDashboardService(LegacySettingsReader settingsReader)
    {
        _settingsReader = settingsReader;
    }

    public DashboardSnapshot GetSnapshot()
    {
        var currentCash = _settingsReader.GetDecimal("CurrentCash", 0m);
        var currentCard = _settingsReader.GetDecimal("CurrentCard", 0m);
        var currentCredit = _settingsReader.GetDecimal("CurrentCredit", 0m);
        var currentSavings = _settingsReader.GetDecimal("AllSavings", 0m);
        var showSavingsInBalance = _settingsReader.GetBool("IsShowSavingsAllowed", true);
        var currentBalance = currentCash + currentCard + (showSavingsInBalance ? currentSavings : 0m);

        return new DashboardSnapshot(
            currentBalance,
            currentCash,
            currentCard,
            currentCredit,
            currentSavings,
            GetRecentTransactions());
    }

    private IReadOnlyList<Transaction> GetRecentTransactions()
    {
        var json = _settingsReader.GetString(TransactionDataKey);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        try
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));

            var transactionDto = JsonConvert.DeserializeObject<TransactionDto>(json, settings);
            return transactionDto?.DataBase
                ?.AsEnumerable()
                .Reverse()
                .Take(20)
                .ToList() ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }
}

public sealed record DashboardSnapshot(
    decimal CurrentBalance,
    decimal CurrentCash,
    decimal CurrentCard,
    decimal CurrentCredit,
    decimal CurrentSavings,
    IReadOnlyList<Transaction> RecentTransactions);
