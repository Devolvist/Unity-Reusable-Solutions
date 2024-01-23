using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.Events
{
    [CreateAssetMenu(menuName = "Reusable Solutions/Events/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        [Space]
        [Header("����� ��������� � �������.")]

        [Tooltip("�������� ����������� �� ������� ������ � �����������.")]
        [SerializeField] private bool _logDataClearing;

        [Tooltip("�������� ����������� � ��������.")]
        [SerializeField] private bool _logSubscription;

        [Tooltip("�������� ����������� �� �������.")]
        [SerializeField] private bool _logUnsubscription;

        [Tooltip("�������� �������������� � ��������� ��������.")]
        [SerializeField] private bool _logReSubscription;

        [Tooltip("�������� �������������� � ���������� ��� �����������.")]
        [SerializeField] private bool _logPublishingWithoutSubscribers;

        [Tooltip("�������� ������ ��������� � ��������������� ������ �����������.")]
        [SerializeField] private bool _logAccessingNonExistentSubscribersListError;

        [Tooltip("�������� ������ �������� ��������������� ����������.")]
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
                    Debug.LogWarning($"������� ���������� ���������� {newSubscriber.Method.Name} � ������ ����������� ������� {name}. ������ ��������.");

                return;
            }

            _subscribers.Add(newSubscriber);

            if (_logSubscription)
                Debug.Log($"{newSubscriber.Method.Name} ���������� �� ������� {name}.");
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
                    Debug.LogError($"������� ������� ��������������� ���������� {subscriber.Method.Name} �� ������ ����������� ������� {name}.");

                return;
            }

            _subscribers.Remove(subscriber);

            if (_logUnsubscription)
                Debug.Log($"{subscriber.Method.Name} ��������� �� ������� {name}.");
        }

        /// <returns>
        /// True - ������� ������������.
        /// False - �� ������������.
        /// </returns>
        public void Publish()
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
                    Debug.LogWarning($"������� ���������� ������� {name} ��� �����������. ������ ��������.");

                return;
            }

            for (int i = _subscribers.Count - 1; i >= 0; i--)
                _subscribers[i]?.Invoke();
        }

        public void LogInfo()
        {
            if (_subscribers == null | SubscribersCount == 0)
            {
                Debug.Log($"{name} �� ����� �����������.");
                return;
            }

            string message = $"��� �������: {name}\n���-�� �����������: {SubscribersCount}\n\n��������� ���������� � �����������:\n\n";

            for (int i = 0; i < _subscribers.Count; i++)
            {
                message += $"��� ������: {_subscribers[i].Method.Name}\n�����������: {_subscribers[i].Method.DeclaringType} \n\n";
            }

            Debug.Log(message);
        }

        public void ClearSubscribersData()
        {
            _subscribers?.Clear();

            if (_logDataClearing)
                Debug.Log($"������ � ����������� ������� {name} �������.");
        }

        private void LogAccessingNonExistentSubscribersListError()
        {
            Debug.LogError("��������� � ��������������� ������ �����������.");
        }
    }
}