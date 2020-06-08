using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountView : ReactiveContentPage<AccountViewModel>
    {
        public AccountView()
        {
            InitializeComponent();

            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .ViewModel
            //            .NumberStream
            //            .ObserveOn(RxApp.MainThreadScheduler)
            //            .BindTo(this, x => x.NumberLabel.Text)
            //            .DisposeWith(disposables);
            //    });
        }
    }
}
