
using System;
using I18NPortable;
using MoneyNote.Models;
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
        public II18N Strings => I18N.Current;
        public Action ActionAfter { get; set; }
        public bool IsCancelPressed { get; set; }
        public CommitPopupView(Action act)
        {
            InitializeComponent();
            downHand.IsVisible = false;
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
        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!IsCancelPressed)
            {
                if (categoryButton.Text == Strings["choose_category"])
                {
                    await PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_need_category"]), true);
                    return;
                }
                var result = entry.Text;
                if (String.IsNullOrWhiteSpace(entry.Text))
                {
                    var type = UnwrapSpendingCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedSpendingCategory", 0));
                    result = type.Type.ToString() + " " + Strings["missed"];
                }
                CrossSettings.Current.AddOrUpdateValue("CommitMessage", result);
                CrossSettings.Current.AddOrUpdateValue("CurrentCommitMoneyFrom", FuncMoneyFrom());

                PopupNavigation.Instance.PopAsync(true);
                ActionAfter?.Invoke();
            }

        }
        private int FuncMoneyFrom()
        {
            if (cashSwitch.IsToggled) return 0; else return 1;
        }
        private void Cancel_Button_Clicked(object sender, System.EventArgs e)
        {
            IsCancelPressed = true;
            downHand.IsVisible = true;
            leftHand.IsVisible = false;
            rightHand.IsVisible = false;
            OnBackgroundClicked();
            PopupNavigation.Instance.PopAsync(true);
        }
        private CategoryDto UnwrapSpendingCategoryType(int id)
        {
            foreach (var item in Categories.GetAllSpendingCategories())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return new CategoryDto();
        }

        #region Settings/Animations
        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
        }
        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            IsCancelPressed = true;
            downHand.IsVisible = true;
            animation.PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom;
            animation.ScaleOut = 1;
            return base.OnBackgroundClicked();
        }
        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
            if (!IsCancelPressed)
            {
                leftHand.IsVisible = true;
                rightHand.IsVisible = true;
            }
        }
        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
            leftHand.IsVisible = false;
            rightHand.IsVisible = false;
        }
        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
            upHand.IsVisible = true;

        }
        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            upHand.IsVisible = false;
        }
        private void PlugForGridGesture(object sender, EventArgs e)
        {
            //dont delete this method for unclosing popup when it tapped on empty space
        }
        #endregion
    }
}
