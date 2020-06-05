using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ReactiveContentPage<FirstViewModel>
    {
        public FirstPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new FirstViewModel();
        }
    }
}
