using UnityTime = UnityEngine.Time;

namespace Devolvist.UnityReusableSolutions.Time
{
    /// <summary>
    /// Оболочка для взаимодействия с UnityEngine.Time.timeScale
    /// </summary>
    public static class GlobalTimePauseService
    {
        private const float DEFAULT_TIME_SCALE = 1.0f;

        public static bool IsPaused => UnityTime.timeScale == 0;

        public static void Pause() => UnityTime.timeScale = 0;

        public static void Unpause() => UnityTime.timeScale = DEFAULT_TIME_SCALE;
    }
}