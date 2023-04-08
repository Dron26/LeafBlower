
using Infrastructure.BaseMonoCache.Code.MonoCache;
using UnityEngine;

namespace Service.SaveLoadService
{
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

        public void ApplyTotalPoints(int totalPoints)
        {
            _dataBase.AddPoints(totalPoints);
            Save();
        }

        public int GetCountSpins() => 
            _dataBase.ReadCountSpins();
        
        public void SaveCountSpins(int counterSpins) => 
            _dataBase.ChangeCountSpins(counterSpins);
    }
}