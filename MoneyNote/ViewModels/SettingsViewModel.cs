using System.Collections.ObjectModel;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Resources.Images;
using MoneyNote.Services;
using MoneyNote.Services.Contracts;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace MoneyNote
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static IMoneyService _moneyService;
        private static ISpendService _spendService;
        private static ISettingsService _settingsService;
        //Language List
        public ObservableCollection<Language> LanguagesList { get; set; }
        public ImageSource LanguageImage { get; set; }
        //Properties 
        public bool Sounds { get; set; }
        public bool AreaCash { get; set; }
        public bool AreaCard { get; set; }
        public int CurrentLang { get; set; }
        //Commands
        public ICommand ResetAll { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand GoAccount { get; }
        public ICommand OffAds { get; set; }
        public ICommand SoundsCommand { get; set; }
        public ICommand AreaByDefaultCommand { get; set; }
        //Variables for normal functionality of the page
        public II18N Strings => I18N.Current;
        public string UrlPathSegment => Strings["menu_settings"];
        public IScreen HostScreen { get; }
        public SettingsViewModel(ISpendService spendService, IMoneyService moneyService, ISettingsService settingsService, IScreen screen = null)
        {
            //main
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            _spendService = spendService;
            _moneyService = moneyService;
            _settingsService = settingsService;
            //get language
            GetLanguages();
            Sounds = _settingsService.GetSoundsSettings();
            AreaCash = _settingsService.GetDefaultSpendingAreaSettings() == 0 ? true : false;
            AreaCard = !AreaCash;
            //commands
            SoundsCommand = ReactiveCommand.Create(() =>
            {
                Sounds = !Sounds;
                _settingsService.SetSoundsSettings(Sounds);
            });
            AreaByDefaultCommand = ReactiveCommand.Create(() =>
            {
                AreaCash = !AreaCash;
                AreaCard = !AreaCard;
                if (AreaCash) _settingsService.SetDefaultSpendingAreaSettings(0);
                else _settingsService.SetDefaultSpendingAreaSettings(1);

            });
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
                return HostScreen.Router.Navigate.Execute(new AccountViewModel(new MoneyService(), message: "From Settings"));
            });
        }
        private async void ResetAllMethod()
        {
            _moneyService.DeleteAllMoneyNotes();
            await _spendService.DeleteAll();
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

            CurrentLang = _settingsService.GetCurrentLanguage();
            switch (CurrentLang)
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
        private void ImageCommandFunc(object sender)
        {
            switch ((int)sender)
            {
                case 1:
                    LanguageImage = ImageSource.FromResource(ImageResources.russian_language);
                    CurrentLang = 1;
                    break;
                case 2:
                    LanguageImage = ImageSource.FromResource(ImageResources.romanian_language);
                    CurrentLang = 2;
                    break;
                case 3:
                    LanguageImage = ImageSource.FromResource(ImageResources.italian_language);
                    CurrentLang = 3;
                    break;
                case 4:
                    LanguageImage = ImageSource.FromResource(ImageResources.german_language);
                    CurrentLang = 4;
                    break;
                case 5:
                    LanguageImage = ImageSource.FromResource(ImageResources.french_language);
                    CurrentLang = 5;
                    break;
                case 6:
                    LanguageImage = ImageSource.FromResource(ImageResources.chinese_language);
                    CurrentLang = 6;
                    break;
                default:
                    LanguageImage = ImageSource.FromResource(ImageResources.english_language);
                    CurrentLang = 0;
                    break;
            }
            _settingsService.SetCurrentLanguage(CurrentLang);
        }
    }
}
