using System.Windows.Input;
using I18NPortable;
using MoneyNote.Services;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace MoneyNote
{
    public class AccountViewModel : ReactiveObject, IRoutableViewModel
    {

        private static MoneyService moneyService;
        public II18N Strings => I18N.Current;
        public string UrlPathSegment => Strings["menu_account"];
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
        public AccountViewModel(string message = null, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            if (!string.IsNullOrEmpty(message)) ShowMessage(message);
            moneyService = new MoneyService();

            GetData();
            MyCashCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Cash Manually:", "Be carrefull, this function will delete current cash record");
                if (!string.IsNullOrEmpty(summ))
                {
                    var oldcash = MyCash;
                    MyCash = decimal.Parse(summ);
                    moneyService.SetCurrentCash(MyCash);
                    MyCurrent = MyCurrent - oldcash + MyCash;
                }
            });
            MyCardCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Card Manually:", "Be carrefull, this function will delete current card record");
                if (!string.IsNullOrEmpty(summ))
                {
                    var oldcard = MyCard;
                    MyCard = decimal.Parse(summ);
                    moneyService.SetCurrentCard(MyCard);
                    MyCurrent = MyCurrent - oldcard + MyCard;
                }
            });
            MyAllIncomeCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Income Manually:", "Be carrefull, this function will delete current income record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyAllIncome = decimal.Parse(summ);
                    moneyService.SetAllIncome(MyAllIncome);
                }
            });
            MyAllOutlayCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Outlay Manually:", "Be carrefull, this function will delete current outlay record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyAllOutlay = decimal.Parse(summ);
                    moneyService.SetAllOutlay(MyAllOutlay);
                }
            });
            MyAllSavingsCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change All Savings Manually:", "Be carrefull, this function will delete current all savings record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyAllSavings = decimal.Parse(summ);
                    moneyService.SetAllSavings(MyAllSavings);
                }
            });
            MyIncomeCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Income Manually:", "Be carrefull, this function will delete current income record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyIncome = decimal.Parse(summ);
                    moneyService.SetAllIncome(MyIncome);
                }
            });
            MyOutlayCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Outlay Manually:", "Be carrefull, this function will delete current outlay record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyOutlay = decimal.Parse(summ);
                    moneyService.SetAllOutlay(MyOutlay);
                }
            });
            MySavingsCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change All Savings Manually:", "Be carrefull, this function will delete current all savings record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MySavings = decimal.Parse(summ);
                    moneyService.SetAllSavings(MyAllSavings);
                }
            });
        }

        private void GetData()
        {
            MyCash = moneyService.GetCurrentCash();
            MyCard = moneyService.GetCurrentCard();
            MyCurrent = MyCash + MyCard;
            MyIncome = moneyService.GetCurrentIncome();
            MyAllIncome = moneyService.GetAllIncome();
            MyOutlay = moneyService.GetCurrentOutlay();
            MyAllOutlay = moneyService.GetAllOutlay();
            MySavings = moneyService.GetCurrentSavings();
            MyAllSavings = moneyService.GetAllSavings();
        }

        private void ShowMessage(string message)
        {
            Application.Current.MainPage.DisplayAlert("Message", message, "", "ok");
        }
    }
}
