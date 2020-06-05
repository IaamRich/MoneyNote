using ReactiveUI;
using Splat;

namespace MoneyNote.ViewModels
{
    public class SecondViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Second Page";

        public IScreen HostScreen { get; }
        public SecondViewModel(string message, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            Message = message;
        }

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }
    }
}
