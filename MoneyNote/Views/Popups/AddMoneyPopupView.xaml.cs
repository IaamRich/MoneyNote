using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using I18NPortable;
using MoneyNote.Controls;
using MoneyNote.Models;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMoneyPopupView : PopupPage
    {
        private ObservableCollection<Category> CategoryList;
        private List<Category> CatList = new List<Category>();
        public II18N Strings => I18N.Current;
        public Action ActionAfter { get; set; }
        public bool IsCancelPressed { get; set; }
        public AddMoneyPopupView(Action act)
        {
            IsCancelPressed = false;
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
            CategoryList = new ObservableCollection<Category>();
            foreach (var item in GetCategories())
            {
                CategoryList.Add(item);
                CatList.Add(item);
            }


            foreach (var view1 in CatList.Select(el => new CategoryView(el)))
            {
                view1.OnCategorySelected += OnCategorySelected;
                bindableCategoryList.Children.Add(view1);
            }
        }
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!IsCancelPressed)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(async _ =>
                {
                    await Task.Run(() =>
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
                            if (String.IsNullOrWhiteSpace(entry.Text)) result = Strings["missed"];
                            CrossSettings.Current.AddOrUpdateValue("AddMoneyValue", moneyValue);
                            CrossSettings.Current.AddOrUpdateValue("AddMoneyMessage", result);
                            CrossSettings.Current.AddOrUpdateValue("CurrentAddedMoneyTo", FuncMoneyFrom());

                            PopupNavigation.Instance.PopAsync(true);
                            ActionAfter?.Invoke();
                        }
                    });
                });
            }
        }
        private void OnCategorySelected(Category e)
        {
            bindableCategoryList.Children.Cast<CategoryView>().ForEach(x => x.IsSelected = false);

            foreach (var item in CatList.Where(item => e.Name != item.Name)) item.IsSelected = false;

            var element = CatList.FirstOrDefault(it => it.Name == e.Name);

            if (element != null)
                element.IsSelected = e.IsSelected;

            if (element != null)
                bindableCategoryList.Children.Cast<CategoryView>().FirstOrDefault(x => x.Category.Name == e.Name)
                        .IsSelected =
                    element.IsSelected;
            categoryPanel.IsVisible = false;
            originalPanel.IsVisible = true;
            categoryButton.Text = e.Name;
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
        protected override bool OnBackgroundClicked()
        {
            animation.PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Top;
            animation.ScaleOut = 1;
            return base.OnBackgroundClicked();
        }
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
            if (!IsCancelPressed)
            {
                leftHand.IsVisible = true;
                rightHand.IsVisible = true;
            }

        }
        private void CategoryButton_Clicked(object sender, EventArgs e)
        {
            categoryPanel.IsVisible = true;
            originalPanel.IsVisible = false;
        }
        public List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 0, Type = TransactionType.Salary, Name = Strings["type_salary"], Image = "salary.png", IsSelected = false},
                new Category { Id = 1, Type = TransactionType.Earnings, Name = Strings["type_earnings"], Image = "additional_earnings.png", IsSelected = false},
                new Category { Id = 2, Type = TransactionType.Gift, Name = Strings["type_gift"], Image = "gift.png", IsSelected = false},
                new Category { Id = 3, Type = TransactionType.Other, Name = Strings["type_other"], Image = "other.png", IsSelected = false}
            };
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
    }
}
