namespace Gameplay
{
    public class StateMachine
    {
        IState currentState;

        public void ChangeState(IState nextState)
        {
            if (nextState == null)
                return;

            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public void Tick(float deltaTime)
        {
            currentState?.Tick(deltaTime);
        }
    }
}
