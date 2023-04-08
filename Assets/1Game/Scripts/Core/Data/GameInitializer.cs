// using System.Collections;
// using System.Collections.Generic;
// using _1Game.Scripts.Core.SaveLoad;
// using _1Game.Scripts.Core.SaveLoad.Data;
// using Agava.YandexGames;
// using UnityEngine;
// using UnityEngine.Events;
// using Leaderboard = _1Game.Scripts.Leaderbord.Leaderboard;
//
// namespace _1Game.Scripts.Core.Data
// {
//     public class GameInitializer : MonoBehaviour
//     {
//         [SerializeField] private Wallet _wallet;
//         [SerializeField] private bool _isTutorialCompleted;
//         [SerializeField] private StageData _stageData;
//         [SerializeField] private UpgradeParametrs _upgradeParametrs;
//         [SerializeField] private Leaderboard _leaderboard;
//         [SerializeField] private Store _store;
//         public UnityAction FirstStart;
//         private List<Dictionary<int, int>> _stagesStarGroup;
//         
//         private IEnumerator Start()
//         {
// #if UNITY_WEBGL && !UNITY_EDITOR
//         YandexGamesSdk.Initialize();
//         LoadCloudData();
// #endif
//             yield return null;
//             LoadSave(PlayerPrefs.GetString(nameof(SaveLoad)));
//             Debug.Log("Load");
//         }
//
//         public void LoadCloudData()
//         {
//             Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
//             Agava.YandexGames.Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init,
//                 topPlayersCount: _leaderboard.AmountRecords, competingPlayersCount: _leaderboard.AmountRecords);
//         }
//
//         private void TryCreatePlayerLeaderboardEntity(LeaderboardEntryResponse leaderboardEntryResponse)
//         {
//             if (leaderboardEntryResponse == null)
//                 Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, 0);
//         }

        // private void LoadSave(string jsonSave)
        // {
        //     if (string.IsNullOrEmpty(jsonSave))
        //     {
        //         FirstStart?.Invoke();
        //         return;
        //     }
        //
        //     SaveLoad save = JsonUtility.FromJson<SaveLoad>(jsonSave);
        //     
        //     if (save != null && save.IsTutorialCompleted == false)
        //     {
        //       FirstStart?.Invoke();
        //         Debug.Log("Tutor");
        //     }
        //
        //         _wallet.ChangeResource(save.Money);
        //         _isTutorialCompleted = save.IsTutorialCompleted;
        //         _stageData.SaveStagesStarGroup(save.StagesStarGroup);
        //         _upgradeParametrs.SaveUpgrades(save.Upgrades);
        //         Debug.Log("Loaded ");
        //     
        // }
        
//         private void LoadSave(string jsonSave)
//         {
//             if (string.IsNullOrEmpty(jsonSave))
//             {
//                 FirstStart?.Invoke();
//                 Generate();
//                 var initialSave = new SaveLoad(0, false, _stagesStarGroup, new List<Upgrade>());
//                 jsonSave = JsonUtility.ToJson(initialSave);
//                 PlayerPrefs.SetString(nameof(SaveLoad), jsonSave);
//                 Debug.Log("New save created.");
//                 return;
//             }
//
//             SaveLoad save = JsonUtility.FromJson<SaveLoad>(jsonSave);
//
//             if (save != null && save.IsTutorialCompleted == false)
//             {
//                 FirstStart?.Invoke();
//                 Debug.Log("Tutor");
//             }
//
//             _wallet.ChangeResource(save.Money);
//             _isTutorialCompleted = save.IsTutorialCompleted;
//             _stageData.SaveStagesStarGroup(save.StagesStarGroup);
//             _upgradeParametrs.SaveUpgrades(save.Upgrades);
//             Debug.Log("Loaded ");
//         }
//
//         private void Generate()
//         {
//             _stagesStarGroup = new List<Dictionary<int, int>>();
//             
//             int numGroups = _stageData.GetComponent<StageController>().CountGroup;
//
//             for (int i = 0; i < numGroups; i++)
//             {
//                 int numStages = _stageData.GetComponent<StageController>().GetCountStages(i);
//                 Dictionary<int, int> stageStars = new Dictionary<int, int>();
//
//                 for (int j = 0; j < numStages; j++)
//                 {
//                     stageStars.Add(j, 0);
//                 }
//
//                 _stagesStarGroup.Add(stageStars);
//             }
//             
//             _upgradeParametrs.Initialize();
//             _store.InitializeUpgradePanels();
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using _1Game.Scripts.Core.SaveLoad;
using _1Game.Scripts.Core.SaveLoad.Data;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Leaderboard = _1Game.Scripts.Leaderbord.Leaderboard;
using System;
using System.Collections;
using Agava.YandexGames;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace _1Game.Scripts.Core.Data
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private Leaderboard _leaderboard;
         private const string Key = "Key";

         [SerializeField] private GameBootstrapper _gameBootstrapper;
        // public UnityAction FirstStart;
        //  private List<Dictionary<int, int>> _stagesStarGroup;
        //  [SerializeField] private StageData _stageData;
        //  [SerializeField] private UpgradeParametrs _upgradeParametrs;
        //  [SerializeField] private Store _store;
        // //[SerializeField] private Button _authorization;
        private void Awake()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield return new WaitForSeconds(0.1f);
        

#elif YANDEX_GAMES

        AudioListener.pause = WebApplication.InBackground;
            while (YandexGamesSdk.IsInitialized == false)
                yield return YandexGamesSdk.Initialize();

            LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

            // if (PlayerAccount.IsAuthorized)
            //     _authorization.gameObject.SetActive(false);

            LoadCloudData();
            LoadSave(PlayerPrefs.GetString(nameof(Key)));

#endif
            LoadSave(PlayerPrefs.GetString(nameof(Key)));
            SceneManager.LoadScene("Menu");
        }

        public void LoadCloudData()
        {
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardConstants.Name, TryCreatePlayerLeaderboardEntity);
            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardConstants.Name, _leaderboard.Init, topPlayersCount: _leaderboard.AmountRecords, competingPlayersCount: _leaderboard.AmountRecords);
        }

        private void TryCreatePlayerLeaderboardEntity(LeaderboardEntryResponse leaderboardEntryResponse)
        {
            if (leaderboardEntryResponse == null)
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, 0);
        }

        private void LoadSave(string jsonSave)
        {
            SaveLoad.SaveLoad save = new();
            if (!string.IsNullOrEmpty(jsonSave))
            {
                save = JsonUtility.FromJson<SaveLoad.SaveLoad>(jsonSave);
            }

            PlayerPrefs.SetString(Key, JsonUtility.ToJson(save));
            PlayerPrefs.Save();
        }
        //
        // private void Generate()
        // {
        //      _stagesStarGroup = new List<Dictionary<int, int>>();
        //      
        //      int numGroups = _stageData.GetComponent<StageController>().CountGroup;
        //
        //      for (int i = 0; i < numGroups; i++)
        //      {
        //          int numStages = _stageData.GetComponent<StageController>().GetCountStages(i);
        //          Dictionary<int, int> stageStars = new Dictionary<int, int>();
        //
        //          for (int j = 0; j < numStages; j++)
        //          {
        //              stageStars.Add(j, 0);
        //          }
        //
        //          _stagesStarGroup.Add(stageStars);
        //      }
        //      
        //      _upgradeParametrs.Initialize();
        //      _store.InitializeUpgradePanels();
        //  }

        private void SetGameBootstrapper()
        {
            GameObject bootstrapper = Instantiate(_gameBootstrapper.gameObject);
        }
    }
}
