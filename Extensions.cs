using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConverterExtensions
{
    public static class Extensions
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

        public static IEnumerable<T> Split<T>(this string value, string delimiters, bool excludeEmpties = false)
        {
            var separators = delimiters.ToCharArray();

            if (!string.IsNullOrWhiteSpace(value))
            {
                var splits = value
                    .Split(separators)
                    .Where(v => !(excludeEmpties && !string.IsNullOrWhiteSpace(v))).ToArray();

                foreach (var split in splits)
                {
                    yield return split.Trim().Convert<string, T>();
                }
            }
        }

        public static IEnumerable<T> Split<T>(this string value, char delimiter, bool excludeEmpties = false)
        {
            var result = value.Split<T>(
                delimiters: delimiter.ToString(),
                excludeEmpties: excludeEmpties).ToArray();

            return result;
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

        public static long ToLong(this string value, long backup = 0)
        {
            return value.ToNullableLong() ?? backup;
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

        public static long? ToNullableLong(this string value)
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

        #endregion Public Methods
    }
}