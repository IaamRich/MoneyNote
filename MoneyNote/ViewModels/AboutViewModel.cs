using ReactiveUI;

namespace MoneyNote
{
    public class AboutViewModel : ReactiveObject, IRoutableViewModel
    {
        public AboutViewModel()
        {

        }

        public string UrlPathSegment => "About";
        public IScreen HostScreen { get; }
    }
}
