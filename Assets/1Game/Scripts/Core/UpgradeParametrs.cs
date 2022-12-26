using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeParametrs : MonoBehaviour
{

    public int FuelPrice  => GetPrice(_numberFuelUpgrade);
    public int PowerPrice  => GetPrice(_numberPowerUpgrade);
    public int CartPrice => GetPrice(_numberCartUpgrade); 
    public int MaxFuelLevel=>GetLevelValue(_numberFuelUpgrade);
    
    private int _maxLevel;
    private int _stepUp;
    private int _value;
    private int _price;

    private int _currentFuelLevel=0;
    private int _numberFuelUpgrade=0;
    

    private int _currentPowerLevel=0;
    private int _numberPowerUpgrade=1;

    private int _currentCartLevel=0;
    private int _numberCartUpgrade=2;
    
    private List<Upgrade>_upgrades = new List<Upgrade>();
    
    public List<UnityAction<int>> Actions = new List<UnityAction<int>>();
    private Upgrade _fuelUpgrade;
    private Upgrade _powerUpgrade;
    private Upgrade _cartUpgrade;
    
    public UnityAction<int> UpFuel;
    public UnityAction<int> UpPower;
    public UnityAction<int> UpCart;

    private void Awake()
    {
        
        
        CreateLevel();
        UpdateLevels();
    }

    void Start()
    {
        Actions.Add(UpFuel);
        Actions.Add(UpPower);
        Actions.Add(UpCart);
        
        SetStartParametr();
    }

    private void CreateLevel()
    {
        LoadParametrs();

        int number = 0;
        
        _maxLevel = 10;
        _stepUp = 20;
        _price = 100;
        _value = 100;

        FillLevel(ref _fuelUpgrade);
        FillUpgradeList( _fuelUpgrade,number);
        number++;

        _maxLevel = 10;
        _stepUp = 1;
        _price = 300;
        _value = 6;

        FillLevel(ref _powerUpgrade);
        FillUpgradeList( _powerUpgrade,number);
        number++;

        _maxLevel = 10;
        _stepUp = 1;
        _price = 500;
        _value = 6;

        FillLevel(ref _cartUpgrade);
        FillUpgradeList( _cartUpgrade,number);
    }
    
    private void FillLevel(ref Upgrade upgrade)
    {
        List<Level> levels=new List<Level>();
        int currentLevel = 0;
        
        for (int i = 0; i < _maxLevel; i++)
        {
            Level tempLevel = new Level(_value,_price);
            
            levels.Add(tempLevel);
            
            _value += _stepUp;
            _price *= 2;
        }

        upgrade = new Upgrade(levels, currentLevel, _maxLevel);
    }

    private void FillUpgradeList(Upgrade upgrade,int number)
    {
        upgrade.SetNumber(number);
        _upgrades.Add(upgrade);
    }
    
    private void SetStartParametr()
    {
        Actions[_numberFuelUpgrade]?.Invoke(_upgrades[_numberFuelUpgrade].Levels[_currentFuelLevel].Value);
        
        Actions[_numberPowerUpgrade]?.Invoke(_upgrades[_numberPowerUpgrade].Levels[_currentPowerLevel].Value);
       
        Actions[_numberCartUpgrade]?.Invoke(_upgrades[_numberCartUpgrade].Levels[_currentCartLevel].Value);
    }
    
    public void LoadParametrs()//int currentFuelLevel,int currentPowerLevel,int currentCartLevel
    {
        int level = 0;
        
        foreach (Upgrade upgrade in _upgrades)
        {
            upgrade.SetLevel(level);
        }
    }
    
    public void OnTapUp(int numberUpgrade)
    {
        _upgrades[numberUpgrade].UpLevel();
        int value = GetLevelValue(numberUpgrade);
        Actions[numberUpgrade]?.Invoke(value);
    }

    public int GetPrice(int numberUpgrade)
    {
        return _upgrades[numberUpgrade].Levels[_upgrades[numberUpgrade].CurrentLevel].Price;
    }

    public bool CanUpLevel(int numberUpgrade)
    {
        return _upgrades[numberUpgrade].CurrentLevel < _upgrades[numberUpgrade].MaxLevel;
    }

    private int GetLevelValue(int numberUpgrade)
    {
        return _upgrades[numberUpgrade].Levels[_upgrades[numberUpgrade].CurrentLevel].Value;
    }
    
    private void OnDisable()
    {

    }

    private void UpdateLevels()
    {
        _currentFuelLevel = _upgrades[_numberFuelUpgrade].CurrentLevel;
        _currentPowerLevel = _upgrades[_numberPowerUpgrade].CurrentLevel;
        _currentCartLevel = _upgrades[_numberCartUpgrade].CurrentLevel;
    }
}
