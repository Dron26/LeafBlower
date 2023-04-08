using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Service.SaveLoadService
{
    [Serializable]
    public class DataBase
    {
        private int _money = 1000;
        private int _points;
        

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
    }
}