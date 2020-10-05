using ReactiveUI.XamForms;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ReactiveContentPage<SettingsViewModel>
    {
        public static double WidthAfter { get; set; } = 10;

        public SettingsView()
        {
            InitializeComponent();
            //BindingContext = ViewModel = new SettingsViewModel();

            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .OneWayBind(ViewModel, vm => vm.CurrentLetter, v => v.LetterLabel.Text)
            //            .DisposeWith(disposables);
            //    });
        }

        private void iconLanguage_Clicked(object sender, System.EventArgs e)
        {
            if (iconLanguage.IsVisible == true)
            {
                CollectionsList.IsVisible = true;
                languageLabel.IsVisible = false;
                iconLanguage.IsVisible = false;
            }
            else
            {
                CollectionsList.IsVisible = false;
                languageLabel.IsVisible = true;
                iconLanguage.IsVisible = true;
            }
        }
        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SetWidthFunc(chooseLanguageStack.Height);
        }
        private void SetWidthFunc(double element)
        {
            WidthAfter = element;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
