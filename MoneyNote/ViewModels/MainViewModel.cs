using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ICommand NavigateToDummyPage { get; set; }
        public ICommand AddSpend { get; set; }
        public ICommand DeleteSpend { get; set; }
        public ICommand UpdateSpend { get; set; }
        public ICommand AddSalary { get; set; }
        //Used Services
        private static SpendService spendService;
        private static MoneyService moneyService;
        //UI variables
        private string SpendDescription { get; set; }
        public string SpendValue { get; set; }
        public decimal CurrentBill { get; set; }
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
            moneyService = new MoneyService();
            //Filling MainPage List
            GetData();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            //Reactive Example to navigate
            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
            //Reactive Command
            AddSpend = ReactiveCommand.Create(() => { OnAdd(); });
            //Add Salary Command
            AddSalary = ReactiveCommand.Create(() => { OnAddSalary(); });
        }
        private void GetData()
        {
            List<Spend> data = new List<Spend>();
            //await Task.Run(() =>
            //{
            data = spendService.GetAll().Result;
            data.Reverse();
            _spends.AddRange(data);
            _spends.Connect().Bind(out _spendingList).Subscribe();
            CurrentBill = moneyService.GetCurrentBill().Result.MyCahsMoney;
            //});
        }
        private async void OnAdd()
        {
            try
            {
                SpendDescription = await App.Current.MainPage.DisplayPromptAsync("Write description:", "You can skip this...");

                Spend item = new Spend
                {
                    Amount = int.Parse(SpendValue),
                    WhereText = SpendDescription,
                    TransactionDate = DateTime.Now
                };
                await Task.Run(async () =>
                {
                    await spendService.SaveItemAsync(item);
                    await moneyService.UpdateAllMoneyAsync(new AllMoney { MyCahsMoney = CurrentBill - item.Amount });
                    _spends.Clear();
                    SpendValue = "";
                    GetData();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("========== ERRORHERE =============:" + ex + "=========== ENDERROR==================");
            }
        }
        private async void OnAddSalary()
        {
            var Amount = decimal.Parse(await App.Current.MainPage.DisplayPromptAsync("Write description:", "You can skip this..."));
            CurrentBill += Amount;
            AllMoney item = new AllMoney
            {
                MyCahsMoney = CurrentBill
            };
            await moneyService.UpdateAllMoneyAsync(item);
        }
    }
}
