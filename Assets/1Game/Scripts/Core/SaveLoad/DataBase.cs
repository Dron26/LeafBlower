using System;
using System.Collections.Generic;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using UnityEngine;

namespace _1Game.Scripts.Core.SaveLoad
{
    [Serializable]
    public class DataBase
    {
        private int _money = 1000;
        private int _points;
        private bool _isTutorialCompleted;
        private  List<Dictionary<int, int>> _stagesStarGroup = new();
        private List<Upgrade> _upgrades = new();
        public long Money=>_money;
        
        public bool ReadTutorialCompleted =>
            _isTutorialCompleted;

        public int CountSpins { get; private set; }
        

        public int ReadAmountMoney =>
            _money;
        
        public void AddMoney(int amountMoney) => 
            _money += amountMoney;

        public void SpendMoney(int amountSpendMoney) => 
            _money -= Mathf.Clamp(amountSpendMoney, 0, int.MaxValue);

        public void AddPoints(int totalPoints) => 
            _points += totalPoints;
        
        public void ChangeCountSpins(int counterSpins) =>
            CountSpins = counterSpins;
        
        public int ReadCountSpins() =>
            CountSpins;

        public void ChangeTutorialCompleted(bool completed) => 
            _isTutorialCompleted = completed;
        
        public void ChangeStagesStarGroup( List<Dictionary<int, int>> stagesStarGroup)
        {
            _stagesStarGroup.Clear();
            
            foreach (Dictionary<int, int> stageInfo in stagesStarGroup)
            {
                _stagesStarGroup.Add(stageInfo);
            }
        }

        public void ChangeUpgradesGroup(List<Upgrade> upgrades)
        {
            _upgrades.Clear();
            
            foreach (Upgrade upgrade in upgrades)
            {
                _upgrades.Add(upgrade);
            }
        }
    }
}