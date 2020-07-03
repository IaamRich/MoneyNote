using I18NPortable;
using ReactiveUI;

namespace MoneyNote
{
    public class AboutViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public AboutViewModel()
        {

        }

        public string UrlPathSegment => Strings["menu_about"];
        public IScreen HostScreen { get; }
    }
}
