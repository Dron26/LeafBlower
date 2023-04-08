using _1Game.Scripts.Infrastructure.Logic;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using Infrastructure.Constants;
using UnityEngine;

namespace Infrastructure.States
{
    public class SceneSwitcher:MonoCache
    {
        private GameStateMachine _stateMachine;
        [SerializeField] private GameObject _battleLevel;
        public void Initialize(LoadLevelState loadLevelState, LoadingCurtain curtain, GameStateMachine stateMachine)
        {
            // GameObject battleLevel = Instantiate(_battleLevel.gameObject);
            // battleLevel.GetComponentInChildren<BattleLevel>().SetSceneSwitcher(this);
            // BattleLevel level = battleLevel.GetComponentInChildren<BattleLevel>();
            // level.SetSceneSwitcher(this);
        }
        
        
        public void EnterMenuLevel() => _stateMachine.Enter<LoadLevelState,string>(SceneName.Menu);

    }
}