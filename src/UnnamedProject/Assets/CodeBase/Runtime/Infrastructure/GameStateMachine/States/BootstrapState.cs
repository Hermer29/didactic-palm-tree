using Infrastructure.GameStateMachine.States.Abstract;
using Infrastructure.Loading;
using Services.Essential;
using UnityEngine;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILoadingService _loadingService;

        public BootstrapState(ICoroutineRunner coroutineRunner, ILoadingService loadingService)
        {
            _coroutineRunner = coroutineRunner;
            _loadingService = loadingService;
        }
        
        public void Enter()
        {
            Debug.Log("Bootstrapping!");
        }

        public void Exit()
        {
            
        }
    }
}