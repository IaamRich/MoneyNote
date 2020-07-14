using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services;
using ReactiveUI;
using Splat;

namespace MoneyNote
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {

        private static SpendService spendService;

        private SourceList<Spend> _spends = new SourceList<Spend>();
        private ReadOnlyObservableCollection<Spend> _spendingList;
        public ReadOnlyObservableCollection<Spend> SpendingList => _spendingList;
        public II18N Strings => I18N.Current;
        public MainViewModel(IScreen hostScreen = null)
        {
            spendService = new SpendService();
            GetData();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
        }

        public async void GetData()
        {
            List<Spend> data = new List<Spend>();
            //await Task.Run(() =>
            //{
            data = spendService.GetAll().Result;
            data.Reverse();
            _spends.AddRange(data);
            _spends.Connect().Bind(out _spendingList).Subscribe();
            //});
            //foreach (var item in SpendingList)
            //{
            //    item.Amount
            //}
        }
        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        public string UrlPathSegment => Strings["menu_main"];

        public IScreen HostScreen { get; }
    }
}
