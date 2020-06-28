using I18NPortable;
using ReactiveUI;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyNote
{
    public class AccountViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public AccountViewModel()
        {

            MyCash = "9999999";
            MyCashCommand = ReactiveCommand.Create(async () =>
            {
                //Application.Current.MainPage.DisplayAlert("Alert", "Hello", "Cancel", "ok");
                string summ = await Application.Current.MainPage.DisplayPromptAsync("Change My Cash Manually:", "Be carrefull, this function will delete current cash record");
                if (!string.IsNullOrEmpty(summ))
                {
                    MyCash = summ;

                }
            });

            //NumberStream = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Select(x => x.ToString());
        }

        //public IObservable<string> NumberStream { get; }

        public string UrlPathSegment => Strings["menu_account"];
        public ICommand MyCashCommand { get; set; }
        public string MyCash { get; set; }
        public IScreen HostScreen { get; }
    }
}
