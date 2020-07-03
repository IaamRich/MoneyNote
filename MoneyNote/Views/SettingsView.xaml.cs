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
        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            //first element check is enough because all elements have the same size.
            OnPlatformChangeFunc(chooseLanguageStack.Height);
        }
        private void OnPlatformChangeFunc(double element)
        {
            WidthAfter = element - 10;
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.iOS:
            //        BorderAfter = element.Height / 2;
            //        break;
            //    default:
            //        BorderAfter = element.Height / 2 + 5;
            //        break;
            //}
        }
    }
}
