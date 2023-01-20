using System;
using System.Collections.Generic;
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


        private void Start()
        {
            Debug.Log("Start");
        }

        private void OnEnable()
        {
            foreach (var upgradePanel in _upgradePanels)
            {
                upgradePanel.UpgradeParametr += OnTapUp;
            }
        }
        
        private void OnTapUp(int numberUpgrade)
        {
            int price = _upgradeParametrs.GetPrice(numberUpgrade);
            bool canUpLevel = _upgradeParametrs.CanUpLevel(numberUpgrade);

            if (price <= _wallet.Money & canUpLevel == true)
            {
                if (canUpLevel == true)
                {
                    _wallet.RemoveResource(price);
                    _upgradeParametrs.OnTapUp(numberUpgrade);
                }
                else
                {
                    RachedMaxLevel?.Invoke(numberUpgrade);
                }
                
            }
            else
            {
                EmptyWallet?.Invoke();
            }
        }

        private void InitializeUpgradePanels()
        {
            _numberUpgrade = 0;
            
            foreach (UpgradePanel upgradePanel in _upgradePanelContainer.GetComponentsInChildren<UpgradePanel>())
            {

                upgradePanel.Initialize(_numberUpgrade,_upgradeParametrs);
                
                _upgradePanels.Add(upgradePanel);

                _numberUpgrade++;
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