using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services.Contracts;
using MoneyNote.Views.Popups;
using Plugin.Settings;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
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
        private static ISpendService _spendService;
        private static IMoneyService _moneyService;
        //UI variables
        public string SpendDescription { get; set; }
        public string AddMoneyDescription { get; set; }
        public decimal AddMoneyValue { get; set; }
        public string SpendValue { get; set; }
        public decimal CurrentBill { get; set; }
        public decimal CurrentCash { get; set; }
        public decimal CurrentCard { get; set; }
        public bool IsCash { get; set; }
        public bool IsCard { get; set; }
        //List Variables
        private SourceList<Spend> _spends = new SourceList<Spend>();
        private ReadOnlyObservableCollection<Spend> _spendingList;
        public ReadOnlyObservableCollection<Spend> SpendingList => _spendingList;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_main"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        public MainViewModel(ISpendService spendService, IMoneyService moneyService, IScreen hostScreen = null)
        {
            _spendService = spendService;
            _moneyService = moneyService;
            IsCash = true;
            IsCard = false;
            GetData();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            //Reactive Example to navigate
            NavigateToDummyPage = ReactiveCommand
                .CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
            AddSpend = ReactiveCommand.Create(() => { OnAdd(); });
            AddSalary = ReactiveCommand.Create(() => { OnAddSalary(); });
        }
        private void GetData()
        {
            var data = _spendService.GetAll().Result;
            data.Reverse();
            _spends.AddRange(data);
            _spends.Connect().Bind(out _spendingList).Subscribe();
            CurrentCard = _moneyService.GetCurrentCard();
            CurrentCash = _moneyService.GetCurrentCash();
            CurrentBill = CurrentCard + CurrentCash;
        }

        private async void OnAdd()
        {
            if (String.IsNullOrEmpty(SpendValue))
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
            }
            else if (SpendValue[0] == '0')
            {
                await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
            }
            else if (SpendValue[0] == '.') await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
            else await PopupNavigation.Instance.PushAsync(new CommitPopupView(OnAddFunc), true);
        }
        private async void OnAddFunc()
        {
            SpendDescription = CrossSettings.Current.GetValueOrDefault("CommitMessage", "");
            Spend item = new Spend
            {
                Amount = decimal.Parse(SpendValue),
                WhereText = SpendDescription,
                TransactionDate = DateTime.Now
            };
            await Task.Run(async () =>
            {
                await _spendService.SaveItemAsync(item);
                switch (CrossSettings.Current.GetValueOrDefault("CurrentCommitMoneyFrom", 0))
                {
                    case 0:
                        CurrentCash -= item.Amount;
                        _moneyService.SetCurrentCash(CurrentCash);
                        break;
                    case 1:
                        CurrentCard -= item.Amount;
                        _moneyService.SetCurrentCard(CurrentCard);
                        break;
                }
                _spends.Clear();
                SpendValue = "";
                GetData();
            });
        }
        private async void OnAddSalary()
        {
            await PopupNavigation.Instance.PushAsync(new AddMoneyPopupView(OnAddSalaryFunc), true);

        }
        private async void OnAddSalaryFunc()
        {
            AddMoneyDescription = CrossSettings.Current.GetValueOrDefault("AddMoneyMessage", "");
            AddMoneyValue = CrossSettings.Current.GetValueOrDefault("AddMoneyValue", 0.0m);
            Save item = new Save
            {
                Amount = AddMoneyValue,
                Description = AddMoneyDescription,
                TransactionDate = DateTime.Now
            };
            await Task.Run(() =>
            {
                //await saveService.SaveItemAsync(item);
                switch (CrossSettings.Current.GetValueOrDefault("CurrentAddedMoneyTo", 0))
                {
                    case 0:
                        CurrentCash += item.Amount;
                        _moneyService.SetCurrentCash(CurrentCash);
                        break;
                    case 1:
                        CurrentCard += item.Amount;
                        _moneyService.SetCurrentCard(CurrentCard);
                        break;
                }
                //clearing and getting
                _spends.Clear();
                GetData();
            });
        }
    }
}
