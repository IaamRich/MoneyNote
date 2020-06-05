using MoneyNote.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactsViewModel();
        }
    }
}
