
using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionPage : ReactiveContentPage<CollectionViewModel>
    {
        public CollectionPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new CollectionViewModel();
        }
    }
}
