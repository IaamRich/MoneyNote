using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ReactiveContentPage<SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();

            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .OneWayBind(ViewModel, vm => vm.CurrentLetter, v => v.LetterLabel.Text)
            //            .DisposeWith(disposables);
            //    });
        }
    }
}
