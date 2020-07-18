
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryChangeNotesView : ContentView
    {
        public HistoryChangeNotesView()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}
