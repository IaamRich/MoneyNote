using System.Collections.ObjectModel;
using System.Linq;
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
        private decimal market, restaurant, transport, business, network, entertainment;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_diagram"];
        public II18N Strings => I18N.Current;
        public IScreen HostScreen { get; }
        //Variables
        public ObservableCollection<Category> DiagramList { get; set; } = new ObservableCollection<Category>();
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
            GetCategories();
        }

        private async void GetCategories()
        {
            var data = await _transactionService.GetAll();
            var outlay = from transaction in data where transaction.Type == TransactionType.Spend select transaction;
            decimal onePercent = outlay.Sum(x => x.Value) / 100;

            foreach (var note in outlay)
            {
                switch (note.Category.Type)
                {
                    case CategoryType.Market:
                        market += note.Value;
                        break;
                    case CategoryType.Restaurant:
                        restaurant += note.Value;
                        break;
                    case CategoryType.Transport:
                        transport += note.Value;
                        break;
                    case CategoryType.Business:
                        business += note.Value;
                        break;
                    case CategoryType.Network:
                        network += note.Value;
                        break;
                    case CategoryType.Entertainment:
                        entertainment += note.Value;
                        break;
                    default:
                        break;
                }
            }
            market /= onePercent;
            restaurant /= onePercent;
            transport /= onePercent;
            business /= onePercent;
            network /= onePercent;
            entertainment /= onePercent;
            var list = new ObservableCollection<Category>();
            Categories.GetAllSpendingCategories().ForEach(x => list.Add(new Category
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Type = x.Type,
                IsSelected = false,
                Percentage = GetPercentageByType(x.Type)
            }));
            var sortList = list.OrderByDescending(x => x.Percentage);
            foreach (var item in sortList)
            {
                DiagramList.Add(item);
            }
        }
        private decimal GetPercentageByType(CategoryType type)
        {
            switch (type)
            {
                case CategoryType.Market:
                    return market;
                case CategoryType.Restaurant:
                    return restaurant;
                case CategoryType.Transport:
                    return transport;
                case CategoryType.Business:
                    return business;
                case CategoryType.Network:
                    return network;
                case CategoryType.Entertainment:
                    return entertainment;
                default:
                    return 0;
            }
        }
    }
}
