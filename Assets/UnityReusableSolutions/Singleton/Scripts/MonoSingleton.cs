using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Singleton
{
    /// <summary>
    /// ���������-��������� ������, �������������,
    /// ��� � ����� ������ �� ����� ������������ ����������� ������ ����, � ������������ � ����� �����.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            // ������������� ����������� ����������.
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
        /// ������������� ����� �������� ����������� ����������.
        /// ��� ��������������� (�� ������� ������ �������� ��������).
        /// </summary>
        protected virtual void InitializeOnAwake() { }
    }
}