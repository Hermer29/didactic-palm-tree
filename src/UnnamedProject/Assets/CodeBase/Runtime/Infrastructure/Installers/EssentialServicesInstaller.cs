using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Services.Essential;
using Services.Essential.Prefs;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class EssentialServicesInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private Bootstrapper _bootstrapper;
        
        public override void InstallBindings()
        {
            BindBootstrapper();
            BindPrefsService();
            BindGameFactory();
            BindGameAssetLoader();
        }

        private void BindBootstrapper() =>
            Container.BindInterfacesAndSelfTo<Bootstrapper>()
                .FromInstance(_bootstrapper)
                .AsSingle();

        private void BindGameFactory() =>
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindGameAssetLoader() =>
            Container.Bind<IGameAssetLoader>()
                .To<GameAssetLoader>()
                .AsSingle();
        
        private void BindPrefsService() =>
            Container.Bind<IPrefsService>()
                .To<PlayerPrefsService>()
                .AsSingle();
    }
}