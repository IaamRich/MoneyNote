using MoneyNote.Models;
using MoneyNote.Persistence;
using ReactiveUI.XamForms;
using SQLite;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ReactiveContentPage<MainViewModel>
    {
        private static SQLiteAsyncConnection _connection;
        private ObservableCollection<Spend> _spendings;
        private ObservableCollection<AllMoney> _allMoney;

        public MainView()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
            //            .DisposeWith(disposables);
            //    });

        }
        protected override async void OnAppearing()
        {
            // создание таблицы, если ее нет
            //await App.Database.CreateTable();
            // привязка данных
            //spendingListView.ItemsSource = await App.Database.GetItemsAsync();
            await _connection.CreateTableAsync<Spend>();

            var spendings = await _connection.Table<Spend>().ToListAsync();
            spendings.Reverse();
            _spendings = new ObservableCollection<Spend>(spendings);
            spendingListView.ItemsSource = _spendings;

            base.OnAppearing();
        }


        async void OnAdd(object sender, System.EventArgs e)
        {
            try
            {
                int amount = Int32.Parse(entrySpend.Text);
                if (!String.IsNullOrWhiteSpace(entrySpend.Text) && amount > 0)
                {
                    string Description = await DisplayPromptAsync("Write description:", "You can skip this...");
                    if (Description == "")
                    {
                        Description = "Not specified";
                    }
                    var spend = new Spend
                    {
                        Amount = amount,
                        WhereText = Description,
                        TransactionDate = DateTime.Now
                    };

                    var allMoney = new AllMoney
                    {
                        AllTimeOutlay = amount
                    };
                    await _connection.InsertAsync(spend);
                    _spendings.Insert(0, spend);
                    //_allMoney.Clear();
                    //var allmoney = await _connection.Table<AllMoney>().ToListAsync();
                    //foreach (var item in allmoney)
                    //{
                    //    var sum = item.AllTimeOutlay + amount;
                    //    item.AllTimeOutlay = sum;
                    //    _allMoney.Add(item);
                    //    currentbill.Text = sum.ToString();
                    //}
                    //foreach (var item in _allMoney)
                    //{
                    //    allmoney.Add(item);
                    //}
                    //await _connection.InsertOrReplaceAsync(allMoney);

                    entrySpend.Text = "";
                }
                else
                {
                    await DisplayAlert("No Amount", "Enter some value", "Ok");
                    entrySpend.Text = "";
                }
            }
            catch
            {
                await DisplayAlert("Wrong Value", "Enter some value", "Ok");
                entrySpend.Text = "";
            }
        }

        async void OnUpdate(object sender, System.EventArgs e)
        {
            var spend = _spendings[0];

            spend.WhereText += " UPDATED";

            await _connection.UpdateAsync(spend);
        }

        async void OnDelete(object sender, System.EventArgs e)
        {
            var spend = _spendings[0];
            await _connection.DeleteAsync(spend);
            _spendings.Remove(spend);
        }
    }
}
