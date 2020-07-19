using System.Threading.Tasks;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;

namespace MoneyNote.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
            App.Database.CreateTableAsync<Settings>();
        }
        public async Task<Settings> GetCurrentLanguage()
        {
            //    var table = await .ConfigureAwait(true);
            var table = await App.Database.Table<Settings>().FirstOrDefaultAsync().ConfigureAwait(false);//.ToListAsync().ConfigureAwait(false);
            if (table == null)
            {
                return new Settings();
            }
            else return table;
        }
        public async Task UpdateAllSettingsAsync(Settings item)
        {
            await App.Database.UpdateAsync(item);
        }
        public async Task DeleteAll()
        {
            await App.Database.UpdateAsync(new Settings());
        }
    }
}
