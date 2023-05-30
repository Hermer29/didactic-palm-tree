namespace Infrastructure.GameStateMachine.States.Abstract
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload material);
    }
}