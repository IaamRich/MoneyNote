using System;
using System.Globalization;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class MonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                if (date >= DateTime.Now || date.Month == DateTime.Now.Month) return true;
                else return false;
            }
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
