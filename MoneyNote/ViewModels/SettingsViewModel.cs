using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Resources.Images;
using MoneyNote.Services;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace MoneyNote
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static MoneyService moneyService;
        private static SpendService spendService;
        private static SettingsService settingsService;
        //Language List
        public ObservableCollection<Language> LanguagesList { get; set; }
        public ImageSource LanguageImage { get; set; }
        //Commands
        public ICommand ResetAll { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand GoAccount { get; }
        public ICommand OffAds { get; set; }
        //Variables for normal functionality of the page
        public II18N Strings => I18N.Current;
        public string UrlPathSegment => Strings["menu_settings"];
        public IScreen HostScreen { get; }
        public SettingsViewModel(IScreen screen = null)
        {
            //main
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            spendService = new SpendService();
            moneyService = new MoneyService();
            settingsService = new SettingsService();
            //get language
            LanguageImage = ImageSource.FromResource(ImageResources.english_language);
            GetLanguages();
            //commands
            ResetAll = ReactiveCommand.Create(() =>
            {
                ResetAllMethod();
            });
            OffAds = ReactiveCommand.Create(() =>
            {
                Application.Current.MainPage.DisplayAlert("Message", "In developing...", "", "ok");
            });
            ImageCommand = new Command(ImageCommandFunc);
            GoAccount = ReactiveCommand.CreateFromObservable(() =>
            {
                return HostScreen.Router.Navigate.Execute(new AccountViewModel("From Settings"));
            });
        }
        private async void ResetAllMethod()
        {
            await moneyService.DeleteAll();
            await spendService.DeleteAll();
            await Application.Current.MainPage.DisplayAlert("Method", "ResetAll is done", "Cancel", "ok");
        }
        private void GetLanguages()
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
            var current = settingsService.GetCurrentLanguage().Result.CurrentLanguage;
            switch (current)
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
        private async void ImageCommandFunc(object sender)
        {

            await Task.Run(async () =>
            {
                var settings = new Settings();
                switch ((int)sender)
                {
                    case 1:
                        LanguageImage = ImageSource.FromResource(ImageResources.russian_language);
                        settings.CurrentLanguage = 1;
                        break;
                    case 2:
                        LanguageImage = ImageSource.FromResource(ImageResources.romanian_language);
                        settings.CurrentLanguage = 2;
                        break;
                    case 3:
                        LanguageImage = ImageSource.FromResource(ImageResources.italian_language);
                        settings.CurrentLanguage = 3;
                        break;
                    case 4:
                        LanguageImage = ImageSource.FromResource(ImageResources.german_language);
                        settings.CurrentLanguage = 4;
                        break;
                    case 5:
                        LanguageImage = ImageSource.FromResource(ImageResources.french_language);
                        settings.CurrentLanguage = 5;
                        break;
                    case 6:
                        LanguageImage = ImageSource.FromResource(ImageResources.chinese_language);
                        settings.CurrentLanguage = 6;
                        break;
                    default:
                        LanguageImage = ImageSource.FromResource(ImageResources.english_language);
                        settings.CurrentLanguage = 0;
                        break;
                }
                await settingsService.UpdateAllSettingsAsync(settings);

            });
        }
    }
}
