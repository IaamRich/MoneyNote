using MoneyNote.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationDemoPage : ReactiveContentPage<ValidationDemoViewModel>
    {
        public ValidationDemoPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new ValidationDemoViewModel();

            this.WhenActivated(d =>
            {
                this.BindValidation(ViewModel, vm => vm.BirthDate, page => page.validationLabel.Text).DisposeWith(d);
                //this.BindValidation(ViewModel, vm => vm.BirthDate, page => page.validationLabel.Text);
            });

        }
    }
}
