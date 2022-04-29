using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace webcore_app.Core.Extensions
{
    public static class StringExtensions
    {
        const string regex = @"/[^\d]+/g";

        public static string Encoded(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var plainBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(plainBytes);
        }

        public static bool IsBase64(this string base64String)
        {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                // Handle the exception
            }
            return false;
        }

        public static string Decoded(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            if(!IsBase64(input))
            {
                return string.Empty;
            }

            byte[] data = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(data);
        }

        public static int ToInt(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            if (int.TryParse(Regex.Replace(input, regex, ""), out int oNumber))
                return oNumber;

            return 0;
        }

        public static decimal ToDecimal(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            if (decimal.TryParse(Regex.Replace(input, regex, ""), out decimal oNumber))
                return oNumber;

            return 0;
        }


        public static double ToDouble(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            if (double.TryParse(Regex.Replace(input, regex, ""), out double oNumber))
                return oNumber;

            return 0;
        }

        public static float ToFloat(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            if (float.TryParse(Regex.Replace(input, regex, ""), out float oNumber))
                return oNumber;

            return 0;
        }

        public static long ToLong(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            if (long.TryParse(Regex.Replace(input, regex, ""), out long oNumber))
                return oNumber;

            return 0;
        }

        public static Guid ToGuid(this string input)
        {
            if (!input.IsGuid()) return new Guid();


            Guid.TryParse(input, out Guid guid);

            return guid;
        }

        public static bool IsGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (Guid.TryParse(input, out _))
                return true;

            return false;
        }

        public static DateTime ToDateTime(this string input, String CultureFormat = "es-DO")
        {
            IFormatProvider culture = new System.Globalization.CultureInfo(CultureFormat, true);

            if (DateTime.TryParse(input, culture, System.Globalization.DateTimeStyles.None, out DateTime oDate))
                return oDate;

            return DateTime.MinValue;
        }

        public static DateTime ToDate(this string input, String CultureFormat = "es-DO")
        {
            IFormatProvider culture = new System.Globalization.CultureInfo(CultureFormat, true);

            if (DateTime.TryParse(input, culture, System.Globalization.DateTimeStyles.None, out DateTime oDate))
                return oDate.Date;

            return DateTime.MinValue;
        }

        public static T ParseJsonObject<T>(this string json) where T : class, new()
        {
            JObject jobject = JObject.Parse(json);
            return JsonConvert.DeserializeObject<T>(jobject.ToString());
        }

    }
}
