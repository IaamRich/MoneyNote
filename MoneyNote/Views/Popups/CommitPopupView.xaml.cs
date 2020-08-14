
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using I18NPortable;
using MoneyNote.Controls;
using MoneyNote.Models;
using Plugin.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommitPopupView : PopupPage
    {
        private ObservableCollection<Category> CategoryList;
        private List<Category> CatList = new List<Category>();
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
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!IsCancelPressed)
            {
                var result = entry.Text;
                if (String.IsNullOrWhiteSpace(entry.Text)) result = Strings["missed"];
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
        private void CategoryButton_Clicked(object sender, EventArgs e)
        {
            categoryPanel.IsVisible = true;
            originalPanel.IsVisible = false;
        }
        public List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 0, Type = TransactionType.Market, Name = Strings["type_market"], Image = "market_shop.png", IsSelected = false},
                new Category { Id = 1, Type = TransactionType.Bar, Name = Strings["type_bar"], Image = "restaurant_bar_bistro.png", IsSelected = false},
                new Category { Id = 2, Type = TransactionType.Transport, Name = Strings["type_transport"], Image = "transport.png", IsSelected = false},
                new Category { Id = 3, Type = TransactionType.Business, Name = Strings["type_business"], Image = "business.png", IsSelected = false},
                new Category { Id = 4, Type = TransactionType.Network, Name = Strings["type_network"], Image = "network_products.png", IsSelected = false},
                new Category { Id = 5, Type = TransactionType.Entertainment, Name = Strings["type_entertainment"], Image = "entertainment.png", IsSelected = false}
            };
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
            //animation.DurationOut = 200;
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
