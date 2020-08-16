using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;

namespace MoneyNote.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<List<Transaction>> GetAll()
        {
            throw new NotImplementedException();
        }
        public Task<List<Transaction>> GetAll(int count)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Create(Transaction item)
        {
            throw new NotImplementedException();
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
