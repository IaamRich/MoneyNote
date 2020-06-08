using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigablePage : ReactiveContentPage<NavigableViewModel>
    {
        public NavigablePage()
        {
            InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
                        .DisposeWith(disposables);
                });
        }
    }
}
