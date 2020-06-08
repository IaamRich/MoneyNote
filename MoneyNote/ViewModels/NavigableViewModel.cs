using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Linq;

namespace MoneyNote
{
    public class NavigableViewModel : ReactiveObject, IRoutableViewModel
    {
        public NavigableViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
        }

        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        public string UrlPathSegment => "Navigable Page";

        public IScreen HostScreen { get; }
    }
}
