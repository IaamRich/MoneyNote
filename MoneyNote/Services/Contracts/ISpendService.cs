using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyNote.Models;

namespace MoneyNote.Services.Contracts
{
    public interface ISpendService
    {
        Task<List<Spend>> GetAll();
        Task<int> SaveItemAsync(Spend item);
        Task DeleteAll();
        //IEnumerable<Contact> GetAllContacts();

        //Task<IEnumerable<Contact>> GetContactsAsync();
        //Task<Contact> GetContact(int id);
        //Task AddContact(Contact contact);
        //Task UpdateContact(Contact contact);
        //Task DeleteContact(Contact contact);
    }
}
