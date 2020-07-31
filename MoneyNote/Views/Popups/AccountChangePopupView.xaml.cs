using System;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountChangePopupView : PopupPage
    {
        //public II18N Strings => I18N.Current;
        public bool IsCancelPressed { get; set; }
        public Action ActionAfter { get; set; }
        public AccountChangePopupView(Action act, string title, string alert)
        {
            InitializeComponent();
            this.title.Text = title;
            this.alertMessage.Text = alert;
            ActionAfter = act;
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!IsCancelPressed)
            {
                var result = entry.Text;
                CrossSettings.Current.AddOrUpdateValue("CurrentAccountPopupValue", result);

                PopupNavigation.Instance.PopAsync(true);
                ActionAfter?.Invoke();
            }
        }
        //private bool CheckStringForValue(string str)
        //{
        //    if (String.IsNullOrEmpty(str))
        //    {
        //        PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
        //        return false;
        //    }
        //    else if (str[0] == '0')
        //    {
        //        PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
        //        return false;
        //    }
        //    else if (str[0] == '.')
        //    {
        //        PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
        //        return false;
        //    }
        //    return true;
        //}
    }
}
