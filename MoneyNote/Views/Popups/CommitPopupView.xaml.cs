
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommitPopupView : PopupPage
    {
        //MainViewModel VM = new MainViewModel();
        //public CommitPopupView(MainViewModel vm)
        //{
        //    InitializeComponent();
        //    VM = vm;
        //}
        public CommitPopupView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            //VM.SpendDescription = entry.Text;
            CrossSettings.Current.AddOrUpdateValue("CommitMessage", entry.Text);
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}
