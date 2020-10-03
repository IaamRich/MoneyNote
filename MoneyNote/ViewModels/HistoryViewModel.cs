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
using MoneyNote.Services.Contracts;
using ReactiveUI;

namespace MoneyNote
{
    public class HistoryViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static ITransactionService _transactionService;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_history"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        //List Variables
        private int lastID = 0;
        public ObservableCollection<Transaction> TransactionsList { get; set; } = new ObservableCollection<Transaction>();
        public List<Transaction> TransactionsFullList { get; set; } = new List<Transaction>();
        //Commands
        public ICommand GoAnaliticsCommand { get; set; }
        public ICommand ChangeFilterCommand { get; set; }
        public ICommand SearchCommand { get; set; }
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
            CreateCommands();
            GetData();

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
                TransactionsFullList.ForEach(x => TransactionsList.Add(x));
            });

            DisplayLastWeek = ReactiveCommand.Create(() =>
            {
                var date = System.DateTime.Now;
                var tempList = new List<Transaction>(); tempList = TransactionsFullList;
                TransactionsList.Clear();
                foreach (var item in tempList)
                {
                    var ts = date.Subtract(item.Date);
                    if (ts.Days <= 7)
                    {
                        TransactionsList.Add(item);
                    }
                }
            });

            DisplayLastMonth = ReactiveCommand.Create(() =>
            {
                var date = System.DateTime.Now;
                var tempList = new List<Transaction>(); tempList = TransactionsFullList;
                TransactionsList.Clear();
                foreach (var item in tempList)
                {
                    var ts = date.Subtract(item.Date);
                    if (ts.Days <= 31)
                    {
                        TransactionsList.Add(item);
                    }
                }
            });

            SearchCommand = ReactiveCommand.Create(() =>
            {
                IsSearchPanelVisible = !IsSearchPanelVisible;
                IsChangeFilterVisible = false;
            });

            SearchingCommand = ReactiveCommand.Create<string>(searchParameter =>
            {
                TransactionsList.Clear();
                if (!String.IsNullOrWhiteSpace(searchParameter))
                {
                    TransactionsFullList.ForEach(x => x.Note.Contains(searchParameter?.ToLower()));
                    foreach (var note in TransactionsFullList)
                    {
                        if (note.Note.ToLower().Contains(searchParameter.ToLower()))
                        {
                            TransactionsList.Add(note);
                        }
                    }
                }
                else TransactionsFullList.ForEach(x => TransactionsList.Add(x));
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
                    TransactionsList = new ObservableCollection<Transaction>();
                    data.ForEach(x => TransactionsList.Add(x));
                    TransactionsFullList = TransactionsList.ToList();
                }
            });
        }
    }
    public class TransactionDay
    {
        public ObservableCollection<Transaction> Transaction { get; set; }
    }
}
