using _1Game.Scripts.Core.SaveLoad;
using _1Game.Scripts.Core.SaveLoad.Data;
using _1Game.Scripts.Infrastructure.Logic;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using Infrastructure.States;
using UnityEngine;

namespace _1Game.Scripts.Core.Data
{
    public class MenuScene:MonoCache
    {

        private StageData _stageData;
        public void Initialize(LoadLevelState loadLevelState, LoadingCurtain curtain, GameStateMachine stateMachine)
        {
            _stageData = GetComponent<StageData>();
        }
        
        
    }
}