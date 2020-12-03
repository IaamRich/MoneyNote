using System;
using I18NPortable;
using MoneyNote.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagramPopupView : PopupPage
    {
        public bool IsCancelPressed { get; set; }
        public II18N Strings => I18N.Current;
        public DiagramPopupView(PercentageCategory category)
        {
            InitializeComponent();
            this.title.Text = category.Name.ToString();
            this.value.Text = category.Value.ToString();
            this.img.Source = category.Image;
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
