using I18NPortable;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Linq;

namespace MoneyNote
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public MainViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
        }

        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        public string UrlPathSegment => Strings["menu_main"];

        public IScreen HostScreen { get; }
    }
}
