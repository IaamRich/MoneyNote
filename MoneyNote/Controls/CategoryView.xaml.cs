
using System;
using MoneyNote.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentView
    {
        public static readonly BindableProperty CategoryProperty =
            BindableProperty.Create("Category", typeof(Category), typeof(CategoryView));

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(bool), typeof(CategoryView), default(bool),
                BindingMode.TwoWay);

        //private readonly ImageSource ic_toggle_left = ImageSource.FromResource(ImageResources.ic_toggle_left,
        //    typeof(BaseProduct).GetTypeInfo().Assembly);

        //private readonly ImageSource ic_toggle_right = ImageSource.FromResource(ImageResources.ic_toggle_right,
        //    typeof(BaseProduct).GetTypeInfo().Assembly);

        public Action<Category> OnCategorySelected;

        public CategoryView(Category category)
        {
            InitializeComponent();
            label.Text = category.Name;
            image.Source = category.Image;
            Category = category;
        }

        public Category Category
        {
            get => GetValue(CategoryProperty) as Category;
            set => SetValue(CategoryProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsSelectedProperty.PropertyName)
            {
                frame.BackgroundColor = IsSelected ? (Color)Application.Current.Resources["AboutBackGround"] : (Color)Application.Current.Resources["BackgroundWhite"];
                label.TextColor = IsSelected ? (Color)Application.Current.Resources["MainGreen"] : (Color)Application.Current.Resources["PopupButton"];
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Category.IsSelected = !Category.IsSelected;
            //label.TextColor = Category.IsSelected ? Color.Red : Color.Gray;

            OnCategorySelected?.Invoke(Category);
        }
    }
}
