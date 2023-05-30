using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class GameAssetLoader : IGameAssetLoader
    {
        public LoadingScreen LoadLoadingScreen()
        {
            return Resources.Load<LoadingScreen>(GameAssetConstants.LoadingScreenResourcesPath);
        }
    }
}