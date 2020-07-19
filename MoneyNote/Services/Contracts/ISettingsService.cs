using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services.Contracts
{
    public interface ISettingsService
    {
        Task<Settings> GetCurrentLanguage();
        Task UpdateAllSettingsAsync(Settings item);
        Task DeleteAll();
    }
}
