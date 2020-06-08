using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ReactiveContentPage<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();

            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
            //            .DisposeWith(disposables);
            //    });
        }
    }
}
