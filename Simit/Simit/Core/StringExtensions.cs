using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Simit.Core
{
    public static class StringExtensions
    {
        public static string ToNotNull(this string text, string defaultValue = "")
        {
            return string.IsNullOrWhiteSpace(text) ? defaultValue : text;
        }

        public static string ToNotNull(this object text, string defaultValue = "")
        {
            return text != null
                ? string.IsNullOrWhiteSpace(text.ToString())
                    ? defaultValue
                    : text.ToString()
                : defaultValue;
        }

        public static string ExtractNumber(this string text)
        {
            return new string(text.Where(char.IsDigit).ToArray());
            ;
        }

        public static string Repeat(this string text, int count)
        {
            return new StringBuilder(text.Length * count).Insert(0, text, count).ToString();
        }

        public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this IEnumerable<T> source, Func<T, T, bool> predicate)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    yield break;

                var currentGroup = new List<T> {iterator.Current};
                while (iterator.MoveNext())
                    if (predicate(currentGroup.Last(), iterator.Current))
                    {
                        currentGroup.Add(iterator.Current);
                    }
                    else
                    {
                        yield return currentGroup;
                        currentGroup = new List<T> {iterator.Current};
                    }

                yield return currentGroup;
            }
        }

        public static bool IsValidEmail(this string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress))
                return false;

            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #region numeri, prezzi e date

        public static int ToInt(this string numero, int defVal = 0)
        {
            if (!int.TryParse(numero, out var num)) num = defVal;

            return num;
        }

        public static int ToInt(this object numero, int defVal = 0)
        {
            return numero?.ToString().ToInt() ?? defVal;
        }


        public static double ToDbl(this string numero, double defVal = 0)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return defVal;

            var style   = NumberStyles.Float;
            var culture = CultureInfo.CreateSpecificCulture("it-IT");

            if (!double.TryParse(numero.Replace(".", ","), style, culture, out var num)) num = defVal;

            return num;
        }

        public static double ToDbl(this object numero, double defVal = 0)
        {
            return numero?.ToString().ToDbl() ?? defVal;
        }

        public static string ToStringPrice(this decimal prezzo)
        {
            return prezzo == 0
                ? "n/a"
                : prezzo.ToString("G29") + " €";
        }

        public static string ToStringNotZero(this double numero, string defVal = "")
        {
            return numero == 0.00
                ? defVal
                : numero.ToString();
        }

        public static DateTime? ToDateTime(this string data, DateTime? defVal = null)
        {
            return !DateTime.TryParse(data, out var myDate) ? defVal : myDate;
        }

        #endregion

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
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
    }

}
