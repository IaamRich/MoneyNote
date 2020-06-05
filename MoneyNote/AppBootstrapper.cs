using MoneyNote.Models;
using MoneyNote.Pages;
using MoneyNote.Services;
using MoneyNote.ViewModels;
using MoneyNote.Views;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

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
            Router.Navigate.Execute(new MainViewModel());
        }

        public RoutingState Router { get; }

        private void RegisterServices()
        {
            Splat.Locator.CurrentMutable.Register(() => new StaticContactsService(), typeof(IContactServices));
        }

        private void RegisterViews()
        {
            Locator.CurrentMutable.Register(() => new FirstPage(), typeof(IViewFor<FirstViewModel>));
            Locator.CurrentMutable.Register(() => new SecondPage(), typeof(IViewFor<SecondViewModel>));
            Locator.CurrentMutable.Register(() => new CollectionPage(), typeof(IViewFor<CollectionViewModel>));
            Locator.CurrentMutable.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));
            //Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        }
        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}
