using System;
using System.Globalization;
using Xamarin.Forms;

namespace Notes.Converters
{
    public class ValidateLengthUsernameConverter : IValueConverter
    {
        public ValidateLengthUsernameConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && ((string)value).Length > 4)
            {
                return true;
            } else
            {
                return false;
            }
                
                

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}

