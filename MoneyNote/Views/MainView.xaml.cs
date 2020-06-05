
using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ReactiveMasterDetailPage<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = ViewModel = new MainViewModel();
        }
    }
}
