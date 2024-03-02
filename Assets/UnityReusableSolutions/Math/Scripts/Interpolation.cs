using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Math
{
    public static class Interpolation
    {
        /// <summary>
        /// Получить коллекцию интерполированного диапазона значений.
        /// </summary>
        /// <param name="minValue">
        /// Мин. значение диапазона.
        /// </param>
        /// <param name="maxValue">
        /// Макс. значение диапазона.
        /// </param>
        /// <param name="valuesCount">
        /// Общее кол-во значений в диапазоне.
        /// </param>
        public static int[] GetRange(int minValue, int maxValue, int valuesCount)
        {
            if (minValue > maxValue)
            {
                Debug.LogError("Мин. значение не может быть больше максимального.");
                return null;
            }

            if (valuesCount > maxValue)
            {
                Debug.LogError("Общее кол-во значений не может быть больше максимального в диапазоне.");
                return null;
            }

            if (valuesCount < 2)
            {
                Debug.LogError("Общее кол-во значений не может быть меньше 2.");
                return null;
            }

            int[] interpolatedValues = new int[valuesCount];

            float interpolationStep = (float)(maxValue - minValue) / (valuesCount - 1);

            interpolatedValues[0] = minValue;
            interpolatedValues[valuesCount - 1] = maxValue;

            for (int i = 1; i < valuesCount - 1; i++)
            {
                interpolatedValues[i] = (int)(minValue + i * interpolationStep);
            }

            return interpolatedValues;
        }
    }
}