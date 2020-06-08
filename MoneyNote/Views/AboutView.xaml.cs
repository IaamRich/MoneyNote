using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutView : ReactiveContentPage<AboutViewModel>
    {
        public AboutView()
        {
            InitializeComponent();
        }
    }
}
