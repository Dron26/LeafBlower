using System;
using UnityEngine;
using Service;



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
        public int CharacterLevel { get; private set; }
        public int CartLevel { get; private set; }

        public float LevelStandartBlover { get; private set; }
        public float LevelAdvancedBlover { get; private set; }

        public void UpdateGameInitializator(GameInitializator gameInitializator)
        {
            IsFirstGame = gameInitializator.IsFirstStart;
        }

        public void FirstUpdateDate(Wallet wallet, LeaveBlowerStandart standartBlower, LeaveBlowerAdvanced advancedBlower, CartTrashBagPicker cartTrashBagPicker, CharacterTrashBagPicker characterTrashBagPicker)
        {
            Money = wallet.Money;
            MaxMoney = wallet.MaxMoney;

            LevelStandartBlover = standartBlower.Level;
            LevelAdvancedBlover = advancedBlower.Level;

            CartLevel= cartTrashBagPicker.Level;
            CharacterLevel = characterTrashBagPicker.Level;

        }
    }
}
