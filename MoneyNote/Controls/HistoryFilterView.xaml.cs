
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryFilterView : ContentView
    {
        public HistoryFilterView()
        {
            InitializeComponent();
        }
        private void ChangeFilterClicked(object sender, System.EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}
