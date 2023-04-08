using System;
using _1Game.Scripts.ADs;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class RewardedAdsButton : MonoBehaviour
    {
        [SerializeField] private AdsSetter _adsSetter;
        [SerializeField] private UpgradePlace _upgradePlace;
        
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            gameObject.SetActive(false);
        }

        // private void OnEnable()
        // {
        //     _upgradePlace.EnterPlace += SetState;
        // }
        //
        // private void OnDisable()
        // {
        //     _upgradePlace.EnterPlace -= SetState;
        // }

        public void OnClick()
        {
            _adsSetter.ShowRewardedAd();
            SetState(false);
        }

        private void SetState(bool isWork)
        {
            gameObject.SetActive(false);
        }
    }
    
}