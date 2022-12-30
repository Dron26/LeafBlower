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
        private UpgradePanel _upgradePanel;

        public UnityAction BuyUpdate;
        public UnityAction EmptyWallet;
        public UnityAction UpLevel;

        private int numberUpgrade;

        private void Awake()
        {
            _upgradePanel = GetComponentInChildren<UpgradePanel>();
            _upgradePanel.gameObject.SetActive(false);
            _fuelPanel = _upgradePanel.GetComponentInChildren<FuelPanel>();
            _characterPanel = _upgradePanel.GetComponentInChildren<CharacterPanel>();
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

            if (price <= _wallet.Money & canUpLevel == true)
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