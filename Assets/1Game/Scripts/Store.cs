using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class Store : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private Wallet _wallet;

    private List<Level> _fuelLevels;
    private List<Level> _powerLevels;
    private List<Level> _cartLevels;
    private int maxLevel;
    private int minLevel;
    private int stepUp;
    private int tempValue ;
    private int _price;

    private void Start()
    {
        _fuelLevels = new List<Level>();
        _powerLevels = new List<Level>();
        _cartLevels = new List<Level>();

        Level fuelLevel = new Level();
        Level powerLevel = new Level();
        Level cartLevel = new Level();

        maxLevel = 10;
        minLevel = 50;
        stepUp = 20;
        tempValue = minLevel;
        _price = 100;

        Initialize(fuelLevel);

        maxLevel = 24;
        minLevel = 1;
        stepUp = 1;
        tempValue = minLevel;

        Initialize(powerLevel);

        //maxLevel = 10;
        //minLevel = 50;
        //stepUp = 20;
        //tempValue = minLevel;
        //_price = 500;

        //InitializeFuel(cartLevel);
    }

    private void OnEnable()
    {
        _upgradePanel.TapUpFuel += OnTapUpFuel;
        _upgradePanel.TapUpPower += OnTapUpPower;
        _upgradePanel.TapUpCart += OnTapUpCart;
    }

    private void OnDisable()
    {
        _upgradePanel.TapUpFuel -= OnTapUpFuel;
        _upgradePanel.TapUpPower -= OnTapUpPower;
        _upgradePanel.TapUpCart -= OnTapUpCart;
    }

    private void OnTapUpFuel()
    {
        //if (item.Price <= _wallet.Money)
        //{
        //    _wallet.RemoveResource(item.Price);
        //    item.Buy();
        //    view.SellButtonClick -= OnSellButtonClick;
        //    _harvester.BuySpareParts(item);
        //}
    }

    private void OnTapUpPower()
    {
        
    }

    private void OnTapUpCart()
    {
        
    }

    private void Initialize(Level level)
    {
        level.maxLevel = maxLevel;
        level.minLevel = minLevel;
        level.stepUp = stepUp;
        level.tempValue = minLevel;
        
        for (int i = 0; i < maxLevel; i++)
        {
            _fuelLevels.Add(level);
            level.tempValue += level.stepUp;
            _price *= 2;
        }
    }
}

class Level
{
    public int maxLevel { get; set; }
    public int minLevel { get; set; }
    public int stepUp { get; set; }
    public int tempValue { get; set; }
}
