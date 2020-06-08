using ReactiveUI;
using Xamarin.Forms;

namespace MoneyNote.ViewModels
{
    public class ColorsDemoViewModel : ReactiveObject
    {
        #region Properties
        private int _red;
        public int Red
        {
            get => _red;
            set
            {
                this.RaiseAndSetIfChanged(ref _red, value);
                this.RaisePropertyChanged(nameof(BackgroundColor));
            }
        }
        private int _green;
        public int Green
        {
            get => _green;
            set
            {
                this.RaisePropertyChanged(nameof(BackgroundColor));
                this.RaiseAndSetIfChanged(ref _green, value);
            }
        }

        private int _blue;
        public int Blue
        {
            get => _blue;
            set
            {
                this.RaisePropertyChanged(nameof(BackgroundColor));
                this.RaiseAndSetIfChanged(ref _blue, value);
            }
        }

        public Color BackgroundColor
        {
            get => Color.FromRgb(Red, Green, Blue);
        }
        #endregion
    }
}
