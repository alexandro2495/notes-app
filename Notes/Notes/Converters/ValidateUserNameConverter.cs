using System;
using System.Globalization;
using Notes.Utils;
using Xamarin.Forms;

namespace Notes.Converters
{
    public class ValidateUserNameConverter : IValueConverter
    {
        public ValidateUserNameConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return ValidatorUtils.UserNameIsValid(value.ToString());
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

