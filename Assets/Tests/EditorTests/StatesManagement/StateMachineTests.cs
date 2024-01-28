using Devolvist.UnityReusableSolutions.StatesManagement;
using NUnit.Framework;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("States Management")]
    public class StateMachineTests
    {
        [Test]
        [Description("јктивное состо€ние €вл€етс€ null при создании объекта без аргумента конструктора.")]
        public void ActiveStateIsNullWhenCreatedWithoutArgument()
        {
            StateMachine stateMachine = new StateMachine();

            Assert.IsNull(stateMachine.ActiveState);
        }

        [Test]
        [Description("јктивное состо€ние €вл€етс€ переданным аргументом при создании объекта c аргументом конструктора.")]
        public void ActiveStateIsArgumentedStateWhenCreatedWithArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("јктивное состо€ние остаетс€ прежним при запросе на изменение состо€ни€ с аргументом того же состо€ни€.")]
        public void ActiveStateIsNotChangedWhenChangingActiveStateWithIdenticalStateInArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("јктивное состо€ние мен€етс€ на новое при запросе на изменение состо€ни€ с аргументом нового состо€ни€.")]
        public void ActiveStateChangedWhenChangingActiveStateWithNewStateInArgument()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.AreEqual(mockState1, stateMachine.ActiveState);
        }

        [Test]
        [Description("јктивное состо€ние остаетс€ прежним при запросе на изменение состо€ни€ с передачей null в качестве аргумента.")]
        public void ActiveStateIsNotChangedWhenChangingActiveStateWithNullArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(null);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("¬ызов Exit() у активного состо€ни€ при изменении состо€ни€ на новое. ")]
        public void CalledExitAtActiveStateWhenChangingActiveState()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState as MockState).IsExited);
        }

        [Test]
        [Description("¬ызов Enter() у нового состо€ни€ при изменении состо€ни€ на новое. ")]
        public void CalledEnterAtNewStateWhenChangingActiveState()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState1 as MockState).IsEntered);
        }

        [Test]
        [Description("¬ызов Exit() у предыдущего состо€ни€ и вызов Enter() у нового при изменении состо€ни€ на новое. ")]
        public void CalledExitAtLastStateThenCalledEnterAtNewStateWhenChangingActiveState()
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