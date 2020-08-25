using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;
using MoneyNote.Views.Popups;
using Plugin.Settings;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Splat;

namespace MoneyNote
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        //Commands
        public ICommand NavigateToDummyPage { get; set; }
        public ICommand AddSpend { get; set; }
        public ICommand DeleteSpend { get; set; }
        public ICommand UpdateSpend { get; set; }
        public ICommand AddSalary { get; set; }
        public ReactiveCommand<Unit, Unit> SelectRecord { get; set; }
        //Used Services
        private static ITransactionService _transactionService;
        private static IMoneyService _moneyService;
        //UI variables
        public CategoryDto SelectedCategory { get; set; }
        public string SpendDescription { get; set; }
        public string AddMoneyDescription { get; set; }
        public decimal AddMoneyValue { get; set; }
        public string SpendValue { get; set; }
        public decimal CurrentBill { get; set; }
        public decimal CurrentCash { get; set; }
        public decimal CurrentCard { get; set; }
        public bool IsCash { get; set; }
        public bool IsCard { get; set; }
        public bool IsMinusAllowed { get; set; }
        //List Variables
        private int lastID = 0;
        public ObservableCollection<Transaction> LastTransactionsList { get; set; }
        //Main Variables
        public string UrlPathSegment => Strings["menu_main"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        public MainViewModel(ITransactionService transactionService, IMoneyService moneyService, IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _transactionService = transactionService;
            _moneyService = moneyService;
            IsCash = true;
            IsCard = false;
            IsMinusAllowed = CrossSettings.Current.GetValueOrDefault("IsMinusAllowed", false);
            CreateCommands();
            GetData();
        }
        #region Methods
        private void CreateCommands()
        {
            //Reactive Example to navigate
            NavigateToDummyPage = ReactiveCommand
                .CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
            //SelectRecord = ReactiveCommand.Create();
            AddSpend = ReactiveCommand.Create(() => { OnSpend(); });
            AddSalary = ReactiveCommand.Create(() => { OnAddSalary(); });
        }
        private void GetData()
        {
            GetLastTransactions();
            CurrentCard = _moneyService.GetCurrentCard();
            CurrentCash = _moneyService.GetCurrentCash();
            CurrentBill = CurrentCard + CurrentCash;
        }
        private void GetLastTransactions()
        {
            var data = _transactionService.GetAll(20).Result;
            if (data.Count != 0)
            {
                lastID = data.First().Id;
            }
            LastTransactionsList = new ObservableCollection<Transaction>();
            data.ForEach(x => LastTransactionsList.Add(x));
        }
        private async void OnSpend()
        {
            if (String.IsNullOrEmpty(SpendValue))
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
            }
            else if (SpendValue[0] == '0')
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
            }
            else if (SpendValue[0] == '.') await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
            else await PopupNavigation.Instance.PushAsync(new CommitPopupView(OnSpendFunc), true);
        }
        private async void OnSpendFunc()
        {
            SpendDescription = CrossSettings.Current.GetValueOrDefault("CommitMessage", "");
            SelectedCategory = UnwrapSpendingCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedSpendingCategory", 0));
            var item = new Transaction
            {
                Id = lastID + 1,
                Value = decimal.Parse(SpendValue),
                Note = SpendDescription,
                Date = DateTime.Now,
                Type = TransactionType.Spend,
                Category = SelectedCategory,
                MathSymbol = '-'
            };
            await _transactionService.Create(item);
            switch (CrossSettings.Current.GetValueOrDefault("CurrentCommitMoneyFrom", 0))
            {
                case 0:
                    if (item.Value > CurrentCash && !IsMinusAllowed)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_cash"]), true);
                        return;
                    }
                    CurrentCash -= item.Value;
                    _moneyService.SetCurrentCash(CurrentCash);
                    break;
                case 1:
                    if (item.Value > CurrentCard && !IsMinusAllowed)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_card"]), true);
                        return;
                    }
                    CurrentCard -= item.Value;
                    _moneyService.SetCurrentCard(CurrentCard);
                    break;
            }
            LastTransactionsList.Clear();
            SpendValue = "";
            GetData();
        }
        private async void OnAddSalary()
        {
            await PopupNavigation.Instance.PushAsync(new AddMoneyPopupView(OnAddSalaryFunc), true);

        }
        private async void OnAddSalaryFunc()
        {
            AddMoneyDescription = CrossSettings.Current.GetValueOrDefault("AddMoneyMessage", "");
            AddMoneyValue = CrossSettings.Current.GetValueOrDefault("AddMoneyValue", 0.0m);
            SelectedCategory = UnwrapAddingCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedAddingCategory", 0));
            var item = new Transaction
            {
                Id = lastID + 1,
                Value = AddMoneyValue,
                Note = AddMoneyDescription,
                Date = DateTime.Now,
                Type = TransactionType.Save,
                Category = SelectedCategory,
                MathSymbol = '+'
            };
            await _transactionService.Create(item);
            switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
            {
                case 0:
                    CurrentCash += item.Value;
                    _moneyService.SetCurrentCash(CurrentCash);
                    break;
                case 1:
                    CurrentCard += item.Value;
                    _moneyService.SetCurrentCard(CurrentCard);
                    break;
            }
            LastTransactionsList.Clear();
            GetData();
        }
        private CategoryDto UnwrapAddingCategoryType(int id)
        {
            foreach (var item in Categories.GetAllAddingCategories())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return new CategoryDto();
        }
        private CategoryDto UnwrapSpendingCategoryType(int id)
        {
            foreach (var item in Categories.GetAllSpendingCategories())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return new CategoryDto();
        }
        #endregion
    }
}
