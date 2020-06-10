using MoneyNote.Models;
using MoneyNote.Persistence;
using ReactiveUI.XamForms;
using SQLite;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryView : ReactiveContentPage<HistoryViewModel>
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Spend> _spendings;
        public HistoryView()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }
        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Spend>();
            var spendings = await _connection.Table<Spend>().ToListAsync();
            spendings.Reverse();
            _spendings = new ObservableCollection<Spend>(spendings);
            historyListView.ItemsSource = _spendings;

            base.OnAppearing();
        }

    }
}
