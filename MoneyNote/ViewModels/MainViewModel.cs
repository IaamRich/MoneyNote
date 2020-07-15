using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
        //Commands
        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; set; }
        public ReactiveCommand<Unit, Unit> AddSpend { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteSpend { get; set; }
        public ReactiveCommand<Unit, Unit> UpdateSpend { get; set; }
        //Used Services
        private static SpendService spendService;
        //UI variables
        private string SpendDescription { get; set; }
        public int SpendValue { get; set; }
        //List Variables
        private SourceList<Spend> _spends = new SourceList<Spend>();
        private ReadOnlyObservableCollection<Spend> _spendingList;
        public ReadOnlyObservableCollection<Spend> SpendingList => _spendingList;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_main"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        public MainViewModel(IScreen hostScreen = null)
        {
            spendService = new SpendService();
            //Filling MainPage List
            GetData();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            //Reactive Example to navigate
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
            //Reactive Command
            AddSpend = ReactiveCommand.Create(() => { OnAdd(); });
        }
        private async void GetData()
        {
            List<Spend> data = new List<Spend>();
            await Task.Run(() =>
            {
                data = spendService.GetAll().Result;
                data.Reverse();
                _spends.AddRange(data);
                _spends.Connect().Bind(out _spendingList).Subscribe();
            });
        }
        public async void OnAdd()
        {
            try
            {
                SpendDescription = await App.Current.MainPage.DisplayPromptAsync("Write description:", "You can skip this...");

                Spend item = new Spend
                {
                    Amount = SpendValue,
                    WhereText = SpendDescription
                };
                await Task.Run(async () =>
                {
                    await spendService.SaveItemAsync(item);
                    _spends.Clear();

                    GetData();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("========== ERRORHERE =============:" + ex + "=========== ENDERROR==================");
            }
        }
    }
}
