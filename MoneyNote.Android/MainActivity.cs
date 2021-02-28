
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PanCardView.Droid;
using Xamarin.Forms;

namespace MoneyNote.Droid
{
    [Activity(Label = "MoneyNote",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Forms.SetFlags("SwipeView_Experimental");
            CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            AppCenter.Start("f38a5d72-667c-4eeb-8a2c-1c534ccd9b3e", typeof(Analytics), typeof(Crashes));

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CardsViewRenderer.Preserve();
            LoadApplication(new App());
            TouchEffect.Android.TouchEffectPreserver.Preserve();
            Lottie.Forms.Droid.AnimationViewRenderer.Init();
            XF.Material.Droid.Material.Init(this, savedInstanceState);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
