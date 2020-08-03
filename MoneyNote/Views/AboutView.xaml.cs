using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutView : ReactiveContentPage<AboutViewModel>
    {
        public AboutView()
        {
            InitializeComponent();
            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
            //            .DisposeWith(disposables);
            //        this
            //            .BindCommand(ViewModel, vm => vm.NavigateBack, v => v.BackButton)
            //            .DisposeWith(disposables);
            //    });
        }

        private void scroll_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            arrow.IsVisible = false;
        }
    }
}
