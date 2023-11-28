namespace Devolvist.UnityReusableSolutions.StatesManagement
{
    public class StateMachine
    {
        public IState ActiveState { get; private set; }

        public StateMachine(IState defaultState = null)
        {
            if (defaultState != null)
            {
                ChangeState(defaultState);
            }
        }

        public void ChangeState(IState targetState)
        {
            if (ActiveState != null & ActiveState == targetState)
            {
                return;
            }

            if (targetState == null)
            {
                return;
            }

            if (ActiveState != null)
            {
                ActiveState.Exit();
            }

            ActiveState = targetState;
            ActiveState.Enter();
        }
    }
}