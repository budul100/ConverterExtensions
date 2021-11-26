using ConverterExtensions;
using NUnit.Framework;
using System.Globalization;

namespace ConverterExtensionsTests
{
    public class Tests
    {
        #region Public Methods

        [Test]
        public void ConvertEmptyIntToInt()
        {
            var result = (default(int)).Convert<int, int>();

            Assert.IsTrue(result == default);
        }

        [Test]
        public void ConvertEmptyStringToInt()
        {
            var result = (default(string)).Convert<string, int>();

            Assert.IsTrue(result == default);
        }

        [Test]
        public void ConvertEmptyStringToString()
        {
            var result = (default(string)).Convert<string, string>();

            Assert.IsTrue(result == default);
        }

        [Test]
        public void ConvertNegativeToDoubleDE()
        {
            var result = "AAa-9.999,99bb".ToDouble(provider: CultureInfo.GetCultureInfo("de-DE"));

            Assert.IsTrue(result == -9999.99);
        }

        [Test]
        public void ConvertNegativeToDoubleEN()
        {
            var result = "AAa-9,999.99bb".ToDouble(provider: CultureInfo.GetCultureInfo("en-GB"));

            Assert.IsTrue(result == -9999.99);
        }

        [Test]
        public void ConvertNegativeToIntDE()
        {
            var result = "AAa-9999,99bb".ToInt(provider: CultureInfo.GetCultureInfo("de-DE"));

            Assert.IsTrue(result == -9999);
        }

        [Test]
        public void ConvertNegativeToIntEN()
        {
            var result = "AAa-9999.99bb".ToInt(provider: CultureInfo.GetCultureInfo("en-GB"));

            Assert.IsTrue(result == -9999);
        }

        [Test]
        public void ConvertToDoubleDE()
        {
            var result = "AAa9.999,99bb".ToDouble(provider: CultureInfo.GetCultureInfo("de-DE"));

            Assert.IsTrue(result == 9999.99);
        }

        [Test]
        public void ConvertToDoubleEN()
        {
            var result = "AAa9,999.99bb".ToDouble(provider: CultureInfo.GetCultureInfo("en-GB"));

            Assert.IsTrue(result == 9999.99);
        }

        [Test]
        public void ConvertToIntDE()
        {
            var result = "AAa9999,99bb".ToInt(provider: CultureInfo.GetCultureInfo("de-DE"));

            Assert.IsTrue(result == 9999);
        }

        [Test]
        public void ConvertToIntEN()
        {
            var result = "AAa9999.99bb".ToInt(provider: CultureInfo.GetCultureInfo("en-GB"));

            Assert.IsTrue(result == 9999);
        }

        #endregion Public Methods
    }
}