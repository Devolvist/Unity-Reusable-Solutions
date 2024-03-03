using Devolvist.UnityReusableSolutions.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Devolvist.UnityReusableSolutions.Internet
{
    /// <summary>
    /// Соединение с интернетом.
    /// </summary>
    public class InternetConnection : MonoSingleton<InternetConnection>
    {
        [Tooltip("Список интернет-адресов для проверки соединения с ними.")]
        private readonly List<string> _checkableUrls =
            new List<string>()
            {
                "https://google.com",
                "https://yandex.com",
                "https://facebook.com",
                "https://unity.com",
                "https://www.github.com"
            };

        public void IsAvailable(Action<bool> result)
        {
            if (_checkableUrls == null | _checkableUrls.Count == 0)
            {
                Debug.LogWarning("Список адресов для проверки пуст.");
                result.Invoke(false);
                return;
            }

            StartCoroutine(Check(success => result.Invoke(success)));
        }

        private IEnumerator Check(Action<bool> resultCallback)
        {
            for (int i = 0; i < _checkableUrls.Count; i++)
            {
                if (_checkableUrls[i] == null | _checkableUrls[i] == string.Empty)
                    continue;

                var webRequest = UnityWebRequest.Get(_checkableUrls[i]);

                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    resultCallback?.Invoke(true);
                    yield break;
                }
            }

            resultCallback?.Invoke(false);
        }
    }
}