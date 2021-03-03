using System;
using System.Globalization;
using I18NPortable;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class ToStringConverter : IValueConverter
    {
        private II18N Strings => I18N.Current;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value.ToString();
            switch (result)
            {
                case "Cash":
                    return Strings["cash"];
                    break;
                case "Card":
                default:
                    return Strings["card"];
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
