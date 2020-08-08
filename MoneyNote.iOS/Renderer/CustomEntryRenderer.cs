using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace MoneyNote.iOS.Renderer
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                //Control.Layer.CornerRadius = 10;
                //Control.TextColor = UIColor.Black;
            }
        }
    }
}
