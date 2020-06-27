using MoneyNote.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyNote.Services
{
    public class MoneyService
    {
        SQLiteAsyncConnection database;

        public MoneyService(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<AllMoney>();
        }
        public async Task<List<AllMoney>> GetItemsAsync()
        {
            return await database.Table<AllMoney>().ToListAsync();

        }
        public async Task<AllMoney> GetItemAsync(int id)
        {
            return await database.GetAsync<AllMoney>(id);
        }
        public async Task<int> DeleteItemAsync(AllMoney item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(AllMoney item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
