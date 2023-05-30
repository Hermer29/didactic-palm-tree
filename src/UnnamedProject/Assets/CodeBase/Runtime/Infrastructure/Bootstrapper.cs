using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using Services.Essential;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, IInitializable, ICoroutineRunner
    {
        private DiContainer _container;
        public static bool Bootstrapped { get; private set; }
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
            DontDestroyOnLoad(this);
        }

        public void Initialize()
        {
            Bootstrapped = true;
            var stateMachine = new StateMachine(_container);
            stateMachine.Enter<BootstrapState>();
        }
    }
}