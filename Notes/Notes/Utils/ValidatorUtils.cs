using System;
using System.Text.RegularExpressions;

namespace Notes.Utils
{
    public static class ValidatorUtils
    {

        static Regex ValidEmailRegex = CreateValidEmailRegex();
        static Regex ValidStrongPassword = CreateValidStrongPassword();
        static Regex ValidUserName = CreateValidUserName();

        private static Regex CreateValidStrongPassword()
        {
            string validStrongPasswordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";

            return new Regex(validStrongPasswordPattern, RegexOptions.IgnoreCase);
        }

        private static Regex CreateValidUserName()
        {
            string validUserNamePattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#_-]).{8,}$";

            return new Regex(validUserNamePattern, RegexOptions.IgnoreCase);
        }

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        internal static bool PasswordIsValid(string password)
        {
            bool isValid = ValidStrongPassword.IsMatch(password);

            return isValid;
        }

        internal static bool UserNameIsValid(string username)
        {
            bool isValid = ValidUserName.IsMatch(username);

            return isValid;
        }
    }
}


