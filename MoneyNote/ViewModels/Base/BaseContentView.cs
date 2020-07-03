using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyNote.ViewModels.Base
{
    public class BaseContentView : ContentView
    {
        private static TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        protected static void GestureFunc(Frame fra)
        {
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // more info about animations:
                // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/animation/simple

                // scale the frame to x1.2
                var scaleUpAnimationTask = fra.ScaleTo(1.2, 500);
                // set opacity to 0 (transparent)
                var fadeOutAnimationTask = fra.FadeTo(0, 500);

                // wait for the 2 animations to finish...
                await Task.WhenAll(scaleUpAnimationTask, fadeOutAnimationTask);

                // scale the frame back to original size
                var scaleDownAnimationTask = fra.ScaleTo(1, 500);
                // set opacity back to 1 (solid)
                var fadeInAnimationTask = fra.FadeTo(1, 500);

                // wait for the 2 animations to finish...
                await Task.WhenAll(scaleDownAnimationTask, fadeInAnimationTask);
            };
            fra.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
