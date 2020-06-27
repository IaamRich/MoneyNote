using I18NPortable;
using ReactiveUI;

namespace MoneyNote
{
    public class HistoryViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public HistoryViewModel()
        {

        }

        public string UrlPathSegment => Strings["menu_history"];
        public IScreen HostScreen { get; }
    }
}
