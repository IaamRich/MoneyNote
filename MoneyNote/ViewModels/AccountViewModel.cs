using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
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
        public ICommand MyCurrentCommand { get; set; }
        public ICommand MyIncomeCommand { get; set; }
        public ICommand MyOutlayCommand { get; set; }
        public ICommand MySavingsCommand { get; set; }
        public ICommand MyAllSavingsCommand { get; set; }
        public decimal MyCash { get; set; }
        public string MyCard { get; set; }
        public string MyCurrent { get; set; }
        public string MyIncome { get; set; }
        public string MyOutlay { get; set; }
        public string MySavings { get; set; }
        public string MyAllSavings { get; set; }
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
                    MyCash = decimal.Parse(summ);

                    var cash = new AllMoney { MyCahsMoney = System.Int32.Parse(summ) };
                    moneyService.UpdateAllMoneyAsync(cash);
                }
            });
            MyCardCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Card Manually:", "Be carrefull, this function will delete current card record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyCard = summ;

                }
            });
            MyCurrentCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Current Manually:", "Be carrefull, this function will delete current money record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyCurrent = summ;

                }
            });
            MyIncomeCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Income Manually:", "Be carrefull, this function will delete current income record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyIncome = summ;

                }
            });
            MyOutlayCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Outlay Manually:", "Be carrefull, this function will delete current outlay record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyOutlay = summ;

                }
            });
            MySavingsCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change This Month Savings Manually:", "Be carrefull, this function will delete current this month savings record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MySavings = summ;

                }
            });
            MyAllSavingsCommand = ReactiveCommand.Create(async () =>
            {
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change All Savings Manually:", "Be carrefull, this function will delete current all savings record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyAllSavings = summ;

                }
            });
        }

        private void GetData()
        {
            MyCash = moneyService.GetCurrentBill().Result.MyCahsMoney;
            MyCard = "9972599";
            MyCurrent = "9991629";
            MyIncome = "9998769";
            MyOutlay = "9993239";
            MySavings = "8769999";
            MyAllSavings = "9112999";
        }

        private void ShowMessage(string message)
        {
            Application.Current.MainPage.DisplayAlert("Message", message, "", "ok");
        }
    }
}
