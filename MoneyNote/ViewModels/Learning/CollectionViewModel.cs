using DynamicData;
using MoneyNote.Models;
using MoneyNote.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;

namespace MoneyNote.ViewModels
{
    public class CollectionViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Collections";

        public IScreen HostScreen { get; }
        private readonly IContactServices _contactServices;
        public CollectionViewModel(IScreen screen = null, IContactServices contactService = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _contactServices = contactService ?? Locator.Current.GetService<IContactServices>();

            _contacts.AddRange(_contactServices.GetAllContacts());

            _contacts.Connect().Bind(out _contactsList).Subscribe();

            _contacts.ReplaceAt(2, new Contact
            {
                Email = "replaced@test.com",
                FullName = "Replace Contact",
                Phone = "1234567890"
            });
            _contacts.Move(0, 3);
        }
        private SourceList<Contact> _contacts = new SourceList<Contact>();

        private readonly ReadOnlyObservableCollection<Contact> _contactsList;
        public ReadOnlyObservableCollection<Contact> Contacts => _contactsList;
    }
}
