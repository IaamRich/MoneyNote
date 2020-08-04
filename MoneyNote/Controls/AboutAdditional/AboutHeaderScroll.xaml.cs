
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutHeaderScroll : ContentView
    {
        public AboutHeaderScroll()
        {
            InitializeComponent();
        }

        private void scroll_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            arrow.IsVisible = false;
        }
    }
}
