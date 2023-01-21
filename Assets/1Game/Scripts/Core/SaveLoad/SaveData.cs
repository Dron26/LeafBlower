using System;
using System.Collections.Generic;

namespace _1Game.Scripts.Core.SaveLoad
{
    [Serializable]
    public class SaveData
    {
        public long Money;
        public bool IsTutorialCompleted;
        public List<Dictionary<int, int>> StagesStarGroup = new();
        public List<Upgrade> Upgrades = new();

        public SaveData(long money,bool isTutorialCompleted,List<Dictionary<int, int>> stagesStarGroup,List<Upgrade> upgrades)
        {
            Money = money;
            IsTutorialCompleted = isTutorialCompleted;
            StagesStarGroup = stagesStarGroup;
            Upgrades = upgrades;
        }
    }
}