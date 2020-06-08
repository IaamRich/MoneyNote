using MoneyNote.Services;
//using MoneyNote.Views;
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
            //Router.Navigate.Execute(new MainViewModel());
        }

        public RoutingState Router { get; }

        private void RegisterServices()
        {
            Splat.Locator.CurrentMutable.Register(() => new StaticContactsService(), typeof(IContactServices));
        }
        public MainViewModel CreateMainViewModel()
        {
            // In a typical routing example the IScreen implementation would be this bootstrapper class.
            // However, a MasterDetailPage is designed to at the root. So, we assign the master-detail
            // view model to play the part of IScreen, instead.
            var viewModel = new MainViewModel();

            return viewModel;
        }
        private void RegisterViews()
        {
            //MainMenu
            Locator.CurrentMutable.Register(() => new MasterCell(), typeof(IViewFor<MasterCellViewModel>));

            // Detail pages
            Locator.CurrentMutable.Register(() => new DummyPage(), typeof(IViewFor<DummyViewModel>));
            Locator.CurrentMutable.Register(() => new NavigablePage(), typeof(IViewFor<NavigableViewModel>));
            Locator.CurrentMutable.Register(() => new NumberStreamPage(), typeof(IViewFor<NumberStreamViewModel>));
            Locator.CurrentMutable.Register(() => new LetterStreamPage(), typeof(IViewFor<LetterStreamViewModel>));
        }
        private void RegisterViewModels()
        {
            // Here, we use contracts to distinguish which routable view model we want to instantiate.
            // This helps us avoid a manual cast to IRoutableViewModel when calling Router.Navigate.Execute(...)
            Locator.CurrentMutable.Register(() => new NavigableViewModel(), typeof(IRoutableViewModel), typeof(NavigableViewModel).FullName);
            Locator.CurrentMutable.Register(() => new NumberStreamViewModel(), typeof(IRoutableViewModel), typeof(NumberStreamViewModel).FullName);
            Locator.CurrentMutable.Register(() => new LetterStreamViewModel(), typeof(IRoutableViewModel), typeof(LetterStreamViewModel).FullName);
        }
        //public Page CreateMainPage()
        //{
        //    return new RoutedViewHost();
        //}
    }
}
