using Devolvist.UnityReusableSolutions.Math;
using NUnit.Framework;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("Math")]
    public class InterpolationTest
    {
        [Test]
        [Description("Кол-во значений в полученном диапазоне равно запрошенному.")]
        public void IsValuesCountInReturnedRangeEqualsRequested()
        {
            for (int i = 2; i <= 100; i++)
            {
                int[] range = Interpolation.GetRange(minValue: 1, maxValue: 100, valuesCount: i);
                Assert.AreEqual(i, range.Length);
            }
        }     
    }
}