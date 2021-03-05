using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteNotePopupView : PopupPage
    {
        //public II18N Strings => I18N.Current;
        public bool IsCancelPressed { get; set; }
        public Action ActionAfter { get; set; }
        public DeleteNotePopupView(Action act, string title)
        {
            InitializeComponent();
            this.title.Text = title;
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

        private void Button_Clicked_Yes(object sender, EventArgs e)
        {
            if (!IsCancelPressed)
            {
                PopupNavigation.Instance.PopAsync(true);
                ActionAfter?.Invoke();
            }
        }
    }
}
