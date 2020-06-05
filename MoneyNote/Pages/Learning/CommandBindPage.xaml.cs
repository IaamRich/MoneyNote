using MoneyNote.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandBindPage : ReactiveContentPage<CommandBindViewModel>
    {
        public CommandBindPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new CommandBindViewModel();

            this.BindCommand(ViewModel, vm => vm.TestCommand, page => page.slider, nameof(slider.ValueChanged));
        }
    }
}
