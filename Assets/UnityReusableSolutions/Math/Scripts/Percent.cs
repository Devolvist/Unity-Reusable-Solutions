using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Math
{
    /// <summary>
    /// Значение процента от 0 до 100.
    /// </summary>
    public struct Percent
    {
        private int _value;

        public int Value
        {
            get => _value;
            set => _value = Validate(value);
        }

        /// <summary>
        /// Преобразовать в процент значение от 0 до 1.0f.
        /// </summary>
        public static Percent From01(float value)
        {
            Percent result =
                new() { Value = Mathf.RoundToInt(Mathf.Clamp(value, 0f, 1f) * 100f) };

            if (value < 0 || value > 1.0f)
                Debug.LogWarning($"Входное значение [{value}] преобразовано в {result.Value}]");

            return result;
        }

        /// <summary>
        /// Преобразовать в значение от 0 до 1.0f.
        /// </summary>
        public float To01()
        {
            return Value / 100f;
        }

        /// <summary>
        /// Убедиться что входное значение от 0 до 100.
        /// </summary>
        /// <returns>
        /// Значение от 0 до 100.
        /// </returns>
        private int Validate(int inputValue)
        {
            int correctResult = Mathf.Clamp(inputValue, 0, 100);

            if (inputValue < 0 || inputValue > 100)
                Debug.LogWarning($"Входное значение [{inputValue}] приведено к {correctResult}%.");

            return correctResult;
        }
    }
}
