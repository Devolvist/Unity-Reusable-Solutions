using Devolvist.UnityReusableSolutions.StringUtilities;
using NUnit.Framework;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("String Utilities")]
    public class NumberViewFormatterTests
    {
        [Test]
        [Description("Форматированная строка совпадает с ожидаемой")]
        public void FormattedStringEqualsExpected()
        {
            double[] testValues = {
                1000,
                1100,
                1_000_000,
                1_100_000,
                1_000_000_000,
                1_100_000_000,
                1_000_000_000_000,
                1_100_000_000_000
            };

            string[] expectedResults = { "1K", "1,1K", "1M", "1,1M", "1B", "1,1B", "1T", "1,1T" };

            for (int i = 0; i < testValues.Length; i++)
            {
                Assert.AreEqual(expectedResults[i], NumberViewFormatter.Format(testValues[i]));
            }
        }
    }
}