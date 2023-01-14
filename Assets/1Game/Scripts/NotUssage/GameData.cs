using System;
using _1Game.Scripts.Core;
using _1Game.Scripts.Item;
using UnityEngine;

namespace Core
{
    public class GameData : MonoBehaviour
    {
        public bool IsFirstGame { get; private set; }
        public long Money { get; private set; }
        public long MaxMoney { get; private set; }
        public int LeaveBlowerStandartLevel { get; private set; }
        public int LeaveBlowerlHardLevel { get; private set; }
        public int CharacterLevel { get; private set; }
        public int CartLevel { get; private set; }
        public int FuelChager { get; private set; }
        public float LevelStandartBlover { get; private set; }
        public float LevelAdvancedBlover { get; private set; }


        public void UpdateGameInitializator(GameInitializator gameInitializator)
        {
            IsFirstGame = gameInitializator.IsFirstStart;
        }

        public void UpdateDate(Wallet wallet, LeaveBlowerStandart standartBlower, LeaveBlowerAdvanced advancedBlower, CartTrashBagPicker cartTrashBagPicker, CharacterTrashBagPicker characterTrashBagPicker, FuelChanger fuelChanger)
        {
            Money = wallet.Money;
            MaxMoney = wallet.MaxMoney;

            LevelStandartBlover = standartBlower.Level;
            LevelAdvancedBlover = advancedBlower.Level;

            CartLevel= cartTrashBagPicker.MaxPickedBag;
            FuelChager = fuelChanger.MaxFueLevel;
        }
    }
}
