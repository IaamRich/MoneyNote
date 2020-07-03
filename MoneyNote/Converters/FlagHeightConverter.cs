using System;
using System.Globalization;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class FlagHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //if ((double)value > 1) // if height exists.
            //    return (((double)value) / 10);  // 1/10 of screen height.
            //else
            //    return 5;
            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
