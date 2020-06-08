using ReactiveUI;

namespace MoneyNote
{
    public class HistoryViewModel : ReactiveObject, IRoutableViewModel
    {
        public HistoryViewModel()
        {

        }

        public string UrlPathSegment => "History";
        public IScreen HostScreen { get; }
    }
}
