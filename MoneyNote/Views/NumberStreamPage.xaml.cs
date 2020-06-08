using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberStreamPage : ReactiveContentPage<NumberStreamViewModel>
    {
        public NumberStreamPage()
        {
            InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .ViewModel
                        .NumberStream
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .BindTo(this, x => x.NumberLabel.Text)
                        .DisposeWith(disposables);
                });
        }
    }
}
