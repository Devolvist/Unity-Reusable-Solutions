using Devolvist.UnityReusableSolutions.Events;
using NUnit.Framework;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("Events")]
    public class EventsTest
    {
        // Синтаксис методов:

        // Ожидаемый_Результат_При_Заданных_Условиях()
        // {
        //    1. Инициализация тестируемых объектов.
        //    2. Выполнение тестируемой логики при заданных условиях (если требуется).
        //    3. Утверждение ожидаемого результата.
        // }
        //
        // Подчеркивания добавлены для удобочитаемости в окне Test Runner.
        
        [Test]
        public void Is_0_Subscribers_When_Instance_Created()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        public void Is_1_Subscriber_When_Subscribe_Single()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        public void Is_1_Subscriber_When_Subscribe_Twice()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        public void Is_0_Subscribers_When_Subscribe_Then_Unsubscribe()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        public void Is_2_Subscribers_When_2_Different_Actions_Subscribes()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction_1);

            Assert.IsTrue(testableEvent.SubscribersCount == 2);
        }

        [Test]
        public void Is_1_Subscriber_When_2_Different_Actions_Subscribes_Then_1_Unsubscribe()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Subscribe(MockAction_1);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 1);
        }

        [Test]
        public void Is_2_Subscribers_When_2_Different_Objects_Subscribes()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();
            MockSubscriber mockClass = new MockSubscriber();
            MockSubscriber mockClass1 = new MockSubscriber();
            
            testableEvent.Subscribe(mockClass.MockClassAction);
            testableEvent.Subscribe(mockClass1.MockClassAction);
            
            Assert.IsTrue(testableEvent.SubscribersCount == 2);
        }

        [Test]
        public void Is_1_Subscriber_When_2_Different_Objects_Subscribes_Then_1_Unsubscribe()
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
        public void Is_0_Subscribers_When_1_Subscribe_Then_Unsubscribe_Twice()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);
            testableEvent.Unsubscribe(MockAction);

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        public void Is_2_Subscribers_When_2_Different_Objects_Subscribes_Twice()
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
        public void Is_0_Subscribers_When_Data_Cleared_With_Active_Subscriber()
        {
            ScriptableEvent testableEvent = ScriptableObject.CreateInstance<ScriptableEvent>();

            testableEvent.Subscribe(MockAction);
            testableEvent.ClearSubscribersData();

            Assert.IsTrue(testableEvent.SubscribersCount == 0);
        }

        [Test]
        public void Is_All_Subscribers_Invoked_When_1_Subscriber_Unsubscribed_At_Process()
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