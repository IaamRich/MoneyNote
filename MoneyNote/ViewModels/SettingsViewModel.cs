using I18NPortable;
using MoneyNote.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



//using DynamicData;
//using MvvmCross.Logging;
//using MvvmCross.Navigation;
//using Plugin.Media;
//using Plugin.Media.Abstractions;
//using ReactiveUI;
//using ReactiveUI.Fody.Helpers;
//using ReportTrailer.Core.Extensions;
//using ReportTrailer.Core.Helpers;
//using ReportTrailer.Core.Model.Account;
//using ReportTrailer.Core.ViewModel.Base;
//using ReportTrailer.Core.ViewModel.Popups;
//using ReportTrailer.WebServices.Abstraction.Services;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Reactive;
//using System.Threading.Tasks;
//using Xamarin.Essentials;
//using Xamarin.Forms;



namespace MoneyNote
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableAsPropertyHelper<char> _currentLetter;

        private List<Language> languages = new List<Language>();
        //public SourceList<Language> LanguagesSource = new SourceList<Language>();
        public ObservableCollection<Language> LanguagesList { get; set; }
        public II18N Strings => I18N.Current;
        public SettingsViewModel()
        {
            _ = GetLanguages();
            ResetAll = ReactiveCommand.Create(() =>
            {
                ResetAllMethod();
            });

            //_currentLetter = Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Scan(64, (acc, current) => acc + 1)
            //    .Select(x => (char)x)
            //    .Take(26)
            //    .ToProperty(this, x => x.CurrentLetter, scheduler: RxApp.MainThreadScheduler);
        }

        //public char CurrentLetter => _currentLetter.Value;

        public string UrlPathSegment => Strings["menu_settings"];
        public ICommand ResetAll { get; set; }

        public IScreen HostScreen { get; }

        private void ResetAllMethod()
        {
            Application.Current.MainPage.DisplayAlert("Method", "ResetAll is done", "Cancel", "ok");
        }

        private async Task GetLanguages()
        {
            LanguagesList = new ObservableCollection<Language>()
            {
                new Language { Id = 0, Sign = "en-US", Name = "English", Image = "us.png" },
                new Language { Id = 1, Sign = "ru-RU", Name = "Russian", Image = "ru.png" },
                new Language { Id = 2, Sign = "md-MD", Name = "Romanian", Image = "md.png" },
                new Language { Id = 3, Sign = "it-IT", Name = "Italian", Image = "it.png" },
                new Language { Id = 4, Sign = "de-DE", Name = "German", Image = "de.png" },
                new Language { Id = 5, Sign = "fr-FR", Name = "French", Image = "fr.png" },
                new Language { Id = 6, Sign = "zh-CN", Name = "Chinese", Image = "zh.png" }
            };


            //foreach (var item in languages)
            //{
            //    LanguagesList.Add(item);
            //}
        }

    }
}
