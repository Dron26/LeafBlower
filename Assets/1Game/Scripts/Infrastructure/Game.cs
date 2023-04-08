using _1Game.Scripts.Infrastructure.Logic;
using Infrastructure.States;
using Lean.Localization;
using Service;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMashine;
        
        public Game(ICorountineRunner corountineRunner, LoadingCurtain curtain)
        {
            StateMashine = new GameStateMachine(new SceneLoader(corountineRunner),curtain,AllServices.Container );
        }
    }
}