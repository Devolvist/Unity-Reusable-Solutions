namespace Devolvist.UnityReusableSolutions.Math
{
    public static class PercentageCalculator 
    {
        public static int GetPercentageValue(int currentValue, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                return 0;

            if (currentValue <= minValue)
                return 0;

            if (currentValue >= maxValue)
                return 100;

            int totalDifference = maxValue - minValue;

            if (totalDifference == 0)
                return 0;

            int differenceBetweenCurrentAndMinValues = currentValue - minValue;

            int percentage = (int)((float)differenceBetweenCurrentAndMinValues / totalDifference * 100);
            return percentage;
        }

        public static int GetPercentageValue(float currentValue, float minValue, float maxValue)
        {
            if (minValue > maxValue)
                return 0;

            if (currentValue <= minValue)
                return 0;

            if (currentValue >= maxValue)
                return 100;

            float totalDifference = maxValue - minValue;

            if (totalDifference == 0)
                return 0;

            float differenceBetweenCurrentAndMinValues = currentValue - minValue;

            int percentage = (int)(differenceBetweenCurrentAndMinValues / totalDifference * 100);
            return percentage;
        }

        public static int GetValueFromPercentage(int percentage, int minValue, int maxValue)
        {
            if (minValue > maxValue)
                return minValue;

            if (percentage <= 0)
                return minValue;

            if (percentage >= 100)
                return maxValue;

            float totalDifference = maxValue - minValue;
            float intermediateResult = totalDifference * percentage / 100;

            int result = (int)(minValue + intermediateResult);
            return result;
        }

        public static float GetValueFromPercentage(int percentage, float minValue, float maxValue)
        {
            if (minValue > maxValue)
                return minValue;

            if (percentage <= 0)
                return minValue;

            if (percentage >= 100)
                return maxValue;

            float range = maxValue - minValue;
            float valueInRange = range * percentage / 100;

            float result = (float)(minValue + valueInRange);
            return result;
        }
    }
}