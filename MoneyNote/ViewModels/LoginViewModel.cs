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
    }
}
