using ConverterExtensions;
using NUnit.Framework;

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

        #endregion Public Methods
    }
}