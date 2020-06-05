using ReactiveUI;
using Splat;

namespace MoneyNote.ViewModels
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Main";

        public IScreen HostScreen { get; }
        public MainViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

        }
    }
}
