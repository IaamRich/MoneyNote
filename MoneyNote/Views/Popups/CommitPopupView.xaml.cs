
using System;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommitPopupView : PopupPage
    {
        public Action ActionAfter { get; set; }
        public CommitPopupView(Action act)
        {
            InitializeComponent();
            switch (CrossSettings.Current.GetValueOrDefault("IdSpendFromWhere", 0))
            {
                case 1:
                    cardSwitch.IsToggled = true;
                    break;
                default:
                    cashSwitch.IsToggled = true;
                    break;
            }
            ActionAfter = act;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var result = entry.Text;
            if (String.IsNullOrWhiteSpace(entry.Text)) result = "Missed";
            CrossSettings.Current.AddOrUpdateValue("CommitMessage", result);
            CrossSettings.Current.AddOrUpdateValue("CurrentCommitMoneyFrom", FuncMoneyFrom());

            PopupNavigation.Instance.PopAsync(true);
            ActionAfter?.Invoke();
        }

        private int FuncMoneyFrom()
        {
            if (cashSwitch.IsToggled) return 0; else return 1;
        }
        private void Cancel_Button_Clicked(object sender, System.EventArgs e)
        {
            OnBackgroundClicked();
            PopupNavigation.Instance.PopAsync(true);
        }
        protected override bool OnBackgroundClicked()
        {
            animation.PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom;
            animation.DurationOut = 200;
            animation.ScaleOut = 1;
            return base.OnBackgroundClicked();
        }
        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
        }
    }
}
