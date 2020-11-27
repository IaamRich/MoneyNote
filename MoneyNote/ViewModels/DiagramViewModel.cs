using System;
using System.Collections.Generic;
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
        //Variables for normal functionality of the page
        private static ITransactionService _transactionService;
        public string UrlPathSegment => Strings["menu_diagram"];
        public II18N Strings => I18N.Current;
        public IScreen HostScreen { get; }
        //Variables
        private decimal market, restaurant, transport, business, network, entertainment;
        public DateTime CurrentDate { get; set; }
        public string CurrentMonthText { get; set; }
        public decimal CurrentOutlay { get; set; } = 0;
        public ObservableCollection<Category> DiagramList { get; set; } = new ObservableCollection<Category>();
        private List<Transaction> AllData { get; set; } = new List<Transaction>();
        public DiagramViewModel(ITransactionService transactionService, string message = null, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _transactionService = transactionService;
            CurrentDate = DateTime.Now;
            CurrentMonthText = DateTime.Now.ToString("MMMM");
            GetData();
        }

        private void GetData()
        {
            GetCategories();
        }

        private async void GetCategories()
        {
            AllData = await _transactionService.GetAll();
            GetOutlayByMonth(CurrentDate);
        }
        private void GetOutlayByMonth(DateTime date)
        {
            var outlay = from transaction in AllData where (transaction.Type == TransactionType.Spend && transaction.Date.Month == date.Month) select transaction;
            decimal onePercent = outlay.Sum(x => x.Value) / 100;

            foreach (var note in outlay)
            {
                CurrentOutlay += note.Value;
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
