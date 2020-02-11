using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Extensions
{
    public static class ConverterExtensions
    {
        #region Private Fields

        private const string RegexNumbers = @"\d+";

        #endregion Private Fields

        #region Public Methods

        public static U Convert<T, U>(this T input)
            where T : IConvertible
        {
            object result = default(U);

            if (typeof(T) == typeof(U))
            {
                result = input;
            }
            else
            {
                var type = Nullable.GetUnderlyingType(typeof(U)) ?? typeof(U);

                if (typeof(T) != typeof(string)
                    || !string.IsNullOrWhiteSpace(input as string))
                {
                    result = System.Convert.ChangeType(input, type);
                }
            }

            return (U)result;
        }

        public static IEnumerable<T> Split<T>(this string value, char delimiter, bool excludeEmpties = false)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var splitted = value
                    .Split(delimiter)
                    .Where(v => !(excludeEmpties && !string.IsNullOrWhiteSpace(v))).ToArray();

                foreach (var s in splitted)
                {
                    yield return s.Trim().Convert<string, T>();
                }
            }
        }

        public static decimal ToDecimal(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = null)
        {
            var result = input.ToNullableDecimal(
                style: style,
                provider: provider) ?? 0;

            return result;
        }

        public static double ToDouble(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = null)
        {
            var result = input.ToNullableDouble(
                style: style,
                provider: provider) ?? 0;

            return result;
        }

        public static T ToEnum<T>(this string value)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Enum.TryParse(
                value: value,
                ignoreCase: true,
                result: out T result);

            return result;
        }

        public static int ToInt(this string input, NumberStyles style = NumberStyles.Integer,
            IFormatProvider provider = null)
        {
            var result = input.ToNullableInt(
                style: style,
                provider: provider) ?? 0;

            return result;
        }

        public static long? ToLong(this string value)
        {
            var result = default(long?);

            if (!string.IsNullOrWhiteSpace(value))
            {
                var text = Regex.Match(
                    input: value,
                    pattern: RegexNumbers,
                    options: RegexOptions.IgnoreCase).Value;

                if (long.TryParse(text, out long conversion))
                {
                    result = conversion;
                }
            }

            return result;
        }

        public static long ToLong(this string value, long backup)
        {
            return value.ToLong() ?? backup;
        }

        public static decimal? ToNullableDecimal(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = null)
        {
            var result = default(decimal?);

            var currentProvider = provider ?? CultureInfo.InvariantCulture;

            if (decimal.TryParse(
                s: input,
                style: style,
                provider: currentProvider,
                result: out decimal num))
            {
                result = num;
            }

            return result;
        }

        public static double? ToNullableDouble(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = null)
        {
            var result = default(double?);

            var currentProvider = provider ?? CultureInfo.InvariantCulture;

            if (double.TryParse(
                s: input,
                style: style,
                provider: currentProvider,
                result: out double num))
            {
                result = num;
            }

            return result;
        }

        public static int? ToNullableInt(this string input, NumberStyles style = NumberStyles.Integer,
            IFormatProvider provider = null)
        {
            var result = default(int?);

            var currentProvider = provider ?? CultureInfo.InvariantCulture;

            if (int.TryParse(
                s: input,
                style: style,
                provider: currentProvider,
                result: out int num))
            {
                result = num;
            }

            return result;
        }

        #endregion Public Methods
    }
}