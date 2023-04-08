// using System;
// using System.Collections.Generic;
//
// namespace _1Game.Scripts.Core.SaveLoad
// {
//     [Serializable]
//     public class SaveLoad
//     {
//         public long Money;
//         public bool IsTutorialCompleted;
//         public List<Dictionary<int, int>> StagesStarGroup = new();
//         public List<Upgrade> Upgrades = new();
//
//         public SaveLoad(long money,bool isTutorialCompleted,List<Dictionary<int, int>> stagesStarGroup,List<Upgrade> upgrades)
//         {
//             Money = money;
//             IsTutorialCompleted = isTutorialCompleted;
//             StagesStarGroup = stagesStarGroup;
//             Upgrades = upgrades;
//         }
//     }
// }

using System;
using System.Collections.Generic;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using Service.SaveLoadService;
using UnityEngine;

namespace _1Game.Scripts.Core.SaveLoad
{
    [Serializable]
    
    public class SaveLoad : MonoCache
    {
        private const string Key = "Key";
        private DataBase _dataBase;
        

        private void Awake()
        {
            _dataBase = PlayerPrefs.HasKey(Key)
                ? JsonUtility.FromJson<DataBase>(PlayerPrefs.GetString(Key))
                : new DataBase();
        }

        protected override void OnDisabled() => 
            Save();
        

        public void ApplyMoney(int amountMoney)
        {
            _dataBase.AddMoney(amountMoney);
            Save();
        }

        public void SpendMoney(int amountSpendMoney)
        {
            _dataBase.SpendMoney(amountSpendMoney);
            Save();
        }
        

        public int ReadAmountMoney() =>
            _dataBase.ReadAmountMoney;

        public void Save()
        {
            PlayerPrefs.SetString(Key, JsonUtility.ToJson(_dataBase));
            PlayerPrefs.Save();
        }
        
        public int GetCountSpins() => 
            _dataBase.ReadCountSpins();
        
        public void SaveCountSpins(int counterSpins) => 
            _dataBase.ChangeCountSpins(counterSpins);

        public void AddMoney(int amountMoney) => 
            _dataBase.AddMoney( amountMoney);
        
        public void SaveStagesStarGroup(List<Dictionary<int, int>> group) => 
            _dataBase.ChangeStagesStarGroup(group);

        public void SaveTutorialCompleted(bool completed) =>
            _dataBase.ChangeTutorialCompleted(completed);
        
        public void SaveUpgrades(List<Upgrade> upgrades) =>
            _dataBase.ChangeUpgradesGroup(upgrades);
        
    }
}