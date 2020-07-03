using I18NPortable;
using ReactiveUI;

namespace MoneyNote
{
    public class TermsViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public TermsViewModel()
        {

        }

        public string UrlPathSegment => Strings["menu_terms"];
        public IScreen HostScreen { get; }
    }
}
