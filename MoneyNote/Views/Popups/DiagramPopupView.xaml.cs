using System;
using System.Collections.ObjectModel;
using System.Linq;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagramPopupView : PopupPage
    {
        public bool IsCancelPressed { get; set; }
        public II18N Strings => I18N.Current;

        public ObservableCollection<Transaction> OutputTransactions { get; set; }
        public ObservableCollection<Transaction> LastTransactionsList { get; set; }
        private int lastID = 0;
        public DiagramPopupView(PercentageCategory category, string month)
        {
            InitializeComponent();
            this.title.Text = category.Name.ToString();
            this.value.Text = category.Value.ToString();
            this.img.Source = category.Image;

            ///

            var _transactionService = new TransactionService();
            var data = _transactionService.GetAll().Result;
            if (data.Count != 0)
            {
                lastID = data.First().Id;
            }
            LastTransactionsList = new ObservableCollection<Transaction>();
            data.ForEach(x => LastTransactionsList.Add(x));

            var outlay = from transaction in LastTransactionsList where (transaction.Category.Type == category.Type && transaction.Date.ToString("MMMM") == month) select transaction;
            OutputTransactions = new ObservableCollection<Transaction>();
            outlay.ToList().ForEach(x => OutputTransactions.Add(x));
            //lastTransactions.ItemsSource = OutputTransactions;

            BindableLayout.SetItemsSource(lastTransactions, OutputTransactions);
            ///
        }
        private void Cancel_Button_Clicked(object sender, System.EventArgs e)
        {
            IsCancelPressed = true;
            PopupNavigation.Instance.PopAsync(true);
        }
        private void PlugForGridGesture(object sender, EventArgs e)
        {
            //dont delete this method for unclosing popup when it tapped on empty space
        }
    }
}
