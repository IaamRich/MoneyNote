using System.Collections.ObjectModel;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;
using ReactiveUI;
using Splat;

namespace MoneyNote.ViewModels
{
    public class DiagramViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static IMoneyService _moneyService;
        private static ISettingsService _settingsService;
        private static ITransactionService _transactionService;
        private string _message;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_diagram"];
        public II18N Strings => I18N.Current;
        public IScreen HostScreen { get; }
        //Variables
        public ObservableCollection<Category> DiagramList { get; set; }
        public DiagramViewModel(ITransactionService transactionService, IMoneyService moneyService, ISettingsService settingsService, string message = null, IScreen screen = null)
        {
            _message = message;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            // if (!string.IsNullOrEmpty(_message)) Application.Current.MainPage.DisplayAlert("Message", message, "", "ok"); ;
            _transactionService = transactionService;
            _moneyService = moneyService;
            _settingsService = settingsService;
            GetData();
        }

        private void GetData()
        {
            var data = _transactionService.GetAll();
            GetCategories();
        }

        private void GetCategories()
        {
            DiagramList = new ObservableCollection<Category>();
            Categories.GetAllSpendingCategories().ForEach(x => DiagramList.Add(new Category
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Type = x.Type,
                IsSelected = false,
                Percentage = "20%"
            }));
        }
    }
}
