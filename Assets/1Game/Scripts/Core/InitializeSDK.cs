using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leaderboard = _1Game.Scripts.Leaderbord.Leaderboard;

namespace _1Game.Scripts.Core
{
    public class InitializeSDK : MonoBehaviour
    {
        public event Action Initialized;
        [SerializeField] private Leaderboard _leaderboard;
        private void Awake()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield return new WaitForSeconds(0.1f);

#elif YANDEX_GAMES
        while(Agava.YandexGames.YandexGamesSdk.IsInitialized == false)
        {
            yield return Agava.YandexGames.YandexGamesSdk.Initialize();
        }
            
                    LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

            if (PlayerAccount.IsAuthorized)
        {
                _authorization.gameObject.SetActive(false);
        }
                  
            LoadCloudData();
#endif
            SceneManager.LoadScene(1);
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
    }

    
}
//
// using System.Collections;
// using Agava.WebUtility;
// using Agava.YandexGames;
// using Lean.Localization;
// using UnityEngine;
//
// namespace _1Game.Scripts.Core
// {
//     public class InitializeSDK : MonoBehaviour
//     {
//         [SerializeField] private Leaderboard _leaderboard;
//         private const string Key = "Key";
//
//         //[SerializeField] private Button _authorization;
//         private IEnumerator Start()
//         {
// #if !UNITY_WEBGL || UNITY_EDITOR
//             // _loadScreen.gameObject.SetActive(false);
//             yield break;
// #endif
//
//             while (YandexGamesSdk.IsInitialized == false)
//                 yield return YandexGamesSdk.Initialize();
//
//             LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);
//
//             // if (PlayerAccount.IsAuthorized)
//             //     _authorization.gameObject.SetActive(false);
//
//             LoadCloudData();
//         }
//
//         // protected override void UpdateCustom()
//         // {
//         //     AudioListener.pause = WebApplication.InBackground;
//         // }
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
//     }
// }