using MoneyNote.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace MoneyNote.ViewModels
{
    public class ContactsViewModel : ReactiveObject
    {
        private List<Contact> _samples = new List<Contact>()
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

        public ContactsViewModel()
        {
            _contacts = new ObservableCollection<Contact>(_samples);

            this.WhenAnyValue(vm => vm.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(query =>
                {
                    var filteredContacts = _samples.Where(c => c.FullName.ToLower().Contains(query) ||
                    c.Phone.Contains(query) || c.Email.Contains(query)).ToList();

                    Contacts = new ObservableCollection<Contact>(filteredContacts);
                });
        }

        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set { this.RaiseAndSetIfChanged(ref _searchQuery, value); }
        }

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set { this.RaiseAndSetIfChanged(ref _contacts, value); }
        }
    }
}
