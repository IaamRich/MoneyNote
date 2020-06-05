using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ReactiveContentPage<SecondViewModel>
    {
        public SecondPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SecondViewModel(null);
        }
    }
}
