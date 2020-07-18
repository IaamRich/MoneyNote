using System;
using System.Windows.Input;
using I18NPortable;
using ReactiveUI;
using Xamarin.Essentials;

namespace MoneyNote
{
    public class AboutViewModel : ReactiveObject, IRoutableViewModel
    {
        public II18N Strings => I18N.Current;
        public ICommand OpenWebCommand { get; set; }
        public ICommand VisitCompanySite { get; set; }
        public AboutViewModel()
        {
            OpenWebCommand = ReactiveCommand.Create(() => { Launcher.OpenAsync(new Uri(@"https://curaciov.com")); });
            VisitCompanySite = ReactiveCommand.Create(() => { Launcher.OpenAsync(new Uri(@"https://sapec.md")); });
        }

        public string UrlPathSegment => Strings["menu_about"];
        public IScreen HostScreen { get; }
    }
}
