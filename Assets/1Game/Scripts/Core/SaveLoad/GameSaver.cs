using _1Game.Scripts.Core.SaveLoad.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace _1Game.Scripts.Core.SaveLoad
{
    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField]private bool _isTutorialCompleted;
        [SerializeField] private StageData _stageData;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
    
        public void SaveGame()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            SaveData save = new SaveData(_wallet.Money,_isTutorialCompleted,_stageData.GetStagesStarGroup(),_upgradeParametrs.GetUpgrades());

            PlayerPrefs.SetString(nameof(SaveData), JsonConvert.SerializeObject(save));
            PlayerPrefs.Save();
        }
    }
}