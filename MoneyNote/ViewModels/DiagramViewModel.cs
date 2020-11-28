using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
        public DateTime CurrentDate { get; set; }
        public string CurrentMonthText { get; set; }
        public decimal CurrentOutlay { get; set; } = 0;
        public ObservableCollection<PercentageCategory> DiagramList { get; set; } = new ObservableCollection<PercentageCategory>();
        private List<Transaction> AllData { get; set; } = new List<Transaction>();
        public ICommand GoMonthBack { get; set; }
        public ICommand GoMonthForward { get; set; }
        public DiagramViewModel(ITransactionService transactionService, string message = null, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _transactionService = transactionService;
            CurrentDate = DateTime.Now;
            CurrentMonthText = DateTime.Now.ToString("MMMM");
            GetCategories();
            CreateCommands();
        }
        private async void GetCategories()
        {
            AllData = await _transactionService.GetAll();
            GetOutlayByMonth(CurrentDate);
        }
        private void CreateCommands()
        {
            GoMonthBack = ReactiveCommand.Create(() =>
            {
                CurrentDate = CurrentDate.AddMonths(-1);
                RefreshData();
                GetOutlayByMonth(CurrentDate);
            });
            GoMonthForward = ReactiveCommand.Create(() =>
            {
                CurrentDate = CurrentDate.AddMonths(1);
                RefreshData();
                GetOutlayByMonth(CurrentDate);
            });
        }
        private void RefreshData()
        {
            CurrentOutlay = 0;
            CurrentMonthText = CurrentDate.ToString("MMMM");
            DiagramList = new ObservableCollection<PercentageCategory>();
        }
        private void GetOutlayByMonth(DateTime date)
        {
            var outlay = from transaction in AllData where (transaction.Type == TransactionType.Spend && transaction.Date.Month == date.Month) select transaction;
            decimal onePercent = outlay.Sum(x => x.Value) / 100;

            if (outlay.Any())
            {
                var random = new Random();
                var list = new ObservableCollection<PercentageCategory>();
                Categories.GetAllSpendingCategories().ForEach(x => list.Add(new PercentageCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Type = x.Type,
                    IsSelected = false,
                    Percentage = 0,
                    //Color = String.Format("#{0:X6}", random.Next(0x1000000)),
                    Color = ConvertTypeToColor(x.Type),
                    Value = 0
                }));
                foreach (var note in outlay)
                {
                    CurrentOutlay += note.Value;
                    list.FirstOrDefault(x => x.Type == note.Category.Type).Value += note.Value;
                }
                list.ToList().ForEach(x => x.Percentage = x.Value / onePercent);
                var sortList = list.OrderByDescending(x => x.Percentage);
                sortList.ToList().ForEach(x => DiagramList.Add(x));
            }
            else App.Current.MainPage.DisplayAlert("Alert", "No Data for this Month", "Ok");
        }

        //Temp Method
        private string ConvertTypeToColor(CategoryType type)
        {
            switch (type)
            {
                case CategoryType.Market:
                    return "#ff6633";
                case CategoryType.Restaurant:
                    return "#6ff6ff";
                case CategoryType.Transport:
                    return "#887799";
                case CategoryType.Business:
                    return "#879969";
                case CategoryType.Network:
                    return "#ff80c0";
                case CategoryType.Entertainment:
                    return "#e8f0ba";
                default:
                    return "#000000";
            }
        }
    }
}
