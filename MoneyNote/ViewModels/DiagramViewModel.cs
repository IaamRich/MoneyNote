using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public string TextByPercentage { get; set; }
        public decimal CurrentMainValue { get; set; } = 0;
        public TransactionType CurrentType { get; set; }
        public bool IsDiagramChangeFilterVisible { get; set; }
        public ObservableCollection<PercentageCategory> DiagramList { get; set; } = new ObservableCollection<PercentageCategory>();
        private List<Transaction> AllData { get; set; } = new List<Transaction>();
        public ICommand GoMonthBack { get; set; }
        public ICommand DisplayOutlay { get; set; }
        public ICommand DisplayIncome { get; set; }
        public ICommand GoMonthForward { get; set; }
        public ICommand ChangeDiagramFilterCommand { get; set; }
        public DiagramViewModel(ITransactionService transactionService, string message = null, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _transactionService = transactionService;
            CurrentType = TransactionType.Spend;
            TextByPercentage = Strings["outlay_by_percentage"];
            IsDiagramChangeFilterVisible = false;
            CurrentDate = DateTime.Now;
            CurrentMonthText = DateTime.Now.ToString("MMMM");
            GetCategories();
            CreateCommands();
        }
        private async void GetCategories()
        {
            AllData = await _transactionService.GetAll();
            GetDataByDateAndType(CurrentDate, CurrentType);
        }
        private void CreateCommands()
        {
            GoMonthBack = ReactiveCommand.Create(() =>
            {
                CurrentDate = CurrentDate.AddMonths(-1);
                RefreshData();
                GetDataByDateAndType(CurrentDate, CurrentType);
            });
            GoMonthForward = ReactiveCommand.Create(() =>
            {
                CurrentDate = CurrentDate.AddMonths(1);
                RefreshData();
                GetDataByDateAndType(CurrentDate, CurrentType);
            });
            ChangeDiagramFilterCommand = ReactiveCommand.Create(() =>
            {
                IsDiagramChangeFilterVisible = !IsDiagramChangeFilterVisible;
            });
            DisplayIncome = ReactiveCommand.Create(() =>
            {
                TextByPercentage = Strings["income_by_percentage"];
                CurrentDate = DateTime.Now;
                CurrentType = TransactionType.Add;
                RefreshData();
                GetDataByDateAndType(CurrentDate, CurrentType);
            });
            DisplayOutlay = ReactiveCommand.Create(() =>
            {
                TextByPercentage = Strings["outlay_by_percentage"];
                CurrentDate = DateTime.Now;
                CurrentType = TransactionType.Spend;
                RefreshData();
                GetDataByDateAndType(CurrentDate, CurrentType);
            });
        }
        private void RefreshData()
        {
            CurrentMainValue = 0;
            CurrentMonthText = CurrentDate.ToString("MMMM");
            DiagramList = new ObservableCollection<PercentageCategory>();
        }
        private void GetDataByDateAndType(DateTime date, TransactionType type)
        {
            var outlay = from transaction in AllData where (transaction.Type == type && transaction.Date.Month == date.Month) select transaction;
            decimal onePercent = outlay.Sum(x => x.Value) / 100;

            try
            {
                if (outlay != null && outlay.Count() != 0)
                {
                    var random = new Random();
                    var list = new List<PercentageCategory>();
                    GetCategoriesByType(type, ref list);
                    foreach (var note in outlay)
                    {
                        CurrentMainValue += note.Value;
                        list.FirstOrDefault(x => x.Type == note.Category.Type).Value += note.Value;
                    }
                    list.ToList().ForEach(x => x.Percentage = x.Value / onePercent);
                    var sortList = list.OrderByDescending(x => x.Percentage);
                    sortList.ToList().ForEach(x => DiagramList.Add(x));
                }
                else App.Current.MainPage.DisplayAlert("Alert", "No Data for this Month", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        private void GetCategoriesByType(TransactionType type, ref List<PercentageCategory> list)
        {
            var dataCategories = new List<CategoryDto>();
            if (type == TransactionType.Spend) dataCategories = Categories.GetAllSpendingCategories();
            else dataCategories = Categories.GetAllAddingCategories();
            foreach (var item in dataCategories)
            {
                list.Add(new PercentageCategory
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    Type = item.Type,
                    IsSelected = false,
                    Percentage = 0m,
                    Color = ConvertTypeToColor(item.Type),
                    Value = 0
                });
            }
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
                case CategoryType.Salary:
                    return "#ff6633";
                case CategoryType.Gift:
                    return "#6ff6ff";
                case CategoryType.Earnings:
                    return "#887799";
                case CategoryType.Other:
                    return "#879969";
                default:
                    return "#000000";
            }
        }
    }
}
