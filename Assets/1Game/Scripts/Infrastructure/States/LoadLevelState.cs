

using System;
using System.Collections.Generic;
using _1Game.Scripts.Core.Data;
using _1Game.Scripts.Infrastructure.Logic;
using Infrastructure.FactoryGame;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly List<string> _sceneNames;
        private readonly List<Action> _actions = new();
        private string _nameScene;

        Dictionary<string, Action> _switherGroup = new();

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, List<string> sceneNames)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _sceneNames = sceneNames;

            FillActionGroup();
            FillSwitcherGroup();
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _nameScene = sceneName;
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            foreach (var (key, value) in _switherGroup)
                if (key == _nameScene)
                    value();
        }

        private void CreateMainScene()
        {
            GameObject main = _gameFactory.CreateMenuScene();
            main.GetComponentInChildren<MenuScene>().Initialize(this,_curtain,_stateMachine);
            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateSceneSwitcher()
        { 
            GameObject sceneSwitcher = _gameFactory.CreateSceneSwitcher();
            sceneSwitcher.GetComponentInChildren<SceneSwitcher>().Initialize(this,_curtain,_stateMachine);
            _stateMachine.Enter<GameLoopState>();
        }

        private void FillSwitcherGroup()
        {
            for (int i = 0; i < _sceneNames.Count; i++)
            {
                _switherGroup.Add(_sceneNames[i], _actions[i]);
            }
        }

        private void FillActionGroup()
        {
            _actions.Add(null);
            _actions.Add(CreateMainScene);
            _actions.Add(CreateSceneSwitcher);
        }
    }
}