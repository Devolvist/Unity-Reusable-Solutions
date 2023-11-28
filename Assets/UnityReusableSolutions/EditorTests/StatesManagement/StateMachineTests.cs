using Devolvist.UnityReusableSolutions.StatesManagement;
using NUnit.Framework;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("States Management")]
    public class StateMachineTests
    {
        /// <summary>
        /// јктивное состо€ние €вл€етс€ null при создании объекта без аргумента конструктора.
        /// </summary>
        [Test]
        public void Active_State_IsNull_When_Created_Without_Argument()
        {
            StateMachine stateMachine = new StateMachine();

            Assert.IsNull(stateMachine.ActiveState);
        }

        /// <summary>
        /// јктивное состо€ние €вл€етс€ переданным аргументом при создании объекта c аргументом конструктора.
        /// </summary>
        [Test]
        public void Active_State_Is_Argumented_State_When_Created_With_Argument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        /// <summary>
        /// јктивное состо€ние остаетс€ прежним при запросе на изменение состо€ни€ с аргументом того же состо€ни€.
        /// </summary>
        [Test]
        public void Active_State_Is_Not_Changed_When_Changing_Active_State_With_Identical_State_In_Argument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        /// <summary>
        /// јктивное состо€ние мен€етс€ на новое при запросе на изменение состо€ни€ с аргументом нового состо€ни€.
        /// </summary>
        [Test]
        public void Active_State_Changed_When_Changing_Active_State_With_New_State_In_Argument()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.AreEqual(mockState1, stateMachine.ActiveState);
        }

        /// <summary>
        /// јктивное состо€ние остаетс€ прежним при запросе на изменение состо€ни€ с передачей null в качестве аргумента.
        /// </summary>
        [Test]
        public void Active_State_Is_Not_Changed_When_Changing_Active_State_With_Null_Argument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(null);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        /// <summary>
        /// ¬ызов Exit() у активного состо€ни€ при изменении состо€ни€ на новое. 
        /// </summary>
        [Test]
        public void Called_Exit_At_Active_State_When_Changing_Active_State()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState as MockState).IsExited);
        }

        /// <summary>
        /// ¬ызов Enter() у нового состо€ни€ при изменении состо€ни€ на новое. 
        /// </summary>
        [Test]
        public void Called_Enter_At_New_State_When_Changing_Active_State()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState1 as MockState).IsEntered);
        }

        /// <summary>
        /// ¬ызов Exit() у предыдущего состо€ни€ и вызов Enter() у нового при изменении состо€ни€ на новое. 
        /// </summary>
        [Test]
        public void Called_Exit_At_Last_State_Then_Called_Enter_At_New_State_When_Changing_Active_State()
        {
            MockState mockState = new MockState();
            MockState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);
            bool successfulResult = mockState.IsExited && mockState1.IsEntered;

            Assert.IsTrue(successfulResult);
        }

        #region Mocks
        /// <summary>
        /// »митаци€ состо€ни€.
        /// </summary>
        private class MockState : IState
        {
            public bool IsEntered { get; private set; }
            public bool IsExited { get; private set; }

            public void Enter()
            {
                IsEntered = true;
                IsExited = false;
            }

            public void Exit()
            {
                IsExited = true;
                IsEntered = false;
            }
        }
        #endregion
    }
}