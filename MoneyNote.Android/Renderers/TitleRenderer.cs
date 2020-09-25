using System;
using Android.Content;
using Android.Graphics;
using MoneyNote.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(TitleRenderer))]
namespace MoneyNote.Droid.Renderers

{
    class TitleRenderer : NavigationPageRenderer
    {

        private Android.Support.V7.Widget.Toolbar _toolbar;
        private Context _context;

        public TitleRenderer(Context context) : base(context)
        {
            _context = context;
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                _toolbar = (Android.Support.V7.Widget.Toolbar)child;
                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                //_toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();


            Console.WriteLine(e.Child.GetType().ToString());
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/flow-bold.otf");
                textView.Typeface = spaceFont;
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}
