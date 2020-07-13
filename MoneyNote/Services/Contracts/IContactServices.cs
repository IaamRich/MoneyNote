using MoneyNote.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyNote.Services
{
    public interface IContactServices
    {
        IEnumerable<Contact> GetAllContacts();

        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContact(int id);
        Task AddContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(Contact contact);

    }

    public class StaticContactsService : IContactServices
    {
        private static List<Contact> _samples = new List<Contact>()
        {
            new Contact{FullName = "GGGG", Email = "gggg@gmail.com", Phone="092358182"},
            new Contact{FullName = "GGGG1", Email = "gggg1@gmail.com", Phone="092358182"},
            new Contact{FullName = "GGGG2", Email = "gggg2@gmail.com", Phone="097345182"},
            new Contact{FullName = "GGGG3", Email = "gggg3@gmail.com", Phone="093468182"},
            new Contact{FullName = "GGGG4", Email = "gggg4@gmail.com", Phone="034568182"},
            new Contact{FullName = "GGGG5", Email = "gggg5@gmail.com", Phone="0923465182"},
            new Contact{FullName = "GGGG6", Email = "gggg6@gmail.com", Phone="0974328182"},
            new Contact{FullName = "GGGG7", Email = "gggg7@gmail.com", Phone="092236182"},
        };

        public Task AddContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _samples;
        }

        public Task<Contact> GetContact(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }
    }
}
