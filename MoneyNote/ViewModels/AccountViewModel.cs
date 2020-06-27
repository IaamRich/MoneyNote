using I18NPortable;
using ReactiveUI;

namespace MoneyNote
{
    public class AccountViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public AccountViewModel()
        {
            //NumberStream = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Select(x => x.ToString());
        }

        //public IObservable<string> NumberStream { get; }

        public string UrlPathSegment => Strings["menu_account"];

        public IScreen HostScreen { get; }
    }
}
