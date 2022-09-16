using System;
using System.Globalization;
using Xamarin.Forms;

namespace Notes.Converters
{
    public class ValidateLengthPasswordConverter : IValueConverter
    {
        public ValidateLengthPasswordConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && ((string)value).Length > 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}

