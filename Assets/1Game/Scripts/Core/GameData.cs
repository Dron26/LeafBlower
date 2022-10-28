using System;
using UnityEngine;

namespace Core
{
    [Serializable]

    public class GameData : MonoBehaviour
    {
        public bool IsEnterGame { get; private set; }
        public int Money { get; private set; }
        public int MaxMoney { get; private set; }

        public void FirstUpdateDate(Wallet wallet)
        {
            Money = wallet.Money;
            MaxMoney = wallet.MaxMoney;
        }
    }
}
