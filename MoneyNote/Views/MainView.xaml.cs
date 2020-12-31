using System.Diagnostics;
using System.Threading.Tasks;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ReactiveContentPage<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void SwipeGestureRecognizer_Left(object sender, Xamarin.Forms.SwipedEventArgs e)
        {
            if (sender is Grid)
            {
                (sender as Grid).TranslateTo((sender as Grid).X - 100, (sender as Grid).Y, 250);
                await Task.Delay(250);
                (sender as Grid).TranslateTo((sender as Grid).X, (sender as Grid).Y, 250);
            }
        }

        private async void SwipeGestureRecognizer_Right(object sender, Xamarin.Forms.SwipedEventArgs e)
        {
            if (sender is Grid)
            {
                (sender as Grid).TranslateTo((sender as Grid).X + 100, (sender as Grid).Y, 250);
                await Task.Delay(250);
                (sender as Grid).TranslateTo((sender as Grid).X, (sender as Grid).Y, 250);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Debug.WriteLine(sender.ToString());
        }
    }
}
