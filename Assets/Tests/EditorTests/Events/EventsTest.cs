using Devolvist.UnityReusableSolutions.Events;
using NUnit.Framework;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("Events")]
    public class EventsTest
    {      
        [Test]
        [Description("0 подписчиков при создании экз. события.")]
        public void Is0SubscribersWhenInstanceCreated()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        [Description("1 подписчик при единоразовой подписке одного метода.")]
        public void Is1SubscriberWhenSubscribeSingle()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        [Description("1 подписчик после повторной подписки того же метода.")]
        public void Is1SubscriberWhenSubscribeTwice()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        [Description("0 подписчиков, когда один метод подписался и отписался.")]
        public void Is0SubscribersWhenSubscribeThenUnsubscribe()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        [Description("2 подписчика, когда два разных метода подписались.")]
        public void Is2SubscribersWhen2DifferentActionsSubscribes()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction_1);

            Assert.IsTrue(testableEvent.SubscribersCount == 2);
        }

        [Test]
        [Description("1 подписчик, когда два разных метода подписались, затем один из них отписался.")]
        public void Is1SubscriberWhen2DifferentActionsSubscribesThen1Unsubscribe()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction_1);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        [Description("2 подписчика, когда два разных клиента подписались.")]
        public void Is2SubscribersWhen2DifferentClientsSubscribed()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            MockSubscriber mockClass = new MockSubscriber();
            MockSubscriber mockClass1 = new MockSubscriber();
            
            testableEvent.Subscribe(mockClass.MockClassAction);
            testableEvent.Subscribe(mockClass1.MockClassAction);
            
            Assert.IsTrue(testableEvent.SubscribersCount == 2);
        }

        [Test]
        [Description("1 подписчик, когда два разных клиента подписались, затем один из них отписался.")]
        public void Is1SubscriberWhen2DifferentClientsSubscribeThen1Unsubscribed()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            MockSubscriber mockClass = new MockSubscriber();
            MockSubscriber mockClass1 = new MockSubscriber();

            testableEvent.Subscribe(mockClass.MockClassAction);
            testableEvent.Subscribe(mockClass1.MockClassAction);
            testableEvent.Unsubscribe(mockClass.MockClassAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        [Description("0 подписчиков, когда один метод подписался, затем отписался дважды.")]
        public void Is0SubscribersWhen1SubscribedThenUnsubscribedTwice()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        [Description("2 подписчика, когда два разных клиента подписались дважды.")]
        public void Is2SubscribersWhen2DifferentClientsSubscribedTwice()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            MockSubscriber mockClass = new MockSubscriber();
            MockSubscriber mockClass1 = new MockSubscriber();

            testableEvent.Subscribe(mockClass.MockClassAction);
            testableEvent.Subscribe(mockClass.MockClassAction);
            testableEvent.Subscribe(mockClass1.MockClassAction);
            testableEvent.Subscribe(mockClass1.MockClassAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 2);
        }

        [Test]
        [Description("0 подписчиков, когда данные о подписчиках очистились при наличии одного активного подписчика.")]
        public void Is0SubscribersWhenDataClearedWithActiveSubscriber()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.ClearSubscribersData();

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        [Description("Все подписчики уведомлены, когда один из подписчиков отписался в процессе перебора коллекции подпичиков.")]
        public void IsAllSubscribersInvokedWhen1SubscriberUnsubscribedAtProcess()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            MockSubscriber_1 subscriber = new MockSubscriber_1(testableEvent);
            MockSubscriber_2 subscriber1 = new MockSubscriber_2(testableEvent);

            testableEvent.Publish();

            Assert.IsTrue(testableEvent.SubscribersCount == 1 && subscriber1.IsEventHandled);
        }

        private void MockAction() { }
        private void MockAction_1() { }

        private class MockSubscriber
        {
            public void MockClassAction() { }
        }

        private class MockSubscriber_1
        {
            public ScriptableEvent Event { get; set; }

            public MockSubscriber_1(ScriptableEvent scriptableEvent)
            {
                Event = scriptableEvent;
                Event.Subscribe(HandleEventAndUnsubscribe);
            }

            public void HandleEventAndUnsubscribe()
            {
                Event.Unsubscribe(HandleEventAndUnsubscribe);
            }
        }

        private class MockSubscriber_2
        {
            public ScriptableEvent Event { get; set; }

            public bool IsEventHandled { get; private set; } = false;

            public MockSubscriber_2(ScriptableEvent scriptableEvent)
            {
                Event = scriptableEvent;
                Event.Subscribe(HandleEvent);
            }

            public void HandleEvent()
            {
                IsEventHandled = true;
            }
        }
    }
}