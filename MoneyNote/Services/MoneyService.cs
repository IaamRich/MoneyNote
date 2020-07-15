using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services
{
    public class MoneyService
    {
        public MoneyService()
        {
            App.Database.CreateTableAsync<AllMoney>();
        }
        public async Task<AllMoney> GetCurrentBill()
        {
            return await App.Database.Table<AllMoney>().FirstOrDefaultAsync().ConfigureAwait(false);//.ToListAsync().ConfigureAwait(false);
        }
        public async Task UpdateAllMoneyAsync(AllMoney item)
        {
            await App.Database.UpdateAsync(item);
        }
        public async Task DeleteIAll()
        {
            await App.Database.DeleteAsync(new AllMoney());
        }
        //SQLiteAsyncConnection database;

        //public MoneyService(string databasePath)
        //{
        //    database = new SQLiteAsyncConnection(databasePath);
        //}

        //public async Task CreateTable()
        //{
        //    await database.CreateTableAsync<AllMoney>();
        //}
        //public async Task<List<AllMoney>> GetItemsAsync()
        //{
        //    return await database.Table<AllMoney>().ToListAsync();

        //}
        //public async Task<AllMoney> GetItemAsync(int id)
        //{
        //    return await database.GetAsync<AllMoney>(id);
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
        ////public async Task SaveItemAsync(AllMoney item)
        ////{

        ////    await database.UpdateAsync(item);

        ////}
    }
}
