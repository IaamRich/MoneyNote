using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services
{
    public class SpendService
    {
        public async Task CreateTable()
        {
            await App.Database.CreateTableAsync<Spend>();
        }
        public async Task<List<Spend>> GetAll()
        {
            return await App.Database.Table<Spend>().ToListAsync().ConfigureAwait(false);
        }
        public async Task<int> SaveItemAsync(Spend item)
        {
            if (item.Id != 0)
            {
                await App.Database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await App.Database.InsertAsync(item);
            }
        }
        //public async Task<List<AllMoney>> GetItemsAsync()
        //{
        //    return await database.Table<AllMoney>().ToListAsync();

        //}
        //public async Task<AllMoney> GetItemAsync(int id)
        //{
        //    return await database.GetAsync<AllMoney>(id);
        //}
        //public async Task<int> DeleteItemAsync(AllMoney item)
        //{
        //    return await database.DeleteAsync(item);
        //}

    }
}
