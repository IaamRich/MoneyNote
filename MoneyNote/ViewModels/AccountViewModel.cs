using System;
using System.Threading.Tasks;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;
using MoneyNote.Views.Popups;
using Plugin.Settings;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Splat;
using Xamarin.Forms;

namespace MoneyNote
{
    public class AccountViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static ITransactionService _transactionService;
        private static IMoneyService _moneyService;
        //Main variables
        public II18N Strings => I18N.Current;
        public string UrlPathSegment => Strings["menu_account"];
        private string _message;

        public IScreen HostScreen { get; }
        public ICommand MyCashCommand { get; set; }
        public ICommand MyCardCommand { get; set; }
        public ICommand MyAllIncomeCommand { get; set; }
        public ICommand MyAllOutlayCommand { get; set; }
        public ICommand MyAllSavingsCommand { get; set; }
        public ICommand MyIncomeCommand { get; set; }
        public ICommand MyOutlayCommand { get; set; }
        public ICommand MySavingsCommand { get; set; }
        public decimal MyCash { get; set; }
        public decimal MyCard { get; set; }
        public decimal MyCurrent { get; set; }
        public decimal MyIncome { get; set; }
        public decimal MyOutlay { get; set; }
        public decimal MyAllIncome { get; set; }
        public decimal MyAllOutlay { get; set; }
        public decimal MySavings { get; set; }
        public decimal MyAllSavings { get; set; }
        public AccountViewModel(ITransactionService transactionService, IMoneyService moneyService, string message = null, IScreen screen = null)
        {
            _message = message;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            if (!string.IsNullOrEmpty(_message)) Application.Current.MainPage.DisplayAlert("Message", message, "", "ok"); ;
            _transactionService = transactionService;
            _moneyService = moneyService;

            _ = GetDataAsync();
            CreateCommands();
        }
        private void CreateCommands()
        {
            MyCashCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Cash Manually:", "Be carrefull, this function will delete current cash record");
                if (CheckStringForValue(summ))
                {
                    var oldcash = MyCash;
                    MyCash = decimal.Parse(summ);
                    _moneyService.SetCurrentCash(MyCash);
                    MyCurrent = MyCurrent - oldcash + MyCash;
                }
            });
            MyCardCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Card Manually:", "Be carrefull, this function will delete current card record");
                if (CheckStringForValue(summ))
                {
                    var oldcard = MyCard;
                    MyCard = decimal.Parse(summ);
                    _moneyService.SetCurrentCard(MyCard);
                    MyCurrent = MyCurrent - oldcard + MyCard;
                }
            });
            MyAllIncomeCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My All Income Manually:", "Be carrefull, this function will delete all income record");
                if (CheckStringForValue(summ))
                {
                    MyAllIncome = decimal.Parse(summ);
                    _moneyService.SetAllIncome(MyAllIncome);
                }
            });
            MyAllOutlayCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My All Outlay Manually:", "Be carrefull, this function will delete all outlay record");
                if (CheckStringForValue(summ))
                {
                    MyAllOutlay = decimal.Parse(summ);
                    _moneyService.SetAllOutlay(MyAllOutlay);
                }
            });
            MyAllSavingsCommand = ReactiveCommand.Create(async () =>
            {//need to update MyAdditinalSavings
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change All Savings Manually:", "Be carrefull, this function will delete all savings record");
                if (CheckStringForValue(summ))
                {
                    MyAllSavings = decimal.Parse(summ);
                    _moneyService.SetAllSavings(MyAllSavings);
                }
            });
            MyIncomeCommand = ReactiveCommand.Create(async () =>
            {
                //string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Current Income Manually:", "Be carrefull, this function will delete current income record");
                //if (CheckStringForValue(summ))
                //{
                //    MyIncome = decimal.Parse(summ);
                //    _moneyService.SetCurrentIncome(MyIncome);
                //}
            });
            MyOutlayCommand = ReactiveCommand.Create(async () =>
            {
                //string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Current Outlay Manually:", "Be carrefull, this function will delete current outlay record");
                //if (CheckStringForValue(summ))
                //{
                //    MyOutlay = decimal.Parse(summ);
                //    _moneyService.SetCurrentOutlay(MyOutlay);
                //}
            });
            MySavingsCommand = ReactiveCommand.Create(async () =>
            {
                //await PopupNavigation.Instance.PushAsync(new AccountChangePopupView(OnMySavingsFunc, Strings["title_current_savings"], Strings["alert_current_savings"]), true);
            });
        }
        private void OnMySavingsFunc()
        {
            var result = CrossSettings.Current.GetValueOrDefault("CurrentAccountPopupValue", "");
            if (CheckStringForValue(result))
            {
                MySavings = decimal.Parse(result);
                //_moneyService.SetCurrentSavings(MySavings);
            }
        }
        private async Task GetDataAsync()
        {
            MyCash = _moneyService.GetCurrentCash();
            MyCard = _moneyService.GetCurrentCard();
            MyCurrent = MyCash + MyCard;

            MyAllSavings = _moneyService.GetAdditionalSavings();

            var date = DateTime.Now;
            var data = await _transactionService.GetAll();
            if (data != null && data?.Count > 0)
            {
                foreach (var note in data)
                {
                    switch (note.Type)
                    {
                        case TransactionType.Spend:
                            MyAllOutlay += note.Value;
                            if (note.Date.Month == date.Month && note.Date.Year == date.Year)
                            {
                                MyOutlay += note.Value;
                            }
                            break;
                        case TransactionType.Add:
                            MyAllIncome += note.Value;
                            if (note.Date.Month == date.Month && note.Date.Year == date.Year)
                            {
                                MyIncome += note.Value;
                            }
                            break;
                        case TransactionType.Save:
                            if (note.MathSymbol == '"')
                            {
                                MyAllSavings += note.Value;
                                if (note.Date.Month == date.Month && note.Date.Year == date.Year)
                                {
                                    MySavings += note.Value;
                                }
                            }
                            else
                            {
                                MyAllSavings -= note.Value;
                                if (note.Date.Month == date.Month && note.Date.Year == date.Year)
                                {
                                    MySavings -= note.Value;
                                }
                            }
                            break;
                        case TransactionType.None:
                        case TransactionType.Bank:
                        default:
                            break;
                    }
                }
            }
        }
        private bool CheckStringForValue(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
                return false;
            }
            else if (str[0] == '0')
            {
                PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
                return false;
            }
            else if (str[0] == '.')
            {
                PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
                return false;
            }
            return true;
        }
    }
}
