using System;
using System.Globalization;
using Notes.Utils;
using Xamarin.Forms;

namespace Notes.Converters
{
    public class ValidatePasswordConverter : IValueConverter
    {
        public ValidatePasswordConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return ValidatorUtils.PasswordIsValid(value.ToString());
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}

