using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InDevelopingPopupView : PopupPage
    {
        //public II18N Strings => I18N.Current;
        public bool IsCancelPressed { get; set; }
        public Action ActionAfter { get; set; }
        public InDevelopingPopupView(string title)
        {
            InitializeComponent();
            this.title.Text = title;
        }
        private void Cancel_Button_Clicked(object sender, System.EventArgs e)
        {
            IsCancelPressed = true;
            PopupNavigation.Instance.PopAsync(true);
        }
        private void PlugForGridGesture(object sender, EventArgs e)
        {
            //dont delete this method for unclosing popup when it tapped on empty space
        }

    }
}
