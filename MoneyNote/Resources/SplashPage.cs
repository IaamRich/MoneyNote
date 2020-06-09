using Xamarin.Forms;

namespace MoneyNote.Resources
{
    public class SplashPage : ContentPage
    {
        Image splashImage;
        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#bedb39");
            this.Content = sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.5, 1500, Easing.Linear);
            await splashImage.ScaleTo(3, 1200, Easing.Linear);
            var bootstrapper = new AppBootstrapper();
            Application.Current.MainPage = new MasterView(bootstrapper.CreateMasterViewModel());

        }
    }
}
