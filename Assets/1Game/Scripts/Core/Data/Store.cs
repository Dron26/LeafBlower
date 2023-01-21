using System;
using System.Collections.Generic;
using _1Game.Scripts.ADs;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class Store : MonoBehaviour
    {
            
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        [SerializeField] private RewardedAdsButton _rewardedAdsButton;
        [SerializeField] private AdsSetter _adsSetter;
        
        private CartPanel _cartPanel;
        private CharacterPanel _characterPanel;
        private FuelPanel _fuelPanel;
        private Panel _upgradePanelContainer;
        public UnityAction Update;
        public UnityAction EmptyWallet;
        public UnityAction<int> RachedMaxLevel;
        private List<UpgradePanel> _upgradePanels = new List<UpgradePanel>();
        private int _numberUpgrade;

        private void Awake()
        {
            _upgradePanelContainer = GetComponentInChildren<Panel>();
            InitializeUpgradePanels();
            _fuelPanel = _upgradePanelContainer.GetComponentInChildren<FuelPanel>();
            _characterPanel = _upgradePanelContainer.GetComponentInChildren<CharacterPanel>();
            _cartPanel = _upgradePanelContainer.GetComponentInChildren<CartPanel>();
        }

        private void OnEnable()
        {
            foreach (var upgradePanel in _upgradePanels)
            {
                upgradePanel.UpgradeParametr += OnTapUp;
            }

            _adsSetter.OnRewardViwed += OnRewardViwed;
        }
        
        private void Start()
        {
            _upgradePanelContainer.gameObject.SetActive(false);
            _rewardedAdsButton.gameObject.SetActive(false);
        }

        private void OnRewardViwed()
        {
            OnTapUp(_numberUpgrade);
        }
        
        private void OnTapUp(int numberUpgrade)
        {
            _numberUpgrade = numberUpgrade;
            
            int price = _upgradeParametrs.GetPrice(numberUpgrade);
            bool canUpLevel = _upgradeParametrs.CanUpLevel(numberUpgrade);

            if (price <= _wallet.Money & canUpLevel==true)
            {

                    _wallet.RemoveResource(price);
                    _upgradeParametrs.OnTapUp(numberUpgrade);
            }
            else if(canUpLevel == false)
            {
                EmptyWallet?.Invoke();
            }
            else
            {
                _rewardedAdsButton.gameObject.SetActive(true);
            }
        }

        

        private void InitializeUpgradePanels()
        {
          int  numberUpgrade = 0;
            
            foreach (UpgradePanel upgradePanel in _upgradePanelContainer.GetComponentsInChildren<UpgradePanel>())
            {

                upgradePanel.Initialize(numberUpgrade,_upgradeParametrs);
                
                _upgradePanels.Add(upgradePanel);

                numberUpgrade++;
            }
        }
        
        private void OnDisable()
        {
            for (int i = 0; i < _upgradePanels.Count; i++)
            {
                _upgradePanels[i].UpgradeParametr -= OnTapUp;
            }
        }
    }
}