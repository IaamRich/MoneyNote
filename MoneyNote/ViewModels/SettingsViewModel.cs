using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Resources.Images;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyNote
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableAsPropertyHelper<char> _currentLetter;

        private List<Language> languages = new List<Language>();
        //public SourceList<Language> LanguagesSource = new SourceList<Language>();
        public ObservableCollection<Language> LanguagesList { get; set; }
        public ImageSource LanguageImage { get; set; }
        public II18N Strings => I18N.Current;
        public SettingsViewModel()
        {
            LanguageImage = ImageSource.FromResource(ImageResources.english_language);
            _ = GetLanguages();
            ResetAll = ReactiveCommand.Create(() =>
            {
                ResetAllMethod();
            });
            OffAds = ReactiveCommand.Create(() =>
            {
                Application.Current.MainPage.DisplayAlert("Message", "In developing...", "", "ok");
            });
            ImageCommand = new Command(ImageCommandFunc);
            GoAccount = ReactiveCommand.Create(() =>
            {
                Application.Current.MainPage.DisplayAlert("Message", "Go Account", "", "ok");
            });

        }

        //public char CurrentLetter => _currentLetter.Value;

        public string UrlPathSegment => Strings["menu_settings"];
        public ICommand ResetAll { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand GoAccount { get; set; }
        public ICommand OffAds { get; set; }
        public IScreen HostScreen { get; }

        private void ResetAllMethod()
        {
            Application.Current.MainPage.DisplayAlert("Method", "ResetAll is done", "Cancel", "ok");
        }

        private async Task GetLanguages()
        {
            LanguagesList = new ObservableCollection<Language>()
            {
                new Language { Id = 0, Sign = "en-US", Name = "English", Image = "en.png" },
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


        private async void ImageCommandFunc(object sender)
        {
            switch ((int)sender)
            {
                case 1:
                    LanguageImage = ImageSource.FromResource(ImageResources.russian_language);
                    break;
                case 2:
                    LanguageImage = ImageSource.FromResource(ImageResources.romanian_language);
                    break;
                case 3:
                    LanguageImage = ImageSource.FromResource(ImageResources.italian_language);
                    break;
                case 4:
                    LanguageImage = ImageSource.FromResource(ImageResources.german_language);
                    break;
                case 5:
                    LanguageImage = ImageSource.FromResource(ImageResources.french_language);
                    break;
                case 6:
                    LanguageImage = ImageSource.FromResource(ImageResources.chinese_language);
                    break;
                default:
                    LanguageImage = ImageSource.FromResource(ImageResources.english_language);
                    break;
            }
        }

    }
}
