using Devolvist.UnityReusableSolutions.StatesManagement;
using NUnit.Framework;

namespace Devolvist.UnityReusableSolutions.EditorTests
{
    [Category("States Management")]
    public class StateMachineTests
    {
        [Test]
        [Description("�������� ��������� �������� null ��� �������� ������� ��� ��������� ������������.")]
        public void ActiveStateIsNullWhenCreatedWithoutArgument()
        {
            StateMachine stateMachine = new StateMachine();

            Assert.IsNull(stateMachine.ActiveState);
        }

        [Test]
        [Description("�������� ��������� �������� ���������� ���������� ��� �������� ������� c ���������� ������������.")]
        public void ActiveStateIsArgumentedStateWhenCreatedWithArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("�������� ��������� �������� ������� ��� ������� �� ��������� ��������� � ���������� ���� �� ���������.")]
        public void ActiveStateIsNotChangedWhenChangingActiveStateWithIdenticalStateInArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("�������� ��������� �������� �� ����� ��� ������� �� ��������� ��������� � ���������� ������ ���������.")]
        public void ActiveStateChangedWhenChangingActiveStateWithNewStateInArgument()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.AreEqual(mockState1, stateMachine.ActiveState);
        }

        [Test]
        [Description("�������� ��������� �������� ������� ��� ������� �� ��������� ��������� � ��������� null � �������� ���������.")]
        public void ActiveStateIsNotChangedWhenChangingActiveStateWithNullArgument()
        {
            IState mockState = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(null);

            Assert.AreEqual(mockState, stateMachine.ActiveState);
        }

        [Test]
        [Description("����� Exit() � ��������� ��������� ��� ��������� ��������� �� �����. ")]
        public void CalledExitAtActiveStateWhenChangingActiveState()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState as MockState).IsExited);
        }

        [Test]
        [Description("����� Enter() � ������ ��������� ��� ��������� ��������� �� �����. ")]
        public void CalledEnterAtNewStateWhenChangingActiveState()
        {
            IState mockState = new MockState();
            IState mockState1 = new MockState();
            StateMachine stateMachine = new StateMachine(mockState);

            stateMachine.ChangeState(mockState1);

            Assert.IsTrue((mockState1 as MockState).IsEntered);
        }

        [Test]
        [Description("����� Exit() � ����������� ��������� � ����� Enter() � ������ ��� ��������� ��������� �� �����. ")]
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
        /// �������� ���������.
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