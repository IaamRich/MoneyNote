using System;
using System.Globalization;
using Xamarin.Forms;

namespace MoneyNote.Converters
{
    public class MinimumMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                var minimumMonth = DateTime.Now.AddMonths(-3);
                if (date < minimumMonth) return true;
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
