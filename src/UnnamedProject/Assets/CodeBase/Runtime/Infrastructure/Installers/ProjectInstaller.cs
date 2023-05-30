using Infrastructure.Loading;
using Services.Essential;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            CreateAndBindLoadingScreen();
            BindLoadingService();
        }

        private void BindLoadingService() =>
            Container.Bind<ILoadingService>()
                .To<LoadingService>()
                .AsSingle();

        private void CreateAndBindLoadingScreen() =>
            Container.Bind<LoadingScreen>()
                .FromComponentInNewPrefabResource("Prefabs/UserInterface/LoadingScreen")
                .AsSingle();

        private void BindCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
    }
}