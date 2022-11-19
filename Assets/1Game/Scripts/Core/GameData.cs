using System;
using UnityEngine;

namespace Core
{
    [Serializable]

    public class GameData : MonoBehaviour
    {
        public bool IsFirstGame { get; private set; }
        public int Money { get; private set; }
        public int MaxMoney { get; private set; }
        public int LeaveBlowerStandartLevel { get; private set; }
        public int LeaveBlowerlHardLevel { get; private set; }
        public int TrashBagLevel { get; private set; }
        public int CartLevel { get; private set; }
        

        public void FirstUpdateDate(Wallet wallet, GameInitializator gameInitializator )
        {
            Money = wallet.Money;
            MaxMoney = wallet.MaxMoney;
            IsFirstGame = gameInitializator.IsFirstStart;
        }
    }
}
