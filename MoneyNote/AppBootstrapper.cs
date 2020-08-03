//using MoneyNote.Views;
using MoneyNote.Services;
using ReactiveUI;
using Splat;

namespace MoneyNote
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            RegisterServices();
            RegisterViews();
            RegisterViewModels();

            //Router.Navigate.Execute(new MasterViewModel());
        }

        public RoutingState Router { get; }

        private void RegisterServices()
        {
            //Splat.Locator.CurrentMutable.Register(() => new MoneyService(), typeof(IMoneyService));
            //Splat.Locator.CurrentMutable.Register(() => new SettingsService(), typeof(ISettingsService));
            //Splat.Locator.CurrentMutable.Register(() => new SpendService(), typeof(ISpendService));
        }
        public MasterViewModel CreateMasterViewModel()
        {
            // In a typical routing example the IScreen implementation would be this bootstrapper class.
            // However, a MasterDetailPage is designed to at the root. So, we assign the master-detail
            // view model to play the part of IScreen, instead.
            var viewModel = new MasterViewModel();

            return viewModel;
        }
        private void RegisterViews()
        {
            //MainMenu
            Locator.CurrentMutable.Register(() => new MasterCell(), typeof(IViewFor<MasterCellViewModel>));

            // Detail pages
            Locator.CurrentMutable.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() => new AccountView(), typeof(IViewFor<AccountViewModel>));
            Locator.CurrentMutable.Register(() => new SettingsView(), typeof(IViewFor<SettingsViewModel>));
            Locator.CurrentMutable.Register(() => new HistoryView(), typeof(IViewFor<HistoryViewModel>));
            Locator.CurrentMutable.Register(() => new TermsView(), typeof(IViewFor<TermsViewModel>));
            Locator.CurrentMutable.Register(() => new AboutView(), typeof(IViewFor<AboutViewModel>));

            //Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        }
        private void RegisterViewModels()
        {
            // Here, we use contracts to distinguish which routable view model we want to instantiate.
            // This helps us avoid a manual cast to IRoutableViewModel when calling Router.Navigate.Execute(...)
            Locator.CurrentMutable.Register(() => new MainViewModel(new SpendService(), new MoneyService()), typeof(IRoutableViewModel), typeof(MainViewModel).FullName);
            Locator.CurrentMutable.Register(() => new AccountViewModel(new MoneyService()), typeof(IRoutableViewModel), typeof(AccountViewModel).FullName);
            Locator.CurrentMutable.Register(() => new SettingsViewModel(new SpendService(), new MoneyService(), new SettingsService()), typeof(IRoutableViewModel), typeof(SettingsViewModel).FullName);
            Locator.CurrentMutable.Register(() => new HistoryViewModel(new SpendService()), typeof(IRoutableViewModel), typeof(HistoryViewModel).FullName);
            Locator.CurrentMutable.Register(() => new TermsViewModel(), typeof(IRoutableViewModel), typeof(TermsViewModel).FullName);
            Locator.CurrentMutable.Register(() => new AboutViewModel(), typeof(IRoutableViewModel), typeof(AboutViewModel).FullName);
        }
        //public Page CreateMainPage()
        //{
        //    return new RoutedViewHost();
        //}
    }
}
