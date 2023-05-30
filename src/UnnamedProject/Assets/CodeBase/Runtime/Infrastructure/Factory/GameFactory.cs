using Infrastructure.AssetManagement;
using static UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly GameAssetLoader _loader;

        public GameFactory(GameAssetLoader loader)
        {
            _loader = loader;
        }
        
        public LoadingScreen CreateLoadingScreen()
        {
            return Instantiate(_loader.LoadLoadingScreen());
        }
    }
}