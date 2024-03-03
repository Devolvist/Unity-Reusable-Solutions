namespace Devolvist.UnityReusableSolutions.StringUtilities
{
    /// <summary>
    /// Форматирование чисел от 1000 в удобочитаемый формат (1К, 24,5К, 1,5M и т.д.)
    /// </summary>
    public static class NumberViewFormatter
    {
        public static string Format(int value)
        {
            return FormatCore(value);
        }

        public static string Format(float value)
        {
            return FormatCore(value);
        }

        public static string Format(double value)
        {
            return FormatCore(value);
        }

        private static string FormatCore(double value)
        {
            if (value < 1000)
            {
                return value.ToString();
            }

            else if (value < 1_000_000)
            {
                if (value % 1000 == 0)
                {
                    return $"{value / 1000:F0}K";
                }
                return $"{value / 1000:F1}K";
            }

            else if (value < 1_000_000_000)
            {
                if (value % 1_000_000 == 0)
                {
                    return $"{value / 1_000_000:F0}M";
                }
                return $"{value / 1_000_000:F1}M";
            }

            else if (value < 1_000_000_000_000)
            {
                if (value % 1_000_000_000 == 0)
                {
                    return $"{value / 1_000_000_000:F0}B";
                }
                return $"{value / 1_000_000_000:F1}B";
            }

            else
            {
                if (value % 1_000_000_000_000 == 0)
                {
                    return $"{value / 1_000_000_000_000:F0}T";
                }
                return $"{value / 1_000_000_000_000:F1}T";
            }
        }
    }
}