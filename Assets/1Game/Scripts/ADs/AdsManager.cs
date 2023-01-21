using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;

namespace ADs
{
    public class AdsManager : MonoBehaviour
    {
        public IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            if (YandexGamesSdk.IsInitialized)
            {
                yield break;
            }

            yield return YandexGamesSdk.Initialize();
        }


        
        public void ShowVideo(Action action)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            VideoAd.Show(onRewardedCallback: () => action?.Invoke());
        }

        public void ShowInterstitial()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            InterstitialAd.Show();
        }
        
    }
}
