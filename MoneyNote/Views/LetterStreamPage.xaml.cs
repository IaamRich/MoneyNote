using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LetterStreamPage : ReactiveContentPage<LetterStreamViewModel>
    {
        public LetterStreamPage()
        {
            InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .OneWayBind(ViewModel, vm => vm.CurrentLetter, v => v.LetterLabel.Text)
                        .DisposeWith(disposables);
                });
        }
    }
}
