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

        public int FuelPrice  => _fuelLevels[_currentFuelLevel].price;
        public int PowerPrice  => _powerLevels[_currentPowerLevel].price; 
        public int CartPrice => _cartLevels[_currentCartLevel].price; 
        public int MaxFuelLevel   => _fuelLevels.Count; 
        public int MaxCartLevel   => _cartLevels.Count; 
        public int MaxPowerLewel  => _powerLevels.Count;
        
        

        public UnityAction BuyUpdate;
        public UnityAction EmptyWallet;

        public UnityAction<int,int> UpFuel;
        public UnityAction<int,int> UpPower;
        public UnityAction<int,int> UpCart;
        
        
        
        private CartPanel _cartPanel;
        private CharacterPanelUI _characterPanel;
        private FuelPanel _fuelPanel;
        private UpgradePanel _upgradePanel;

        private List<Level> _fuelLevels;
        private List<Level> _powerLevels;
        private List<Level> _cartLevels;



        private int _maxFuelLevel;
        private int _currentFuelLevel=0;

        private int _maxPowerLevel;
        private int _currentPowerLevel=0;

        private int _maxCartLevel;
        private int _currentCartLevel=0;

        private int _maxLevel;
        private int _stepUp;
        private int _value;
        private int _price;

        private void Awake()
        {
            _upgradePanel = GetComponentInChildren<UpgradePanel>();
            _upgradePanel.gameObject.SetActive(false);
            _fuelPanel = _upgradePanel.GetComponentInChildren<FuelPanel>();
            _characterPanel = _upgradePanel.GetComponentInChildren<CharacterPanelUI>();
            _cartPanel= _upgradePanel.GetComponentInChildren<CartPanel>();
            CreateLevel();

        }

        private void OnEnable()
        {
            _upgradePlace.EnterPlace += OnEnterPlace;
            _upgradePlace.ExitPlace += OnTapClose;
            _fuelPanel.UpFuel += OnTapUpFuel;
            _characterPanel.UpPower += OnTapUpPower;
            _cartPanel.UpCart += OnTapUpCart;
        }

        private void Start()
        {
            SetStartParametr();
        }

        private void CreateLevel()
        {
            _fuelLevels = new List<Level>();
            _powerLevels = new List<Level>();
            _cartLevels = new List<Level>();

            Level fuelLevel = new Level();
            Level powerLevel = new Level();
            Level cartLevel = new Level();

            LoadParametrs();

            _maxLevel = 10;
            _stepUp = 20;
            _price = 100;
            _value = 100;
            _maxFuelLevel = _maxLevel;

            FillLevel(_fuelLevels);

            _maxLevel = 10;
            _stepUp = 1;
            _price = 300;
            _value = 6;
            _maxPowerLevel = _maxLevel;

            FillLevel(_powerLevels);

            _maxLevel = 10;
            _stepUp = 1;
            _price = 500;
            _value = 6;
            _maxCartLevel = _maxLevel;

            FillLevel(_cartLevels);
        }

        private void FillLevel(List<Level> levels)
        {

            for (int i = 0; i < _maxLevel; i++)
            {
                Level tempLevel = new Level();
                tempLevel.stepUp = _stepUp;
                tempLevel.value = _value;
                tempLevel.price = _price;


                levels.Add(tempLevel);
                _value += _stepUp;
                _price *= 2;
            }
        }

        private void OnEnterPlace()
        {
            _upgradePanel.gameObject.SetActive(true);
        }

        private void OnTapUpFuel()
        {
            OnTapUp(_fuelLevels, ref _currentFuelLevel, _maxFuelLevel, UpFuel);
        }


        private void OnTapUpPower()
        {
            OnTapUp(_powerLevels, ref _currentPowerLevel, _maxPowerLevel, UpPower);

        }

        private void OnTapUpCart()
        {
            OnTapUp(_cartLevels, ref _currentCartLevel, _maxCartLevel, UpCart);
        }

        private void OnTapClose()
        {
            _upgradePanel.gameObject.SetActive(false);
        }
        
        private void OnTapUp(List<Level> levels, ref int currentLevel, int maxLevel,UnityAction<int,int> action)
        {
            if (levels[currentLevel].price <= _wallet.Money & currentLevel <= maxLevel)
            {
                _wallet.RemoveResource(levels[currentLevel].price);
                currentLevel++;
                action?.Invoke(levels[currentLevel].value,currentLevel);
            }
            else
            {
                EmptyWallet?.Invoke();
            }
        }

        public void LoadParametrs()
        {
            _currentFuelLevel = 0;
            _currentPowerLevel = 0;
            _currentCartLevel = 0;
        }

        private void OnDisable()
        {
            _upgradePlace.EnterPlace -= OnEnterPlace;
            _upgradePlace.ExitPlace -= OnTapClose;
            _fuelPanel.UpFuel -= OnTapUpFuel;
            _characterPanel.UpPower -= OnTapUpPower;
            _cartPanel.UpCart -= OnTapUpCart;
        }

        private void SetStartParametr()
        {
            UpFuel?.Invoke(_fuelLevels[_currentFuelLevel].value,_currentCartLevel);
            UpPower?.Invoke(_powerLevels[_currentPowerLevel].value,_currentCartLevel);
            UpCart?.Invoke(_cartLevels[_currentCartLevel].value,_currentCartLevel);
        }
    }
}