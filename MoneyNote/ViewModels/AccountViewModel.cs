using ReactiveUI;

namespace MoneyNote
{
    public class AccountViewModel : ReactiveObject, IRoutableViewModel
    {
        public AccountViewModel()
        {
            //NumberStream = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Select(x => x.ToString());
        }

        //public IObservable<string> NumberStream { get; }

        public string UrlPathSegment => "Account";

        public IScreen HostScreen { get; }
    }
}
