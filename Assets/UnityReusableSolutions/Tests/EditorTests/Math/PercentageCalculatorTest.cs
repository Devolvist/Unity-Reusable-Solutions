using NUnit.Framework;
using UnityEngine;
using static Devolvist.UnityReusableSolutions.Math.PercentageCalculator;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("Math")]
    public class PercentageCalculatorTest
    {
        [Test]
        public void TestIntResult()
        {
            // Тест на 0%.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 0, minValue: 0, maxValue: 10));

            // Тест на 50%.
            Assert.AreEqual(50, GetPercentageValue(currentValue: 5, minValue: 0, maxValue: 10));

            // Тест на 100%.
            Assert.AreEqual(100, GetPercentageValue(currentValue: 10, minValue: 0, maxValue: 10));

            // Тест на от 0 до 50%.
            Assert.LessOrEqual(50, GetPercentageValue(currentValue: Random.Range(5, 11), minValue: 0, maxValue: 10));

            // Тест на от 50 до 100%.
            Assert.GreaterOrEqual(50, GetPercentageValue(currentValue: Random.Range(0, 6), minValue: 0, maxValue: 10));

            // Тесты возврата значения, исходя из известного процента, минимального и максимального значения.
            Assert.AreEqual(50, GetValueFromPercentage(percentage: 50, minValue: 0, maxValue: 100));
            Assert.AreEqual(0, GetValueFromPercentage(percentage: 0, minValue: 0, maxValue: 100));
            Assert.AreEqual(100, GetValueFromPercentage(percentage: 100, minValue: 0, maxValue: 100));


            // Обработка некорректных данных.

            // Минимальное значение больше максимального.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 10, minValue: 10, maxValue: 0));

            // Текущее значение меньше минимального.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 10, minValue: 12, maxValue: 100));

            // Текущее значение больше максимального.
            Assert.AreEqual(100, GetPercentageValue(currentValue: 10, minValue: 0, maxValue: 8));
        }

        [Test]
        public void TestFloatResult()
        {
            // Тест на 0%.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 0f, minValue: 0f, maxValue: 10f));

            // Тест на 50%.
            Assert.AreEqual(50, GetPercentageValue(currentValue: 5f, minValue: 0f, maxValue: 10f));

            // Тест на 100%.
            Assert.AreEqual(100, GetPercentageValue(currentValue: 10f, minValue: 0f, maxValue: 10f));

            // Тест на от 0 до 50%.
            Assert.LessOrEqual(50, GetPercentageValue(currentValue: Random.Range(5f, 10f), minValue: 0f, maxValue: 10f));

            // Тест на от 50 до 100%.
            Assert.GreaterOrEqual(50, GetPercentageValue(currentValue: Random.Range(0f, 5f), minValue: 0f, maxValue: 10f));

            // Тест на 0% в диапазоне от 0.1f до 1.0f.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 0.1f, minValue: 0.1f, maxValue: 1.0f));

            // Тест на 50% в диапазоне от 0.1f до 1.0f.
            Assert.AreEqual(50, GetPercentageValue(currentValue: 0.55f, minValue: 0.1f, maxValue: 1.0f));

            // Тест на 100% в диапазоне от 0.1f до 1.0f.
            Assert.AreEqual(100, GetPercentageValue(currentValue: 1.0f, minValue: 0.1f, maxValue: 1.0f));

            // Тест на от 0 до 50% в диапазоне от 0.1f до 1.0f.
            Assert.LessOrEqual(50, GetPercentageValue(currentValue: Random.Range(0.55f, 1.0f), minValue: 0.1f, maxValue: 1.0f));

            // Тест на от 50 до 100% в диапазоне от 0.1f до 1.0f.
            Assert.GreaterOrEqual(50, GetPercentageValue(currentValue: Random.Range(0.1f, 0.55f), minValue: 0.1f, maxValue: 1.0f));

            // Тесты возврата значения, исходя из известного процента, минимального и максимального значения.
            Assert.AreEqual(50.0f, GetValueFromPercentage(percentage: 50, minValue: 0.0f, maxValue: 100.0f));
            Assert.AreEqual(0.0f, GetValueFromPercentage(percentage: 0, minValue: 0.0f, maxValue: 100.0f));
            Assert.AreEqual(100, GetValueFromPercentage(percentage: 100, minValue: 0.0f, maxValue: 100.0f));


            // Обработка некорректных данных.

            // Минимальное значение больше максимального.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 10.0f, minValue: 10.0f, maxValue: 0f));

            // Текущее значение меньше минимального.
            Assert.AreEqual(0, GetPercentageValue(currentValue: 10.0f, minValue: 12.0f, maxValue: 100.0f));

            // Текущее значение больше максимального.
            Assert.AreEqual(100, GetPercentageValue(currentValue: 10.0f, minValue: 0f, maxValue: 8.0f));
        }
    }
}