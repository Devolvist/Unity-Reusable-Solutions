using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Events
{
    [CreateAssetMenu(menuName = "Reusable Solutions/Events/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        [Space]
        [Header("Вывод сообщений в консоль.")]

        [Tooltip("Выводить уведомление об очистке данных о подписчиках.")]
        [SerializeField] private bool _logDataClearing;

        [Tooltip("Выводить уведомления о подписке.")]
        [SerializeField] private bool _logSubscription;

        [Tooltip("Выводить уведомления об отписке.")]
        [SerializeField] private bool _logUnsubscription;

        [Tooltip("Выводить предупреждение о повторной подписке.")]
        [SerializeField] private bool _logReSubscription;

        [Tooltip("Выводить предупреждение о публикации без подписчиков.")]
        [SerializeField] private bool _logPublishingWithoutSubscribers;

        [Tooltip("Выводить ошибку обращения к несуществующему списку подписчиков.")]
        [SerializeField] private bool _logAccessingNonExistentSubscribersListError;

        [Tooltip("Выводить ошибку удаления несуществующего подписчика.")]
        [SerializeField] private bool _logNonExistentSubscriberDeleting;

        private List<Action> _subscribers;

        public int SubscribersCount
        {
            get
            {
                if (_subscribers == null)
                    return 0;

                return _subscribers.Count;
            }
        }

        private void Reset()
        {
            ClearSubscribersData();
        }

        private void OnDisable()
        {
            ClearSubscribersData();
        }

        public void Subscribe(Action newSubscriber)
        {
            _subscribers ??= new List<Action>();

            if (_subscribers.Contains(newSubscriber))
            {
                if (_logReSubscription)
                    Debug.LogWarning($"Попытка повторного добавления {newSubscriber.Method.Name} в список подписчиков события {name}. Отмена операции.");

                return;
            }

            _subscribers.Add(newSubscriber);

            if (_logSubscription)
                Debug.Log($"{newSubscriber.Method.Name} подписался на событие {name}.");
        }

        public void Unsubscribe(Action subscriber)
        {
            if (_subscribers == null)
            {
                if (_logAccessingNonExistentSubscribersListError)
                    LogAccessingNonExistentSubscribersListError();

                return;
            }

            if (!_subscribers.Contains(subscriber))
            {
                if (_logNonExistentSubscriberDeleting)
                    Debug.LogError($"Попытка удалить несуществующего подписчика {subscriber.Method.Name} из списка подписчиков события {name}.");

                return;
            }

            _subscribers.Remove(subscriber);

            if (_logUnsubscription)
                Debug.Log($"{subscriber.Method.Name} отписался от события {name}.");
        }

        /// <returns>
        /// True - успешно инициировано.
        /// False - не инициировано.
        /// </returns>
        public virtual void Publish()
        {
            if (_subscribers == null)
            {
                if (_logAccessingNonExistentSubscribersListError)
                    LogAccessingNonExistentSubscribersListError();

                return;
            }

            if (_subscribers.Count == 0)
            {
                if (_logPublishingWithoutSubscribers)
                    Debug.LogWarning($"Попытка публикации события {name} без подписчиков. Отмена операции.");

                return;
            }

            for (int i = _subscribers.Count - 1; i >= 0; i--)
                _subscribers[i]?.Invoke();
        }

        public void LogInfo()
        {
            if (_subscribers == null | SubscribersCount == 0)
            {
                Debug.Log($"{name} не имеет подписчиков.");
                return;
            }

            string message = $"Имя события: {name}\nКол-во подписчиков: {SubscribersCount}\n\nПодробная информация о подписчиках:\n\n";

            for (int i = 0; i < _subscribers.Count; i++)
            {
                message += $"Имя метода: {_subscribers[i].Method.Name}\nПринадлежит: {_subscribers[i].Method.DeclaringType} \n\n";
            }

            Debug.Log(message);
        }

        public void ClearSubscribersData()
        {
            _subscribers?.Clear();

            if (_logDataClearing)
                Debug.Log($"Данные о подписчиках события {name} очищены.");
        }

        private void LogAccessingNonExistentSubscribersListError()
        {
            Debug.LogError("Обращение к несуществующему списку подписчиков.");
        }
    }
}