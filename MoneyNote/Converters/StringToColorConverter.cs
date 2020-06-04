using System;
using System.Globalization;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var text = value.ToString();

                if (text == "Blue")
                {
                    return Color.Blue;
                }
                else if (text == "Red")
                {
                    return Color.Purple;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
