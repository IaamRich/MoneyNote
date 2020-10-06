using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagramView : ReactiveContentPage<DiagramViewModel>
    {
        public DiagramView()
        {
            InitializeComponent();
        }
    }
}
