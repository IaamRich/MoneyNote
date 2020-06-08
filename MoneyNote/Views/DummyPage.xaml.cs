using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DummyPage : ReactiveContentPage<DummyViewModel>
    {
        public DummyPage()
        {
            InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
                        .DisposeWith(disposables);
                    this
                        .BindCommand(ViewModel, vm => vm.NavigateBack, v => v.BackButton)
                        .DisposeWith(disposables);
                });
        }
    }
}
