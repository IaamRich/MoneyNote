using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyNote.Dtos;
using MoneyNote.Helpers;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;

namespace MoneyNote.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<List<Transaction>> GetAll()
        {
            var dto = Settings.TransactionData?.DataBase;
            return Task.FromResult(dto);
        }
        public Task<List<Transaction>> GetAll(int count)
        {
            var dto = Settings.TransactionData?.DataBase;
            var answer = new List<Transaction>();
            if (dto != null)
            {
                dto.Reverse();
                if (dto.Count < count)
                {
                    foreach (var item in dto)
                    {
                        answer.Add(item);
                    }
                }
                else
                    for (int i = 0; i < count; i++)
                    {
                        answer.Add(dto.FirstOrDefault(x => x.Id == i));
                    }
            }

            return Task.FromResult(answer);
        }
        public Task<bool> Create(Transaction item)
        {
            var dto = Settings.TransactionData?.DataBase;
            if (dto == null)
            {
                Settings.TransactionData = new TransactionDto { DataBase = new List<Transaction>() };
            }
            var all = Settings.TransactionData.DataBase;
            all.Add(item);
            Settings.TransactionData = new TransactionDto { DataBase = all };
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
