using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services;
using MoneyNote.Services.Contracts;
using ReactiveUI;
using Splat;

namespace MoneyNote
{
    public class HistoryViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static ITransactionService _transactionService;
        //Variables for normal functionality of the page
        public II18N Strings => I18N.Current;
        public string UrlPathSegment => Strings["menu_history"];
        public IScreen HostScreen { get; }
        //List Variables
        private int lastID = 0;
        public ObservableCollection<TransactionDay> TransactionsList { get; set; } = new ObservableCollection<TransactionDay>();
        public List<Transaction> TransactionsFullList { get; set; } = new List<Transaction>();
        //Commands
        public ICommand GoAnaliticsCommand { get; set; }
        public ICommand ChangeFilterCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DiagramCommand { get; set; }
        //Filter
        public ICommand DisplayAllDate { get; set; }
        public ICommand DisplayLastWeek { get; set; }
        public ICommand DisplayLastMonth { get; set; }
        public ReactiveCommand<string, Unit> SearchingCommand { get; set; }
        public bool IsChangeFilterVisible { get; set; }
        public bool IsSearchPanelVisible { get; set; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public HistoryViewModel(ITransactionService transactionService, IScreen screen = null)
        {
            IsChangeFilterVisible = false;
            _transactionService = transactionService;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            CreateCommands();
            GetData();

            DiagramCommand = ReactiveCommand.CreateFromObservable(() =>
            {
                if (TransactionsFullList != null && TransactionsFullList.Count > 0)
                {
                    return HostScreen.Router.Navigate.Execute(new ViewModels.DiagramViewModel(new TransactionService()/*, TransactionsFullList*/));
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alert", "No Data", "ok");
                    return null;
                }
            });

            this.WhenAnyValue(x => x.SearchText)
                .Where(x => x == "")
                .Throttle(TimeSpan.FromSeconds(1))
                .InvokeCommand(SearchingCommand);

            this.WhenAnyValue(x => x.SearchText)
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Throttle(TimeSpan.FromSeconds(1))
                .InvokeCommand(SearchingCommand);
        }
        private void CreateCommands()
        {
            DisplayAllDate = ReactiveCommand.Create(() =>
            {
                TransactionsList.Clear();
                var tempList = new List<Transaction>();
                TransactionsFullList.ForEach(x => tempList.Add(x));
                DisplayGroupingList(tempList);
            });

            DisplayLastWeek = ReactiveCommand.Create(() =>
            {
                var date = System.DateTime.Now;
                var tempList = new List<Transaction>();
                foreach (var item in TransactionsFullList)
                {
                    var ts = date.Subtract(item.Date);
                    if (ts.Days < 7)
                    {
                        tempList.Add(item);
                    }
                }
                DisplayGroupingList(tempList);
            });

            DisplayLastMonth = ReactiveCommand.Create(() =>
            {
                var date = System.DateTime.Now;
                var tempList = new List<Transaction>();
                foreach (var item in TransactionsFullList)
                {
                    var ts = date.Subtract(item.Date);
                    if (ts.Days < 31)
                    {
                        tempList.Add(item);
                    }
                }
                DisplayGroupingList(tempList);
            });

            SearchCommand = ReactiveCommand.Create(() =>
            {
                IsSearchPanelVisible = !IsSearchPanelVisible;
                IsChangeFilterVisible = false;
            });

            SearchingCommand = ReactiveCommand.Create<string>(searchParameter =>
            {
                var tempList = new List<Transaction>();

                if (!String.IsNullOrWhiteSpace(searchParameter))
                {
                    var dec = 0.0m;
                    if (decimal.TryParse(searchParameter, out dec))
                    {
                        //TransactionsFullList.ForEach(x => x.Value = dec);
                        foreach (var note in TransactionsFullList)
                        {
                            if (note.Value == dec)
                            {
                                tempList.Add(note);
                            }
                        }
                    }
                    else
                    {
                        //TransactionsFullList.ForEach(x => x.Note.Contains(searchParameter?.ToLower()));
                        foreach (var note in TransactionsFullList)
                        {
                            if (note.Note.ToLower().Contains(searchParameter.ToLower()))
                            {
                                tempList.Add(note);
                            }
                        }
                    }
                }
                else TransactionsFullList.ForEach(x => tempList.Add(x));
                DisplayGroupingList(tempList);
            });

            GoAnaliticsCommand = ReactiveCommand.Create(() =>
            {

            });

            ChangeFilterCommand = ReactiveCommand.Create(() =>
            {
                IsChangeFilterVisible = !IsChangeFilterVisible;
                IsSearchPanelVisible = false;
            });
        }
        private async void GetData()
        {
            var data = new List<Transaction>();
            await Task.Run(() =>
            {
                data = _transactionService.GetAll().Result;
                if (data != null && data?.Count > 0)
                {
                    data.Reverse();
                    lastID = data.First().Id;
                    TransactionsFullList = data;
                    DisplayGroupingList(data);
                }
            });
        }
        private void DisplayGroupingList(List<Transaction> data)
        {
            TransactionsList = new ObservableCollection<TransactionDay>();
            var days = data.GroupBy(d => new { year = d.Date.Year, month = d.Date.Month, day = d.Date.Day });
            foreach (var item in days)
            {
                var day = new ObservableCollection<Transaction>();
                foreach (var note in item) day.Add(note);
                TransactionsList.Add(new TransactionDay
                {
                    Date = day.FirstOrDefault().Date,
                    DayNotes = day
                });
            }
        }
    }
    public class TransactionDay : ReactiveObject
    {
        public DateTime Date { get; set; }
        public ObservableCollection<Transaction> DayNotes { get; set; }
    }
}
