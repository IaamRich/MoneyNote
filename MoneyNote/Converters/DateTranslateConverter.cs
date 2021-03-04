using System;
using System.Globalization;
using I18NPortable;
using MoneyNote.Services;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class DateTranslateConverter : IValueConverter
    {
        private II18N Strings => I18N.Current;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var _settingsService = new SettingsService();
            var language = _settingsService.GetCurrentLanguageSettings();
            var valueDate = DateTime.Parse(value.ToString());

            switch (language)
            {
                case 1:
                    CultureInfo lru = new CultureInfo("ru-RU");
                    return valueDate.ToString("d MMMM, dddd", lru);
                case 2:
                    CultureInfo lro = new CultureInfo("ro-RO");
                    return valueDate.ToString("d MMMM, dddd", lro);
                case 0:
                default:
                    CultureInfo len = new CultureInfo("en-US");
                    return valueDate.ToString("MMMM d, dddd", len);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
