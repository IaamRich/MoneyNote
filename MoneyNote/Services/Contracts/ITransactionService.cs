using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAll();
        Task<List<Transaction>> GetAll(int count);
        Task<bool> Create(Transaction item);
        Task<bool> Delete(int id);
        Task DeleteAll();
    }
}
