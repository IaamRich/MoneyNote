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
        public ObservableCollection<Transaction> TransactionsList { get; set; }
        //Commands
        public ICommand ChangeNotes { get; set; }
        public bool IsChangeNotesVisible { get; set; }
        public ICommand ChangeNotesLook { get; set; }
        public ICommand DisplayLastWeek { get; set; }
        public bool IsChangeNotesLookVisible { get; set; }

        public HistoryViewModel(ITransactionService transactionService, IScreen screen = null)
        {
            IsChangeNotesVisible = false;
            IsChangeNotesLookVisible = false;
            _transactionService = transactionService;
            CreateCommands();
            GetData();
        }
        private void CreateCommands()
        {
            DisplayLastWeek = ReactiveCommand.Create(() =>
            {
                var date = System.DateTime.Now;
                var g = new List<Transaction>(); g = TransactionsList.ToList();
                TransactionsList.Clear();
                foreach (var item in g)
                {
                    if (item.Date.Day == date.Day)
                    {
                        TransactionsList.Add(item);
                    }
                }
            });
            ChangeNotes = ReactiveCommand.Create(() =>
            {
                IsChangeNotesVisible = !IsChangeNotesVisible; IsChangeNotesLookVisible = false;
            });
            ChangeNotesLook = ReactiveCommand.Create(() => { IsChangeNotesLookVisible = !IsChangeNotesLookVisible; IsChangeNotesVisible = false; });
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
                }
            });
        }
    }
}
