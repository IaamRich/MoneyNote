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
        public ICommand BankCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ReactiveCommand<Transaction, Unit> SelectNote { get; set; }
        //Used Services
        private static ITransactionService _transactionService;
        private static IMoneyService _moneyService;
        private static ISettingsService _settingsService;
        //UI variables
        public CategoryDto SelectedCategory { get; set; }
        public bool IsCreditVisible { get; set; }
        public string SpendDescription { get; set; }
        public string AddMoneyDescription { get; set; }
        public string CreditDescription { get; set; }
        public string SaveDescription { get; set; }
        public decimal AddMoneyValue { get; set; }
        public decimal CreditValue { get; set; }
        public decimal SaveValue { get; set; }
        public string SpendValue { get; set; }
        public decimal CurrentBill { get; set; }
        public decimal CurrentCash { get; set; }
        public decimal CurrentCard { get; set; }
        public decimal CurrentCredit { get; set; }
        public decimal CurrentAllSavings { get; set; }
        public bool IsMinusAllowed { get; set; }
        //List Variables
        private int lastID = 0;
        public ObservableCollection<Transaction> LastTransactionsList { get; set; }
        //Main Variables
        public string UrlPathSegment => Strings["menu_main"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        public MainViewModel(ITransactionService transactionService, IMoneyService moneyService, ISettingsService settingsService, IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _settingsService = settingsService;
            _transactionService = transactionService;
            _moneyService = moneyService;
            CreateCommands();
            GetData();
        }
        #region Methods
        private void CreateCommands()
        {
            //Reactive Example to navigate
            NavigateToDummyPage = ReactiveCommand
                .CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
            SelectNote = ReactiveCommand.Create<Transaction>(async note =>
            {
                //await PopupNavigation.Instance.PushAsync(new DiagramPopupView(Strings["alert_no_cash_delete"]), true);
                var result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure to delete this note?", "Yes", "No");
                if (result)
                {
                    if (note.Date.Month != DateTime.Now.Month && note.Date.Year != DateTime.Now.Year)
                    {

                    }
                    if (note.Bill == TransactionBill.Cash)
                    {
                        if (CurrentCash < note.Value)
                        {
                            await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_cash_delete"]), true);
                            return;
                        }
                    }
                    else
                    {
                        if (CurrentCard < note.Value)
                        {
                            await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_card_delete"]), true);
                            return;
                        }
                    }
                    if (await _transactionService.Delete(note.Id))
                    {
                        LastTransactionsList.Remove(note);
                        switch (note.MathSymbol)
                        {
                            case '+':
                                if (note.Bill == TransactionBill.Cash)
                                {
                                    _moneyService.SetCurrentCash(CurrentCash - note.Value);
                                    if (note.Type == TransactionType.Bank) _moneyService.SetCurrentCredit(CurrentCash - note.Value);
                                }
                                else
                                {
                                    _moneyService.SetCurrentCard(CurrentCard - note.Value);
                                    if (note.Type == TransactionType.Bank) _moneyService.SetCurrentCredit(CurrentCard - note.Value);
                                }
                                break;
                            case '-':
                                if (note.Bill == TransactionBill.Cash)
                                {
                                    _moneyService.SetCurrentCash(CurrentCash + note.Value);
                                    if (note.Type == TransactionType.Bank) _moneyService.SetCurrentCredit(CurrentCash + note.Value);
                                }
                                else
                                {
                                    _moneyService.SetCurrentCard(CurrentCard + note.Value);
                                    if (note.Type == TransactionType.Bank) _moneyService.SetCurrentCredit(CurrentCard + note.Value);
                                }
                                break;
                            default:
                                break;
                        }
                        GetData();
                    }
                }
            });
            AddSpend = ReactiveCommand.Create(() => { OnSpend(); });
            AddSalary = ReactiveCommand.Create(() => { OnAddSalary(); });
            BankCommand = ReactiveCommand.Create(() => { OnBankCommand(); });
            SaveCommand = ReactiveCommand.Create(() => { OnSaveCommand(); });
        }
        private void GetData()
        {
            GetLastTransactions();
            CurrentCard = _moneyService.GetCurrentCard();
            CurrentCash = _moneyService.GetCurrentCash();
            CurrentCredit = _moneyService.GetCurrentCredit();
            CurrentAllSavings = _moneyService.GetAllSavings();
            if (_settingsService.GetBalanceSettings())
                CurrentBill = CurrentCard + CurrentCash + CurrentAllSavings;
            else
                CurrentBill = CurrentCard + CurrentCash;
            IsCreditVisible = CurrentCredit > 0 ? true : false;
            IsMinusAllowed = _settingsService.GetAutoCreditSettings();
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
            else if (SpendValue == "0" || SpendValue == "0.")
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
            }
            else if (SpendValue[0] == '0' && SpendValue.Length > 1 && SpendValue[1] != '.')
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
            }
            else if (SpendValue[0] == '.')
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
            }
            else if (SpendValue[0] == '-')
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_minus"]), true);
            }
            else await PopupNavigation.Instance.PushAsync(new CommitPopupView(OnSpendFunc), true);
        }
        private async void OnSpendFunc()
        {
            SpendDescription = CrossSettings.Current.GetValueOrDefault("CommitMessage", "");
            SelectedCategory = UnwrapSpendingCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedSpendingCategory", 0));
            var item = new Transaction
            {
                Id = ++lastID,
                Value = decimal.Parse(SpendValue),
                Note = SpendDescription,
                Date = DateTime.Now,
                Type = TransactionType.Spend,
                Category = SelectedCategory,
                MathSymbol = '-'
            };
            switch (CrossSettings.Current.GetValueOrDefault("CurrentCommitMoneyFrom", 0))
            {
                case 0:
                    if (item.Value > CurrentCash)
                    {
                        if (!IsMinusAllowed)
                        {
                            await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_cash"]), true);
                            return;
                        }
                        else
                        {
                            CurrentCredit += item.Value;
                            _moneyService.SetCurrentCredit(CurrentCredit);
                        }
                    }
                    else
                    {
                        CurrentCash -= item.Value;
                        _moneyService.SetCurrentCash(CurrentCash);
                    }
                    item.Bill = TransactionBill.Cash;
                    break;
                case 1:
                    if (item.Value > CurrentCard)
                    {
                        if (!IsMinusAllowed)
                        {
                            await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_card"]), true);
                            return;
                        }
                        else
                        {
                            CurrentCredit += item.Value;
                            _moneyService.SetCurrentCredit(CurrentCredit);
                        }
                    }
                    else
                    {
                        CurrentCard -= item.Value;
                        _moneyService.SetCurrentCard(CurrentCard);
                    }
                    item.Bill = TransactionBill.Card;
                    break;
            }
            _ = _transactionService.Create(item);
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
                Id = ++lastID,
                Value = AddMoneyValue,
                Note = AddMoneyDescription,
                Date = DateTime.Now,
                Type = TransactionType.Add,
                Category = SelectedCategory,
                MathSymbol = '+'
            };
            switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
            {
                case 0:
                    CurrentCash += item.Value;
                    item.Bill = TransactionBill.Cash;
                    _moneyService.SetCurrentCash(CurrentCash);
                    break;
                case 1:
                    CurrentCard += item.Value;
                    item.Bill = TransactionBill.Card;
                    _moneyService.SetCurrentCard(CurrentCard);
                    break;
            }
            _ = _transactionService.Create(item);
            LastTransactionsList.Clear();
            GetData();
        }
        private async void OnBankCommand()
        {
            await PopupNavigation.Instance.PushAsync(new BankTransactionsPopupView(OnBankFunc), true);
        }
        private async void OnSaveCommand()
        {
            await PopupNavigation.Instance.PushAsync(new SaveMoneyPopupView(OnSaveFunc), true);
        }
        private async void OnBankFunc()
        {
            CreditDescription = CrossSettings.Current.GetValueOrDefault("CreditMessage", "");
            CreditValue = CrossSettings.Current.GetValueOrDefault("CreditValue", 0.0m);
            SelectedCategory = UnwrapBankCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedBankCategory", 0));
            switch (SelectedCategory.Type)
            {
                case CategoryType.Lend:
                    var transaction = new Transaction
                    {
                        Id = ++lastID,
                        Value = CreditValue,
                        Note = CreditDescription,
                        Date = DateTime.Now,
                        Type = TransactionType.Bank,
                        Category = SelectedCategory,
                        MathSymbol = '+'
                    };
                    switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
                    {
                        case 0:
                            CurrentCash += transaction.Value;
                            CurrentCredit += transaction.Value;
                            _moneyService.SetCurrentCash(CurrentCash);
                            _moneyService.SetCurrentCredit(CurrentCredit);
                            transaction.Bill = TransactionBill.Cash;
                            break;
                        case 1:
                            CurrentCard += transaction.Value;
                            CurrentCredit += transaction.Value;
                            _moneyService.SetCurrentCard(CurrentCard);
                            _moneyService.SetCurrentCredit(CurrentCredit);
                            transaction.Bill = TransactionBill.Card;
                            break;
                    }
                    _ = _transactionService.Create(transaction);
                    break;
                case CategoryType.Repay:
                    if (CurrentCredit <= 0)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_credit"]), true);
                        return;
                    }
                    var item = new Transaction
                    {
                        Id = ++lastID,
                        Value = CreditValue,
                        Note = CreditDescription,
                        Date = DateTime.Now,
                        Type = TransactionType.Bank,
                        Category = SelectedCategory,
                        MathSymbol = '-'
                    };
                    switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
                    {
                        case 0:
                            if (CreditValue > CurrentCash)
                            {
                                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_cash_bank"]), true);
                                return;
                            }
                            if (CreditValue > CurrentCredit) item.Value = CurrentCredit;
                            CurrentCash -= item.Value;
                            CurrentCredit -= item.Value;
                            _moneyService.SetCurrentCash(CurrentCash);
                            _moneyService.SetCurrentCredit(CurrentCredit);
                            item.Bill = TransactionBill.Cash;
                            break;
                        case 1:
                            if (CreditValue > CurrentCard)
                            {
                                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_card_bank"]), true);
                                return;
                            }
                            if (CreditValue > CurrentCredit) item.Value = CurrentCredit;
                            CurrentCard -= item.Value;
                            CurrentCredit -= item.Value;
                            _moneyService.SetCurrentCard(CurrentCard);
                            _moneyService.SetCurrentCredit(CurrentCredit);
                            item.Bill = TransactionBill.Card;
                            break;
                    }
                    _ = _transactionService.Create(item);
                    break;
                default:
                    break;
            }
            LastTransactionsList.Clear();
            GetData();
        }
        private async void OnSaveFunc()
        {
            SaveDescription = CrossSettings.Current.GetValueOrDefault("SaveMessage", "");
            SaveValue = CrossSettings.Current.GetValueOrDefault("SaveValue", 0.0m);
            SelectedCategory = UnwrapSaveCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedSaveCategory", 0));
            switch (SelectedCategory.Type)
            {
                case CategoryType.Save:
                    var transaction = new Transaction
                    {
                        Id = ++lastID,
                        Value = SaveValue,
                        Note = SaveDescription,
                        Date = DateTime.Now,
                        Type = TransactionType.Save,
                        Category = SelectedCategory,
                        MathSymbol = '-'
                    };
                    switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
                    {
                        case 0:
                            if (SaveValue > CurrentCash)
                            {
                                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_cash_save"]), true);
                                return;
                            }
                            CurrentCash -= transaction.Value;
                            CurrentAllSavings += transaction.Value;
                            _moneyService.SetCurrentCash(CurrentCash);
                            _moneyService.SetAllSavings(CurrentAllSavings);
                            transaction.Bill = TransactionBill.Cash;
                            break;
                        case 1:
                            if (SaveValue > CurrentCard)
                            {
                                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_card_save"]), true);
                                return;
                            }
                            CurrentCard -= transaction.Value;
                            CurrentAllSavings += transaction.Value;
                            _moneyService.SetCurrentCard(CurrentCard);
                            _moneyService.SetAllSavings(CurrentAllSavings);
                            transaction.Bill = TransactionBill.Card;
                            break;
                    }
                    _ = _transactionService.Create(transaction);
                    break;
                case CategoryType.Take:
                    if (SaveValue > CurrentAllSavings)
                    {
                        await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_savings"]), true);
                        return;
                    }
                    var item = new Transaction
                    {
                        Id = ++lastID,
                        Value = SaveValue,
                        Note = SaveDescription,
                        Date = DateTime.Now,
                        Type = TransactionType.Save,
                        Category = SelectedCategory,
                        MathSymbol = '+'
                    };
                    switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
                    {
                        case 0:
                            CurrentCash += item.Value;
                            CurrentAllSavings -= item.Value;
                            _moneyService.SetCurrentCash(CurrentCash);
                            _moneyService.SetAllSavings(CurrentAllSavings);
                            item.Bill = TransactionBill.Cash;
                            break;
                        case 1:
                            CurrentCard += item.Value;
                            CurrentAllSavings -= item.Value;
                            _moneyService.SetCurrentCard(CurrentCard);
                            _moneyService.SetAllSavings(CurrentAllSavings);
                            item.Bill = TransactionBill.Card;
                            break;
                    }
                    _ = _transactionService.Create(item);
                    break;
                default:
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
        private CategoryDto UnwrapBankCategoryType(int id)
        {
            foreach (var item in Categories.GetAllBankCategories())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return new CategoryDto();
        }
        private CategoryDto UnwrapSaveCategoryType(int id)
        {
            foreach (var item in Categories.GetAllSaveCategories())
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
