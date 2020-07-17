using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services.Contracts
{
    public interface IMoneyService
    {
        Task<AllMoney> GetCurrentBill();
        Task UpdateAllMoneyAsync(AllMoney item);
        Task DeleteAll();
    }
}
