using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ConverterExtensions
{
    public static class NumberConverters
    {
        #region Private Fields

        private static readonly Regex decimalRegex = new Regex(@"(\d*(\,|\.))*\d+");
        private static readonly Regex integerRegex = new Regex(@"\d+");

        #endregion Private Fields

        #region Public Methods

        public static decimal ToDecimal(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = input.ToNullableDecimal(
                style: style,
                provider: provider);

            return result ?? 0;
        }

        public static double ToDouble(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = input.ToNullableDouble(
                style: style,
                provider: provider);

            return result ?? 0;
        }

        public static int ToInt(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = input.ToNullableInt(
                style: style,
                provider: provider);

            return result ?? 0;
        }

        public static long ToLong(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = input.ToNullableLong(
                style: style,
                provider: provider);

            return result ?? 0;
        }

        public static decimal? ToNullableDecimal(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = default(decimal?);

            provider = provider ?? CultureInfo.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = GetDecimalMatch(input);

                if (match.Success
                    && decimal.TryParse(
                        s: match.Value,
                        style: style,
                        provider: provider,
                        result: out decimal number))
                {
                    result = number;
                }
            }

            return result;
        }

        public static double? ToNullableDouble(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = default(double?);

            provider = provider ?? CultureInfo.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = GetDecimalMatch(input);

                if (match.Success
                    && double.TryParse(
                        s: match.Value,
                        style: style,
                        provider: provider,
                        result: out double number))
                {
                    result = number;
                }
            }

            return result;
        }

        public static int? ToNullableInt(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = default(int?);

            provider = provider ?? CultureInfo.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = GetIntegerMatch(input);

                if (match.Success
                    && int.TryParse(
                        s: match.Value,
                        style: style,
                        provider: provider,
                        result: out int number))
                {
                    result = number;
                }
            }

            return result;
        }

        public static long? ToNullableLong(this string input, NumberStyles style = NumberStyles.Number,
            IFormatProvider provider = default)
        {
            var result = default(long?);

            provider = provider ?? CultureInfo.CurrentCulture;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = GetIntegerMatch(input);

                if (match.Success
                    && long.TryParse(
                        s: match.Value,
                        style: style,
                        provider: provider,
                        result: out long number))
                {
                    result = number;
                }
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static Match GetDecimalMatch(string input)
        {
            return decimalRegex.Match(input);
        }

        private static Match GetIntegerMatch(string input)
        {
            return integerRegex.Match(input);
        }

        #endregion Private Methods
    }
}