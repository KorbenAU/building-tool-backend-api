using System.Text.RegularExpressions;

namespace Microservice.Business.Extensions
{
    public static class StringExtensions
    {
        public static bool MeetsPasswordPolicy(this string password)
        {
            if (password.Length < Constants.PasswordMinimumLength)
                return false;

            if (UpperCaseCount(password) < Constants.PasswordMinimumUpperCase)
                return false;

            if (LowerCaseCount(password) < Constants.PasswordMinimumLowerCase)
                return false;

            if (NumericCount(password) < Constants.PasswordMinimumNumeric &&
                NonAlphaCount(password) < Constants.PasswordMinimumNonAlpha)
                return false;

            return true;
        }

        private static int UpperCaseCount(string password)
        {
            return Regex.Matches(password, "[A-Z]").Count;
        }

        private static int LowerCaseCount(string password)
        {
            return Regex.Matches(password, "[a-z]").Count;
        }

        private static int NumericCount(string password)
        {
            return Regex.Matches(password, "[0-9]").Count;
        }

        private static int NonAlphaCount(string password)
        {
            return Regex.Matches(password, @"[^0-9a-zA-Z\._]").Count;
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNullOrEmpty(this string original)
        {
            return string.IsNullOrEmpty(original);
        }

        public static bool IsNotNullOrEmpty(this string original)
        {
            return !string.IsNullOrEmpty(original);
        }
    }
}