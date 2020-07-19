using System.ComponentModel;

namespace MoneyNote.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;

        public string UserName
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //#region Properties
        //private int _red;
        //public int Red
        //{
        //    get => _red;
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref _red, value);
        //        this.RaisePropertyChanged(nameof(BackgroundColor));
        //    }
        //}
        //private int _green;
        //public int Green
        //{
        //    get => _green;
        //    set
        //    {
        //        this.RaisePropertyChanged(nameof(BackgroundColor));
        //        this.RaiseAndSetIfChanged(ref _green, value);
        //    }
        //}

        //private int _blue;
        //public int Blue
        //{
        //    get => _blue;
        //    set
        //    {
        //        this.RaisePropertyChanged(nameof(BackgroundColor));
        //        this.RaiseAndSetIfChanged(ref _blue, value);
        //    }
        //}

        //public Color BackgroundColor
        //{
        //    get => Color.FromRgb(Red, Green, Blue);
        //}
    }
}
