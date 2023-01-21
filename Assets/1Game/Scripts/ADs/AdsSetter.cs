using System;
using System.Collections;
using System.Collections.Generic;
using _1Game.Scripts.Core;
using _1Game.Scripts.UI;
using ADs;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.ADs
{
    public class AdsSetter : MonoBehaviour
    {

        [SerializeField] private AdsManager _adsManager;
        [SerializeField] private ExitPanel _exitPanel;
        [SerializeField] private Wallet _wallet;

        private UnityEvent _onRewardViwed = new UnityEvent();
        private UnityEvent _onOpenViwed = new UnityEvent();
        private UnityEvent _onClosedViwed = new UnityEvent();
        
        private bool _soundStatus = false;

        public event UnityAction OnRewardViwed
        {
            add=>_onRewardViwed.AddListener(value);
            remove=>_onRewardViwed.RemoveListener(value);
    } 
        public event UnityAction OnOpenViwed
        {
            add=>_onOpenViwed.AddListener(value);
            remove=>_onOpenViwed.RemoveListener(value);
        }
        public event UnityAction OnClosedViwed
        {
            add=>_onClosedViwed.AddListener(value);
            remove=>_onClosedViwed.RemoveListener(value);
        }

        private void OnEnable()
        {
            _exitPanel.SetNextLevel+= OnEndLevel;
            OnOpenViwed += OnOpen;
            OnRewardViwed += OnRewarded;
            OnClosedViwed += OnClosed;
        }
        
          
        public void  Start()
        {
            ShowFullScreenAd(); 
        }
        

        private void OnEndLevel()
        {
            ShowFullScreenAd();
        }

        
        
        public void ShowRewardedAd()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            print("Show Ads");
            VideoAd.Show(onOpenCallback:_onOpenViwed.Invoke,onRewardedCallback:_onRewardViwed.Invoke,onCloseCallback:_onClosedViwed.Invoke);
        }
        

        public void ShowFullScreenAd()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            if (YandexGamesSdk.IsInitialized == false)
            {
                return;
            }

            print("Show Ads");
            InterstitialAd.Show(onOpenCallback:OnOpen, onCloseCallback: OnFullScreenShowed);
        }
        
        public void OnOpen()
        {
            _soundStatus = AudioListener.pause;
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        public void OnRewarded()
        {
            _wallet.AddResource(50);
            Time.timeScale = 1;
        }

        public void OnClosed()
        {
            AudioListener.pause = _soundStatus;
            Time.timeScale = 1;
        }

        public void OnFullScreenShowed(bool parameter)
        {
            AudioListener.pause = _soundStatus;
            Time.timeScale = _soundStatus == true ? 0 : 1;
        }

        public void OnDisable()
        {
            _exitPanel.SetNextLevel-= OnEndLevel;
            OnOpenViwed -= OnOpen;
            OnRewardViwed -= OnRewarded;
            OnClosedViwed -= OnClosed;
        }
        
    }
}
