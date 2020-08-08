
using Android.App;
using Android.Content.PM;
using Android.OS;
using TouchEffect.Android;
using Xamarin.Forms.Platform.Android;

namespace MoneyNote.Droid
{
    [Activity(Label = "MoneyNotique",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            // Create your application here
            TouchEffectPreserver.Preserve();
        }
    }
}
