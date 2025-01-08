using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Utility
{
    public static class Utils
    {
        public static decimal ToTruncateDecimal(this decimal value) => Math.Round(value, 2, MidpointRounding.ToZero);

        //public static string ToFormatDate(this DateTime date) => date.ToString("h:mmtt MM/dd/yy").ToLower();
        //public static string ToFormatDate(this DateTime date) => date.ToString("h:mmtt dd/MM/yy").ToLower();
        public static string ToFormatDate(this DateTime date) => date.ToString("h:mmtt dd-MM-yyyy").ToLower();


        public static T AsObject<T>(this IFormCollection pairs) where T : class, new()
        {
            string jsonString = $"{{{string.Join(",", pairs.Select(x => $"\"{x.Key}\" : \"{x.Value}\""))}}}";

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public static string GetCurrentDomain(this HttpContext httpContext)
        {

            // Get the current HTTP context
            var request = httpContext.Request;

            // Get the port part of the URL if available
            var port = request.Host.Port.HasValue ? $":{request.Host.Port}" : "";

            // Construct the base URL
            var baseUrl = $"{request.Scheme}://{request.Host.Host}{port}{request.PathBase}";

            return baseUrl;
            //string domain = request.Scheme + Uri.SchemeDelimiter + request.Host + (request.IsDefaultPort ? "" : ":" + request.Url.Port);
        }
        public static void RedirectWithData(NameValueCollection data, string url, HttpContext httpContext)
        {
            try
            {
                HttpResponse response = httpContext.Response;
                response.Clear();

                StringBuilder s = new StringBuilder();
                s.Append("<html>");
                s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
                s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
                foreach (string key in data)
                {
                    s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
                }
                s.Append("</form></body></html>");
                response.WriteAsync(s.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static void RedirectWithJsonData(NameValueCollection data, string url, HttpContext httpContext)
        {
            HttpResponse response = httpContext.Response;
            response.Clear();

            StringBuilder s = new StringBuilder();
            foreach (string key in data)
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
            }
            s.Append("</form></body></html>");
            response.WriteAsync(s.ToString());
        }



        //Uri url = new Uri("http://localhost/rest/something/browse").
        //  AddQuery("page", "0").
        //  AddQuery("pageSize", "200");
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(uri);

            // this code block is taken from httpValueCollection.ToString() method
            // and modified so it encodes strings with HttpUtility.UrlEncode
            if (httpValueCollection.Count == 0)
                ub.Query = String.Empty;
            else
            {
                var sb = new StringBuilder();

                for (int i = 0; i < httpValueCollection.Count; i++)
                {
                    string text = httpValueCollection.GetKey(i);
                    {
                        text = HttpUtility.UrlEncode(text);

                        string val = (text != null) ? (text + "=") : string.Empty;
                        string[] vals = httpValueCollection.GetValues(i);

                        if (sb.Length > 0)
                            sb.Append('&');

                        if (vals == null || vals.Length == 0)
                            sb.Append(val);
                        else
                        {
                            if (vals.Length == 1)
                            {
                                sb.Append(val);
                                sb.Append(HttpUtility.UrlEncode(vals[0]));
                            }
                            else
                            {
                                for (int j = 0; j < vals.Length; j++)
                                {
                                    if (j > 0)
                                        sb.Append('&');

                                    sb.Append(val);
                                    sb.Append(HttpUtility.UrlEncode(vals[j]));
                                }
                            }
                        }
                    }
                }

                ub.Query = sb.ToString();
            }

            return ub.Uri;
        }
        //public static decimal ToTruncateDecimal(this decimal value) => Math.Round(value, 2, MidpointRounding.ToZero);

        public static string MaskAccountNumber(string input)
        {
            if (input.Length < 10)
            {
                // Handle invalid input
                throw new Exception("invalid account number input");
            }

            // Extract first 6 and last 4 digits
            string firstSix = input.Substring(0, 6);
            string lastFour = input.Substring(input.Length - 4, 4);

            // Calculate the number of masked characters
            int maskedLength = input.Length - 10;

            // Create the masked string
            string maskedMiddle = new string('*', maskedLength);

            // Concatenate the parts to form the final result
            string maskedString = firstSix + maskedMiddle + lastFour;

            return maskedString;

        }
        public static string FormatDate(this DateTime? date)
        {
            return date?.ToString("dd-MM-yyyy");
        }
        public static string ReplaceFirstAndLastComma(string input)
        {
            // Remove leading comma
            if (input.StartsWith(","))
            {
                input = input.Substring(1);
            }

            // Remove leading space
            if (input.StartsWith(" "))
            {
                input = input.Substring(1);
            }

            // Remove trailing comma
            if (input.EndsWith(","))
            {
                input = input.Substring(0, input.Length - 1);
            }
            if (input.EndsWith(", "))
            {
                input = input.Substring(0, input.Length - 2);
            }
            // Replace remaining commas with empty value
            //input = input.Replace(",", "");

            return input;
        }
        public static string ConvertToBanglaNumber(int number)
        {
            string[] banglaDigits = { "০", "১", "২", "৩", "৪", "৫", "৬", "৭", "৮", "৯" };
            List<string> banglaNumberList = new List<string>();

            while (number > 0)
            {
                int digit = number % 10;
                banglaNumberList.Add(banglaDigits[digit]);
                number = number / 10;
            }

            banglaNumberList.Reverse();
            return string.Join("", banglaNumberList);
        }
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <returns>Age in years today. 0 is returned for a future date of birth.</returns>
        public static int Age(this DateTime birthDate)
        {
            return Age(birthDate, DateTime.Today);
        }

        /// <summary>
        /// Calculates the age in years of the current System.DateTime object on a later date.
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <param name="laterDate">The date on which to calculate the age.</param>
        /// <returns>Age in years on a later day. 0 is returned as minimum.</returns>
        public static int Age(this DateTime birthDate, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - birthDate.Year;

            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }

            return age;
        }
        public static string GetLocalIpAddress()
        {
            UnicastIPAddressInformation mostSuitableIp = null;

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var network in networkInterfaces)
            {
                if (network.OperationalStatus != OperationalStatus.Up)
                    continue;

                var properties = network.GetIPProperties();

                if (properties.GatewayAddresses.Count == 0)
                    continue;

                foreach (var address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    if (!address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }

                    // The best IP is the IP got from DHCP server
                    if (address.PrefixOrigin != PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }

                    return address.Address.ToString();
                }
            }

            return mostSuitableIp != null
                ? mostSuitableIp.Address.ToString()
                : "";
        }



        public static string ConvertStringToHash1(string input)
        {
            using (var sha1 = SHA1.Create())
                return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input))).ToLower();
        }
        public static bool SplitStringCheck(this string splitStr, string checkStr)
        {
            try
            {
                if (string.IsNullOrEmpty(splitStr))
                {
                    return false;
                }
                var listStr = splitStr.Split(",").ToList();
                var result = listStr.Where(e => e == checkStr).FirstOrDefault();
                if (result == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static IEnumerable<TSource> DistinctByList<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static IEnumerable<T> DistinctByValue<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
        //public static IEnumerable<t> DistinctByValue<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        //{
        //    return list.GroupBy(propertySelector).Select(x => x.First());
        //}
        public static decimal FeePerchantageAmount(decimal amount, decimal perchant)
        {
            decimal result = (amount * perchant) / 100;
            return result;
        }
        public static string ConvertHashSha1(string stringToHash)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash))).Replace("-", "");
            }
        }
        public static string ConvertSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Use BitConverter to convert the hash bytes to a hexadecimal string
                string hashedString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashedString;
            }
        }
        public static decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }
        //public static double RoundUp(double input, int places)
        //{
        //    double multiplier = Math.Pow(10, Convert.ToDouble(places));
        //    return Math.Ceiling(input * multiplier) / multiplier;
        //}

        public static decimal RoundUp(decimal input, int places)
        {
            decimal multiplier = Convert.ToDecimal(Math.Pow(10, Convert.ToDouble(places)));
            return Math.Round(input * multiplier) / multiplier;
        }
        public static DateTime GetOtpExpireDateTime(int minutes)
        {
            return DateTime.Now.AddMinutes(minutes);
        }
        public static string GenerateOTP()
        {
            return (new Random()).Next(100000, 999999).ToString();
        }


        public static string GenerateTransactionId()
        {
            var rrn = Utils.GetRRN();
            var otpRandom = new Random();
            char padLeft = Convert.ToChar(otpRandom.Next(1, 2).ToString());
            var num = Convert.ToInt64(rrn + otpRandom.Next(1, 99).ToString().PadLeft(2, padLeft));
            return DecimalToArbitrarySystem(num, 35);
        }



        public static string DecimalToArbitrarySystem(long decimalNumber, int radix)
        {
            const int BitsInLong = 64;
            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";   //abcdefghijklmnopqrstuvwxyz

            if (radix < 2 || radix > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new String(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }

        public static string GetRRN()
        {
            return DateTime.Now.ToString("yyMMddHHmmssfffff");
        }
        public static long GetLongRandom()
        {
            Random r = new Random();
            long number = r.NextInt64();
            return number;
        }
        public static string ConvertBase64ToString(string encodedString)
        {
            try
            {
                byte[] data = Convert.FromBase64String(encodedString);
                string decodedString = Encoding.UTF8.GetString(data);
                return decodedString;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }


        public static KeyValuePair<string, bool> FormatMobileNumber(String MobileNumber)
        {

            var mobileNumberFormat = MobileNumber.Substring(0, 3);
            if (mobileNumberFormat == "+88")
            {
                mobileNumberFormat = MobileNumber.Replace("+", String.Empty);
            }
            else if (mobileNumberFormat == "880")
            {
                mobileNumberFormat = MobileNumber.Replace(" ", String.Empty);
            }
            else
            {
                var imobileNumberFormat = "88" + MobileNumber;
                mobileNumberFormat = imobileNumberFormat.Replace(" ", String.Empty);//Trim();
            }

            var isMobileNumber = IsPhoneNumber(mobileNumberFormat);
            if (isMobileNumber == false)
            {
                return new KeyValuePair<string, bool>("Invalid number Format", false);
            }


            return new KeyValuePair<string, bool>(mobileNumberFormat, true);
        }
        public static (long Number, bool IsValid) ValidateMobileNumber(String MobileNumber)
        {
            var mobileNumberFormat = MobileNumber.Substring(0, 3);
            if (mobileNumberFormat == "+88")
            {
                mobileNumberFormat = MobileNumber.Replace("+", String.Empty);
            }
            else if (mobileNumberFormat == "880")
            {
                mobileNumberFormat = MobileNumber.Replace(" ", String.Empty);
            }
            else
            {
                var imobileNumberFormat = "88" + MobileNumber;
                mobileNumberFormat = imobileNumberFormat.Replace(" ", String.Empty);//Trim();
            }

            var isMobileNumber = IsPhoneNumber(mobileNumberFormat);
            if (isMobileNumber == false)
            {
                return (0, false);
            }
            return (Convert.ToInt64(mobileNumberFormat), true);
        }
        public static bool IsPhoneNumber(string number)
        {
            //return Regex.Match(number, @"^8801([13-9]\d{1})[\d]{7}$").Success;
            return Regex.Match(number, @"^01([13-9]\d{1})[\d]{7}$").Success;
            //Regex.Match(number, @"^\+8801([13-9]\d{1})[\d]{7}$").Success;
        }
        public static bool IsValidEmail(string email)
        {
            // Regular expression for validating an Email
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Using Regex class to validate the email
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }

    }
}
