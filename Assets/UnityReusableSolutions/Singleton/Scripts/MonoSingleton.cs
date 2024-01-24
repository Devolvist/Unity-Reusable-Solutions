using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Singleton
{
    /// <summary>
    /// √лобально-доступный объект, гарантирующий,
    /// что в любой момент он будет единственным экземпл€ром своего типа, и существующим в любой сцене.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            // »нициализаци€ глобального экземпл€ра.
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
        /// »нициализаци€ после создани€ глобального экземпл€ра.
        /// ƒл€ переопределени€ (не требует вызова базового варианта).
        /// </summary>
        protected virtual void InitializeOnAwake() { }

#if UNITY_EDITOR
        /// <summary>
        /// ѕрисвоить активному экземпл€ру null, если он в данный момент иницииализирован.
        /// </summary>
        public static void ResetInstance()
        {
            if (Instance == null)
            {
                Debug.LogWarning($"—инглтона {typeof(T)} в данный момент не существует.");
                return;
            }

            Instance = null;
            Debug.Log($"—инглтон {typeof(T)} успешно аннулирован.");
        }
#endif
    }
}