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

        private void img11_Clicked(object sender, System.EventArgs e)
        {
            if (img11.IsVisible == true)
            {
                CollectionsList.IsVisible = true;
                languageLabel.IsVisible = false;
                img11.IsVisible = false;
            }
            else
            {
                CollectionsList.IsVisible = false;
                languageLabel.IsVisible = true;
                img11.IsVisible = true;
            }
        }
    }
}
