using ReactiveUI;

namespace MoneyNote
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableAsPropertyHelper<char> _currentLetter;

        public SettingsViewModel()
        {
            //_currentLetter = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Scan(64, (acc, current) => acc + 1)
            //    .Select(x => (char)x)
            //    .Take(26)
            //    .ToProperty(this, x => x.CurrentLetter, scheduler: RxApp.MainThreadScheduler);
        }

        //public char CurrentLetter => _currentLetter.Value;

        public string UrlPathSegment => "Settings";

        public IScreen HostScreen { get; }
    }
}
