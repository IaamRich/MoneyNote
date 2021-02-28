
using FFImageLoading.Forms.Platform;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PanCardView.iOS;
using UIKit;
using Xamarin.Forms;

namespace MoneyNote.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags("SwipeView_Experimental");
            Rg.Plugins.Popup.Popup.Init();

            CachedImageRenderer.Init();
            AppCenter.Start("e5753842-cc10-4a5d-b170-dac37070ad4b", typeof(Analytics), typeof(Crashes));

            global::Xamarin.Forms.Forms.Init();
            CardsViewRenderer.Preserve();
            XF.Material.iOS.Material.Init();
            LoadApplication(new App());

            AnimationViewRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
