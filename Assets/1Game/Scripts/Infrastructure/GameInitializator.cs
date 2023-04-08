using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using Lean.Localization;
using Service.SaveLoadService;
using UnityEngine;
using Leaderboard = _1Game.Scripts.Leaderbord.Leaderboard;

namespace Infrastructure
{
    public class GameInitializator : MonoCache
    {
        [SerializeField] private Leaderboard _leaderboard;
        private const string Key = "Key";
    
        //[SerializeField] private Button _authorization;
        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            // _loadScreen.gameObject.SetActive(false);
            yield break;
#endif

            while (YandexGamesSdk.IsInitialized == false)
                yield return YandexGamesSdk.Initialize();

            LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);

            // if (PlayerAccount.IsAuthorized)
            //     _authorization.gameObject.SetActive(false);

            LoadCloudData();
            LoadSave(PlayerPrefs.GetString(nameof(Key)));
        }

        protected override void UpdateCustom()
        {
            AudioListener.pause = WebApplication.InBackground;
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
            DataBase save = new();
            if (string.IsNullOrEmpty(jsonSave))
            {
                // DisableLoadScreen();

            }
            else
            {
                save = JsonUtility.FromJson<DataBase>(jsonSave);

            }

            PlayerPrefs.SetString(Key, JsonUtility.ToJson(save));
            PlayerPrefs.Save();


            //DisableLoadScreen();
        }

        // private void DisableLoadScreen()
        // {
        //     _loadScreen.gameObject.SetActive(false);
        // }
    }
}
