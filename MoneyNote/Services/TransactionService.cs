using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyNote.Helpers;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;

namespace MoneyNote.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<List<Transaction>> GetAll()
        {
            var dto = Settings.TransactionData;
            return Task.FromResult(dto);
        }
        public Task<List<Transaction>> GetAll(int count)
        {
            var dto = Settings.TransactionData;
            var answer = new List<Transaction>();
            for (int i = 0; i < count; i++)
            {
                answer.Add(dto.FirstOrDefault(x => x.Id == i));
            }
            return Task.FromResult(answer);
        }
        public Task<bool> Create(Transaction item)
        {
            var all = Settings.TransactionData;
            all.Add(item);
            Settings.TransactionData = all;
            return Task.FromResult(true);
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
