using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ReactiveContentView<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();
            BindingContext = ViewModel = new MenuViewModel();
        }
    }
}
