using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class UpgradeParametrs : MonoBehaviour
    {
        [SerializeField] private StageController _stageController;
        [SerializeField] private ParticleSystem _particleSystem;
        public int FuelPrice => GetPrice(_numberFuelUpgrade);
        public int PowerPrice => GetPrice(_numberPowerUpgrade);
        public int CartPrice => GetPrice(_numberCartUpgrade);

        public int FuelLevel => GetLevel(_numberFuelUpgrade);
        public int PowerLevel => GetLevel(_numberPowerUpgrade);
        public int CartLevel => GetLevel(_numberCartUpgrade);

        public int MaxFuelLevel => GetMaxLevel(_numberFuelUpgrade);
        public int MaxPowerLevel => GetMaxLevel(_numberPowerUpgrade);
        public int MaxCartLevel => GetMaxLevel(_numberCartUpgrade);

        public List<Upgrade> _upgrades = new();

        public List<UnityAction<int>> Actions = new();

        private int _maxLevel;
        private int _stepUp;
        private int _value;
        private int _price;

        private int _currentFuelLevel = 0;
        private int _numberFuelUpgrade = 0;

        private int _currentPowerLevel = 0;
        private int _numberPowerUpgrade = 1;

        private int _currentCartLevel = 0;
        private int _numberCartUpgrade = 2;

        private Upgrade _fuelUpgrade;
        private Upgrade _powerUpgrade;
        private Upgrade _cartUpgrade;

        public UnityAction<int> UpFuel;
        public UnityAction<int> UpPower;
        public UnityAction<int> UpCart;

        public void Initialize()
        {
            CreateLevel();
        }
        
        private void Start()
        {
            SetStartParametr();
        }

        private void CreateLevel()
        {
            int number = 0;

            _maxLevel = 10;
            _stepUp = 20;
            _price = 100;
            _value = 100;

            FillLevel(ref _fuelUpgrade);
            FillUpgradeList(_fuelUpgrade, number);
            number++;

            _maxLevel = 10;
            _stepUp = 1;
            _price = 300;
            _value = 6;

            FillLevel(ref _powerUpgrade);
            FillUpgradeList(_powerUpgrade, number);
            number++;

            _maxLevel = 10;
            _stepUp = 1;
            _price = 500;
            _value = 6;

            FillLevel(ref _cartUpgrade);
            FillUpgradeList(_cartUpgrade, number);

            _upgrades[_numberFuelUpgrade].UpgradeUP += UpFuel;
            _upgrades[_numberPowerUpgrade].UpgradeUP += UpPower;
            _upgrades[_numberCartUpgrade].UpgradeUP += UpCart;
        }

        private void FillLevel(ref Upgrade upgrade)
        {
            List<Level> levels = new List<Level>();
            int currentLevel = 0;

            for (int i = 0; i <= _maxLevel; i++)
            {
                Level tempLevel = new Level(_value, _price);

                levels.Add(tempLevel);

                _value += _stepUp;
                _price *= 2;
            }

            upgrade = new Upgrade(levels, currentLevel, _maxLevel);
        }

        private void FillUpgradeList(Upgrade upgrade, int number)
        {
            upgrade.SetNumber(number);


            _upgrades.Add(upgrade);
        }

        private void UpdateLevels()
        {
            _currentFuelLevel = _upgrades[_numberFuelUpgrade].CurrentLevel;
            _currentPowerLevel = _upgrades[_numberPowerUpgrade].CurrentLevel;
            _currentCartLevel = _upgrades[_numberCartUpgrade].CurrentLevel;
        }

        private void SetStartParametr()
        {
            UpFuel?.Invoke(_upgrades[_numberFuelUpgrade].Levels[_currentFuelLevel].Value);
            UpPower?.Invoke(_upgrades[_numberPowerUpgrade].Levels[_currentPowerLevel].Value);
            UpCart?.Invoke(_upgrades[_numberCartUpgrade].Levels[_currentCartLevel].Value);
        }

        public void OnTapUp(int numberUpgrade)
        {
            _particleSystem.Stop();
            _particleSystem.Play();
            _upgrades[numberUpgrade].UpLevel();
            int value = GetLevelValue(numberUpgrade);

            UpdateLevels();
            
            for (int i = 0; i < _upgrades.Count; i++)
            {
                if (i == numberUpgrade)
                {
                    _upgrades[i].UpgradeUP?.Invoke(value);
                }
            }
            
        }

        public int GetPrice(int numberUpgrade)
        {
            return _upgrades[numberUpgrade].Levels[_upgrades[numberUpgrade].CurrentLevel].Price;
        }

        public bool CanUpLevel(int numberUpgrade)
        {
            return _upgrades[numberUpgrade].CurrentLevel < _upgrades[numberUpgrade].MaxLevel;
        }

        public int GetLevel(int numberUpgrade)
        {
            return _upgrades[numberUpgrade].CurrentLevel;
        }

        private int GetLevelValue(int numberUpgrade)
        {
            return _upgrades[numberUpgrade].Levels[_upgrades[numberUpgrade].CurrentLevel].Value;
        }

        public int GetMaxLevel(int numberUpgrade)
        {
            return _upgrades[numberUpgrade].MaxLevel;
        }


        public List<Upgrade> GetUpgrades()
        {
            List<Upgrade> _tempUpgrades = new List<Upgrade>();

            for (int i = 0; i < _upgrades.Count; i++)
            {
                _tempUpgrades.Add(_upgrades[i]);
            }

            return _tempUpgrades;
        }

        public void SetUpgrades(List<Upgrade> tempUpgrades)
        {
            for (int i = 0; i < tempUpgrades.Count; i++)
            {
                _upgrades.Add(tempUpgrades[i]);
            }
        }

        private void OnDisable()
        {
            _upgrades[_numberFuelUpgrade].UpgradeUP -= UpFuel;
            _upgrades[_numberPowerUpgrade].UpgradeUP -= UpPower;
            _upgrades[_numberCartUpgrade].UpgradeUP -= UpCart;
        }
    }
}