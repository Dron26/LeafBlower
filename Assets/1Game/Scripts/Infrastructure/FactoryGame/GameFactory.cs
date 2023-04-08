using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.FactoryGame
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        
        public GameFactory(IAssets assets) => 
            _assets = assets;
        
        public GameObject CreateMenuScene() => 
            _assets.Instantiate(AssetPath.MenuScene);
        
        public GameObject CreateSceneSwitcher() => 
            _assets.Instantiate(AssetPath.SceneSwitcher);
    }
}