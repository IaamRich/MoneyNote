using ReactiveUI;
using Splat;

namespace MoneyNote.ViewModels
{
    public class MenuViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Main";
        public IScreen HostScreen { get; }
        public MenuViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
