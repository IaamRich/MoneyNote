using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services
{
    public class SpendService
    {
        public async Task CreateTable()
        {
            await App.Connection.CreateTableAsync<Spend>();
        }
        public async Task<List<Spend>> GetAll()
        {
            return await App.Connection.Table<Spend>().ToListAsync().ConfigureAwait(false);
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
        //public async Task<int> SaveItemAsync(AllMoney item)
        //{
        //    //if (item.Id != 0)
        //    //{
        //    await database.UpdateAsync(item);
        //    return item.Id;
        //    //}
        //    //else
        //    //{
        //    //    return await database.InsertAsync(item);
        //    //}
        //}
    }
}
