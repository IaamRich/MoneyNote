
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryChangeNotesLookView : ContentView
    {
        public HistoryChangeNotesLookView()
        {
            InitializeComponent();
        }
        private void ChangeNotesLookFunc(object sender, System.EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}
