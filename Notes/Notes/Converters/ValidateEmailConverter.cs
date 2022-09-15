using System;
using System.Globalization;
using System.Net.Mail;
using Xamarin.Forms;
using System.ComponentModel;
using Notes.Utils;

namespace Notes.Converters
{
    public class ValidateEmailConverter : IValueConverter
    {
        public ValidateEmailConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string)
            {
                return ValidatorUtils.EmailIsValid(value.ToString());
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

