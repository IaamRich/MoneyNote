using System;
using System.Threading.Tasks;
using I18NPortable;
using MoneyNote.Models;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveMoneyPopupView : PopupPage
    {
        public II18N Strings => I18N.Current;
        public Action ActionAfter { get; set; }
        public bool IsCancelPressed { get; set; }
        public SaveMoneyPopupView(Action act)
        {
            IsCancelPressed = false;
            InitializeComponent();
            ActionAfter = act;
        }
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!IsCancelPressed)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(async _ =>
                {
                    await Task.Run(async () =>
                    {
                        if (String.IsNullOrEmpty(entry.Text))
                        {
                            PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
                        }
                        else if (entry.Text[0] == '0')
                        {
                            PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value_zero"]), true);
                        }
                        else if (entry.Text[0] == '.') PopupNavigation.Instance.PushAsync(new AlertPopupView(Strings["alert_no_value"]), true);
                        else
                        {
                            var moneyValue = decimal.Parse(entry.Text);
                            var result = entryDescription.Text;

                            if (String.IsNullOrWhiteSpace(entryDescription.Text))
                            {
                                var type = UnwrapAddingCategoryType(CrossSettings.Current.GetValueOrDefault("SelectedBankCategory", 0));
                                result = type.Type.ToString() + " " + Strings["missed"];
                            }
                            CrossSettings.Current.AddOrUpdateValue("CreditValue", moneyValue);
                            CrossSettings.Current.AddOrUpdateValue("CreditMessage", result);

                            PopupNavigation.Instance.PopAsync(true);
                            ActionAfter?.Invoke();
                        }
                    });
                });
            }
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
        private CategoryDto UnwrapAddingCategoryType(int id)
        {
            foreach (var item in Categories.GetAllBankCategories())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return new CategoryDto();
        }
        #region Settings/Animations
        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            animation.PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Top;
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
            downHand.IsVisible = true;

        }
        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            downHand.IsVisible = false;
        }
        private void PlugForGridGesture(object sender, EventArgs e)
        {
            //dont delete this method for unclosing popup when it tapped on empty space
        }
        #endregion
    }
}
