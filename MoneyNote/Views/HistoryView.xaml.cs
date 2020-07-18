using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryView : ReactiveContentPage<HistoryViewModel>
    {
        public HistoryView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            changeNotes.IsVisible = false;
        }
    }
}
