using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using I18NPortable;
using ReactiveUI;
using Splat;
using Xamarin.Essentials;

namespace MoneyNote
{
    public class MasterViewModel : ReactiveObject, IScreen
    {
        public II18N Strings => I18N.Current;
        public ICommand ShareCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public MasterViewModel()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            MenuItems = GetMenuItems();

            NavigateToMenuItem = ReactiveCommand.CreateFromObservable<IRoutableViewModel, Unit>(
                routableVm => Router.NavigateAndReset.Execute(routableVm).Select(_ => Unit.Default));

            this.WhenAnyValue(x => x.Selected)
                .Where(x => x != null)
                .StartWith(MenuItems.First())
                .Select(x => Locator.Current.GetService<IRoutableViewModel>(x.TargetType.FullName))
                .InvokeCommand(NavigateToMenuItem);

            ShareCommand = ReactiveCommand.Create(async () =>
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Uri = "https://curaciov.com"
                });
            });
            LikeCommand = ReactiveCommand.Create(() =>
            {
                Launcher.OpenAsync(new Uri(@"https://curaciov.com"));
            });
        }

        private MasterCellViewModel _selected;
        public MasterCellViewModel Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }

        public ReactiveCommand<IRoutableViewModel, Unit> NavigateToMenuItem { get; }

        public IEnumerable<MasterCellViewModel> MenuItems { get; }

        public RoutingState Router { get; }

        private IEnumerable<MasterCellViewModel> GetMenuItems()
        {
            return new[]
            {
                new MasterCellViewModel { Title = Strings["menu_main"], IconSource = "main.png", TargetType = typeof(MainViewModel) },
                new MasterCellViewModel { Title = Strings["menu_account"], IconSource = "account.png", TargetType = typeof(AccountViewModel) },
                new MasterCellViewModel { Title = Strings["menu_settings"], IconSource = "settings.png", TargetType = typeof(SettingsViewModel) },
                new MasterCellViewModel { Title = Strings["menu_history"], IconSource = "history.png", TargetType = typeof(HistoryViewModel) },
                //new MasterCellViewModel { Title = Strings["menu_terms"], IconSource = "terms.png", TargetType = typeof(TermsViewModel) },
                new MasterCellViewModel { Title = Strings["menu_about"], IconSource = "about.png", TargetType = typeof(AboutViewModel) },
            };
        }
    }
}
