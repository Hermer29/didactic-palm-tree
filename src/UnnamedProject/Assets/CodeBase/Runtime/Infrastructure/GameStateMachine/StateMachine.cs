using System;
using System.Collections.Generic;
using Infrastructure.GameStateMachine.States;
using Infrastructure.GameStateMachine.States.Abstract;
using Infrastructure.Loading;
using Services.Essential;
using Zenject;

namespace Infrastructure.GameStateMachine
{
    public class StateMachine
    {
        private readonly DiContainer _container;
        private readonly Dictionary<Type, Func<IExitableState>> _states;
        private IExitableState _activeState;
        
        public StateMachine(DiContainer container)
        {
            _container = container;
            _states = new Dictionary<Type, Func<IExitableState>>
            {
                {typeof(BootstrapState), () => new BootstrapState(
                    coroutineRunner: _container.Resolve<ICoroutineRunner>(),
                    loadingService: _container.Resolve<ILoadingService>())}
            };
        }

        public void Enter<TState>() where TState: class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            IExitableState state = GetState<TState>();
            _activeState = state;
            return (TState) state;
        }

        private IExitableState GetState<TState>() where TState: class, IExitableState
        {
            return _states[typeof(TState)].Invoke() as TState;
        }
    }
}