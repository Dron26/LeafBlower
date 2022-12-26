using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.Events;
using Service;

namespace Core
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        
        public UnityAction BuyUpdate;
        public UnityAction EmptyWallet;
        public UnityAction UpLevel;



        private CartPanel _cartPanel;
        private CharacterPanelUI _characterPanel;
        private FuelPanel _fuelPanel;
        private UpgradePanel _upgradePanel;

        private int numberUpgrade;

        private void Awake()
        {
            _upgradePanel = GetComponentInChildren<UpgradePanel>();
            _upgradePanel.gameObject.SetActive(false);
            _fuelPanel = _upgradePanel.GetComponentInChildren<FuelPanel>();
            _characterPanel = _upgradePanel.GetComponentInChildren<CharacterPanelUI>();
            _cartPanel = _upgradePanel.GetComponentInChildren<CartPanel>();
        }

        private void OnEnable()
        {
            _upgradePlace.EnterPlace += OnEnterPlace;
            _upgradePlace.ExitPlace += OnTapClose;
            _fuelPanel.UpFuel += OnTapUpFuel;
            _characterPanel.UpPower += OnTapUpPower;
            _cartPanel.UpCart += OnTapUpCart;
        }

        

        private void OnEnterPlace()
        {
            _upgradePanel.gameObject.SetActive(true);
        }

        private void OnTapUpFuel()
        {
            numberUpgrade = 0;

        }


        private void OnTapUpPower()
        {
            numberUpgrade = 1;
            OnTapUp(numberUpgrade);
        }

        private void OnTapUpCart()
        {
            numberUpgrade = 2;
            OnTapUp(numberUpgrade);
        }

        private void OnTapUp(int numberUpgrade)
        {
            int price = _upgradeParametrs.GetPrice(numberUpgrade);
            bool canUpLevel = _upgradeParametrs.CanUpLevel(numberUpgrade);
            
            if (price<= _wallet.Money & canUpLevel==true)
            {
                _wallet.RemoveResource(price);
                _upgradeParametrs.OnTapUp(numberUpgrade);
            }
            else
            {
                EmptyWallet?.Invoke();
            }
        }

        private void OnTapClose()
        {
            _upgradePanel.gameObject.SetActive(false);
        }
        
       



        private void OnDisable()
        {
            _upgradePlace.EnterPlace -= OnEnterPlace;
            _upgradePlace.ExitPlace -= OnTapClose;
            _fuelPanel.UpFuel -= OnTapUpFuel;
            _characterPanel.UpPower -= OnTapUpPower;
            _cartPanel.UpCart -= OnTapUpCart;
        }


    }
}