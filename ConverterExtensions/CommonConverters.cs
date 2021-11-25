using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConverterExtensions
{
    public static class CommonConverters
    {
        #region Public Methods

        public static U Convert<T, U>(this T input)
            where T : IConvertible
        {
            object result = default(U);

            if (!EqualityComparer<T>.Default.Equals(
                x: input,
                y: default))
            {
                if (typeof(U) == typeof(T))
                {
                    result = input;
                }
                else
                {
                    if (typeof(T) != typeof(string)
                        || !string.IsNullOrWhiteSpace(input.ToString()))
                    {
                        var type = Nullable.GetUnderlyingType(typeof(U)) ?? typeof(U);

                        result = System.Convert.ChangeType(
                            value: input,
                            conversionType: type,
                            provider: CultureInfo.CurrentCulture);
                    }
                }
            }

            return (U)result;
        }

        public static T ToEnum<T>(this string value)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("The output type must be an enumerated type");
            }

            Enum.TryParse(
                value: value,
                ignoreCase: true,
                result: out T result);

            return result;
        }

        #endregion Public Methods
    }
}