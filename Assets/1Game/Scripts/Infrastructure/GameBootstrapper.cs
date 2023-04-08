using _1Game.Scripts.Infrastructure.Logic;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoCache,ICorountineRunner
    {
        [SerializeField] private  LoadingCurtain Curtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this,Curtain);
            _game.StateMashine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}