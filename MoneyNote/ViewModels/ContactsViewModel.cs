using MoneyNote.Models;
using MoneyNote.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace MoneyNote.ViewModels
{
    public class ContactsViewModel : ReactiveObject
    {
        private IContactServices _contactService;

        public ContactsViewModel(IContactServices contactServices = null)
        {
            _contactService = contactServices ?? (IContactServices)Splat.Locator.Current.GetService(typeof(IContactServices));

            //_contacts = new ObservableCollection<Contact>(_samples);
            var allContacts = _contactService.GetAllContacts();
            _contacts = new ObservableCollection<Contact>(allContacts);

            this.WhenAnyValue(vm => vm.SearchQuery)
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(query =>
                {
                    var filteredContacts = allContacts.Where(c => c.FullName.ToLower().Contains(query) ||
                    c.Phone.Contains(query) || c.Email.Contains(query)).ToList();

                    Contacts = new ObservableCollection<Contact>(filteredContacts);
                });

            this.WhenAnyValue(vm => vm.Contacts)
                .Select(contacts =>
                {
                    if (Contacts.Count == allContacts.Count())
                    {
                        return "No filters applied";
                    }
                    return $"{Contacts.Count} have been found in result for '{SearchQuery}'";
                })
                .ToProperty(this, vm => vm.SearchResult, out _searchResult);

            var canExecuteClear = this.WhenAnyValue(vm => vm.SearchQuery)
                .Select(query =>
                {
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        return false;
                    }
                    return true;
                });
            //Simple Command
            ClearCommand = ReactiveCommand.Create(ClearSerach, canExecuteClear);
            //Handle the Exceptions
            ClearCommand.ThrownExceptions.Subscribe(ex =>
            {
                Debug.WriteLine(ex.Message);
            });
        }

        #region Properties
        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set { this.RaiseAndSetIfChanged(ref _searchQuery, value); }
        }

        private readonly ObservableAsPropertyHelper<string> _searchResult;
        public string SearchResult { get => _searchResult.Value; }
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set { this.RaiseAndSetIfChanged(ref _contacts, value); }
        }
        #endregion
        #region Commands
        public ReactiveCommand<Unit, Unit> ClearCommand { get; }
        #endregion

        #region Methods
        private void ClearSerach()
        {
            //throw new Exception("this is an example");
            SearchQuery = string.Empty;
        }
        #endregion
    }
}
