using System.Collections;
using _1Game.Scripts.Core.SaveLoad;
using _1Game.Scripts.Core.SaveLoad.Data;
using Agava.YandexGames;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core.Data
{
    public class GameInitializator : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private bool _isTutorialCompleted;
        [SerializeField] private StageData _stageData;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        [SerializeField] private Leaderboard _leaderboard;
        public UnityAction FirstStart;

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();
            LoadCloudData();
            LoadSave(PlayerPrefs.GetString(nameof(SaveData)));
            Debug.Log("GameInitializator -IEnumerator Start()-LoadSave(PlayerPrefs.GetString(nameof(SaveData)));");
            Debug.Log("nameof(SaveData)");

        }

        public void LoadCloudData()
        {
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init,
                topPlayersCount: _leaderboard.AmountRecords, competingPlayersCount: _leaderboard.AmountRecords);
        }

        private void TryCreatePlayerLeaderboardEntity(LeaderboardEntryResponse leaderboardEntryResponse)
        {
            if (leaderboardEntryResponse == null)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, 0);
        }


        private void LoadSave(string jsonSave)
        {
            Debug.Log("string jsonSave");
            
            if (string.IsNullOrEmpty(jsonSave))
            {
                FirstStart?.Invoke();
                
                Debug.Log("string.IsNullOrEmpty(jsonSave");
                Debug.Log("FirstStart?.Invoke();");
                return;
            }

            SaveData save = JsonConvert.DeserializeObject<SaveData>(jsonSave);
            
            Debug.Log("SaveData save = JsonConvert.DeserializeObject<SaveData>(jsonSave);");
            if (save != null && save.IsTutorialCompleted == false)
            {
                Debug.Log("if (save != null && save.IsTutorialCompleted == false)");
                Debug.Log("FirstStart?.Invoke();");
                FirstStart?.Invoke();
            }
            else
            {
                Debug.Log("else");
                _wallet.ChangeResource(save.Money);
                _isTutorialCompleted = save.IsTutorialCompleted;
                _stageData.SetStagesStarGroup(save.StagesStarGroup);
                _upgradeParametrs.SetUpgrades(save.Upgrades);
                Debug.Log("_wallet.ChangeResource(save.Money);_isTutorialCompleted = save.IsTutorialCompleted;_stageData.SetStagesStarGroup(save.StagesStarGroup);_upgradeParametrs.SetUpgrades(save.Upgrades);");
            }
        }
    }
}