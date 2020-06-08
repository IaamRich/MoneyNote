using ReactiveUI;

namespace MoneyNote
{
    public class TermsViewModel : ReactiveObject, IRoutableViewModel
    {
        public TermsViewModel()
        {

        }

        public string UrlPathSegment => "Terms";
        public IScreen HostScreen { get; }
    }
}
