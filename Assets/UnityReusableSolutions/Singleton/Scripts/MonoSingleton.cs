using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Singleton
{
    /// <summary>
    /// Глобально-доступный объект, гарантирующий,
    /// что в любой момент он будет единственным экземпляром своего типа, и существующим в любой сцене.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            // Инициализация глобального экземпляра.
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;

            DontDestroyOnLoad(gameObject);

            InitializeOnAwake();
        }

        /// <summary>
        /// Инициализация после создания глобального экземпляра.
        /// Для переопределения (не требует вызова базового варианта).
        /// </summary>
        protected virtual void InitializeOnAwake() { }

#if UNITY_EDITOR
        /// <summary>
        /// Присвоить активному экземпляру null, если он в данный момент иницииализирован.
        /// </summary>
        public static void ResetInstance()
        {
            if (Instance == null)
            {
                Debug.LogWarning($"Синглтона {typeof(T)} в данный момент не существует.");
                return;
            }

            Instance = null;
            Debug.Log($"Синглтон {typeof(T)} успешно аннулирован.");
        }
#endif
    }
}