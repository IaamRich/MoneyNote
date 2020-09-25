//using Android.Graphics;
//using MoneyNote.Droid.Renderers;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android.AppCompat;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
//namespace MoneyNote.Droid.Renderers
//{
//    public class CustomNavigationPageRenderer : NavigationPageRenderer
//    {
//        private Android.Support.V7.Widget.Toolbar _toolbar;

//        public override void OnViewAdded(Android.Views.View child)
//        {
//            base.OnViewAdded(child);

//            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
//            {
//                _toolbar = (Android.Support.V7.Widget.Toolbar)child;
//                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            base.Dispose(disposing);

//            if (disposing)
//            {
//                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
//            }
//        }

//        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
//        {
//            var view = e.Child.GetType();

//            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
//            {
//                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
//                var spaceFont = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "Fonts/FlowExt-Bold.otf");
//                var systemFont = Typeface.Default;
//                var systemBoldFont = Typeface.DefaultBold;
//                textView.Typeface = spaceFont;
//                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
//            }
//        }
//    }
//}
