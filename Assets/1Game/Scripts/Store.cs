using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    [SerializeField] private UpgradePlace _upgradePlace;
    [SerializeField] private Wallet _wallet;

    public int FuelLevel{get=>currentFuelLevel;set{;}}
    public int PowerLevel{get=>currentPowerLevel;set{;}}
    public int CartLevel{get=>currentCartLevel;set{;}}
    
    private UpgradePanel _upgradePanel;

    private List<Level> _fuelLevels;
    private List<Level> _powerLevels;
    private List<Level> _cartLevels;

    public UnityAction BuyUpdate;

    private int maxFuelLevel;
    private int currentFuelLevel;
    private int minFuelLevel;

    private int maxPowerLevel;
    private int currentPowerLevel;
    private int minPowerLevel;

    private int maxCartLevel;
    private int currentCartLevel;
    private int minCartLevel;

    private int maxLevel;
    private int minLevel;
    private int stepUp;
    private int currentValue;
    private int _price;

    private void Awake()
    {
        _upgradePanel = GetComponentInChildren<UpgradePanel>();
        _upgradePanel.gameObject.SetActive(false);
        _fuelLevels = new List<Level>();
        _powerLevels = new List<Level>();
        _cartLevels = new List<Level>();

        Level fuelLevel = new Level();
        Level powerLevel = new Level();
        Level cartLevel = new Level();

        LoadParametrs();

        maxFuelLevel = 10;
        minFuelLevel = 50;
        stepUp = 20;
        currentValue = minLevel;
        _price = 100;

        InitializeLevels(fuelLevel);

        maxLevel = 24;
        minLevel = 1;
        stepUp = 1;
        currentValue = minLevel;

        InitializeLevels(powerLevel);

        //maxLevel = 10;
        //minLevel = 50;
        //stepUp = 20;
        //tempValue = minLevel;
        //_price = 500;

        //InitializeFuel(cartLevel);
    }

    private void OnEnable()
    {
        _upgradePlace.EnterPlace += OnEnterPlace;
        _upgradePlace.ExitPlace += OnTapClose;
        _upgradePanel.TapUpFuel += OnTapUpFuel;
        _upgradePanel.TapUpPower += OnTapUpPower;
        _upgradePanel.TapUpCart += OnTapUpCart;
        _upgradePanel.Close += OnTapClose;
    }

    private void OnDisable()
    {
        _upgradePlace.EnterPlace -= OnEnterPlace;
        _upgradePlace.ExitPlace -= OnTapClose;
        _upgradePanel.TapUpFuel -= OnTapUpFuel;
        _upgradePanel.TapUpPower -= OnTapUpPower;
        _upgradePanel.TapUpCart -= OnTapUpCart;
        _upgradePanel.Close -= OnTapClose;
    }

    private void OnTapUpFuel()
    {
        OnTapUp(_fuelLevels, ref currentFuelLevel);       
    }

    private void OnTapUpPower()
    {
        OnTapUp(_powerLevels, ref currentPowerLevel);
    }

    private void OnTapUpCart()
    {
        OnTapUp(_cartLevels, ref currentCartLevel);
    }

    private void OnTapClose()
    {
        _upgradePanel.gameObject.SetActive(false);
    }
    
    private void OnEnterPlace()
    {
        _upgradePanel.gameObject.SetActive(true);
    }
    
    private void OnTapUp(List<Level> _levels, ref int currentLevel)
    {
        if (_levels[currentLevel + 1].price <= _wallet.Money)
        {
            _wallet.RemoveResource(_levels[currentLevel + 1].price);
            currentLevel++;
        }
    }

    private void InitializeLevels(Level level)
    {
        for (int i = 0; i < maxLevel; i++)
        {
            level.stepUp = stepUp;
            level.tempValue = minLevel;
            level.price = _price;

            _fuelLevels.Add(level);
            currentValue += stepUp;
            _price *= 2;
        }
    }

    public void LoadParametrs()
    {
        currentFuelLevel=0;
        currentPowerLevel=0;
        currentCartLevel=0;
    }
}

class Level
{
    public int stepUp { get; set; }
    public int tempValue { get; set; }
    public int price { get; set; }
}