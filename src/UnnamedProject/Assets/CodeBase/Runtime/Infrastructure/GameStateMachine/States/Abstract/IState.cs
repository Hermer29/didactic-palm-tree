namespace Infrastructure.GameStateMachine.States.Abstract
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}