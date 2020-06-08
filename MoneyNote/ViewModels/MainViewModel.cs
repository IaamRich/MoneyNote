using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Linq;

namespace MoneyNote
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
        }

        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        public string UrlPathSegment => "Main";

        public IScreen HostScreen { get; }
    }
}
