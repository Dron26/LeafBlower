using Service;
using UnityEngine;

namespace Infrastructure.FactoryGame
{
    public interface IGameFactory:IService
    {
        GameObject CreateMenuScene();
        GameObject CreateSceneSwitcher();
    }
}