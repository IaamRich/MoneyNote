using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ICommand SearchingCommand { get; set; }
        //Filter
        public ICommand DisplayAllDate { get; set; }
        public ICommand DisplayLastWeek { get; set; }
        public ICommand DisplayLastMonth { get; set; }
        public bool IsChangeFilterVisible { get; set; }
        public bool IsSearchPanelVisible { get; set; }
        public string SearchText { get; set; }

        public HistoryViewModel(ITransactionService transactionService, IScreen screen = null)
        {
            IsChangeFilterVisible = false;
            _transactionService = transactionService;
            CreateCommands();
            GetData();
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
            });
            SearchingCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    TransactionsList.Clear();
                    TransactionsFullList.ForEach(x => x.Note.Contains(SearchText.ToLower()));
                    foreach (var note in TransactionsFullList)
                    {
                        if (note.Note.ToLower().Contains(SearchText.ToLower()))
                        {
                            TransactionsList.Add(note);
                        }
                    }
                }
            });
            GoAnaliticsCommand = ReactiveCommand.Create(() =>
            {

            });
            ChangeFilterCommand = ReactiveCommand.Create(() =>
            {
                IsChangeFilterVisible = !IsChangeFilterVisible;
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
}
