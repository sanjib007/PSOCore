using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.Extentions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// copy object properties
        /// </summary>
        /// <param name="self">this</param>
        /// <param name="parent">parent object</param>
        public static void CopyObjectFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
        public static string BankAccMaskKeepFirstSixLastFour(this string accountNo)
        {
            if (String.IsNullOrEmpty(accountNo))
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                if (accountNo.Length > 12)
                {
                    return sb.Clear().Append(accountNo.Substring(0, 6)).Append("".PadLeft(accountNo.Length - 10, '*')).Append(accountNo.Substring(accountNo.Length - 4)).ToString();
                }
                else
                {
                    return sb.Clear().Append("".PadLeft(accountNo.Length - 4, '*')).Append(accountNo.Substring(accountNo.Length - 4)).ToString();
                }
            }


        }
        public static string CardBin(this string CardNumber)
        {
            if (String.IsNullOrEmpty(CardNumber))
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                return sb.Clear().Append(CardNumber.Substring(0, 6)).ToString();
            }


        }
        public static string MobileNumberMax(this string accountNo)
        {
            StringBuilder sb = new StringBuilder();

            if (accountNo.Length > 12)
            {
                return sb.Clear().Append(accountNo.Substring(0, 6)).Append("".PadLeft(accountNo.Length - 10, '*')).Append(accountNo.Substring(accountNo.Length - 3)).ToString();
            }
            else
            {
                return sb.Clear().Append("".PadLeft(accountNo.Length - 4, '*')).Append(accountNo.Substring(accountNo.Length - 4)).ToString();
            }

        }
        public static string EmailMax(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }
            string pattern = @"(?<=[\w]{4})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(email, pattern, m => new string('*', m.Length));
            return result;
        }

        public static string DecemailToStringFormat(this decimal value)
        {
            return String.Format("{0:0.00}", value);
        }

        public static string DecemailToStringFormatExactlyTwoDigits(this decimal value)
        {
             decimal truncatedNumber = Math.Truncate(value * 100) / 100;
             return truncatedNumber.ToString("0.00");
        }

        
        //String.Format("{0:0.00}", transaction.TransactionAmount);

        public static T ToObject<T>(this IDictionary<string, object> source)
       where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
