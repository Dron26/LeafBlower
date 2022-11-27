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
        
    public int FuelLevel{get=> _fuelLevels[currentFuelLevel].price;set{;}}
    public int PowerLevel{get=> _powerLevels[currentPowerLevel].price; set{;}}
    public int CartLevel{get=> _cartLevels[currentCartLevel].price ; set{;}}

        private CartPanel _cartPanel;
        private CharacterPanelUI _characterPanel;
        private FuelPanel _fuelPanel;

    private UpgradePanel _upgradePanel;

    private List<Level> _fuelLevels;
    private List<Level> _powerLevels;
    private List<Level> _cartLevels;

    public UnityAction BuyUpdate;
    public UnityAction EmptyWallet;

    public UnityAction UpFuel;


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

        CreateLevel();
        FillLevels(_cartLevels);
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
        if (OnTapUp(_fuelLevels, ref currentFuelLevel, maxFuelLevel) ==true )
        {
            UpFuel?.Invoke();
        }
    }

    private void OnTapUpPower()
    {
        //OnTapUp(_powerLevels, ref currentPowerLevel);
    }

    private void OnTapUpCart()
    {
        //OnTapUp(_cartLevels, ref currentCartLevel);
    }

    private void OnTapClose()
    {
        _upgradePanel.gameObject.SetActive(false);
    }
    
    private bool OnTapUp(List<Level> _levels, ref int currentLevel,int maxLevel)
    {
        bool canBuy=false;

        if (_levels[currentLevel].price <= _wallet.Money& currentLevel<=maxLevel)
        {
            _wallet.RemoveResource(_levels[currentLevel].price);
            currentLevel++;
            canBuy = true;
        }
        else
        {
            EmptyWallet?.Invoke();
        }

        return canBuy;
    }

    private void FillLevels(List<Level> levels)
    {
        for (int i = 0; i < maxLevel; i++)
        {
            Level tempLevel = new Level();
            tempLevel.stepUp = stepUp;
            tempLevel.tempValue = minLevel;
            tempLevel.price = _price;

            levels.Add(tempLevel);
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

    private void OnDisable()
    {
            _upgradePlace.EnterPlace -= OnEnterPlace;
            _upgradePlace.ExitPlace -= OnTapClose;
            _fuelPanel.UpFuel -= OnTapUpFuel;
            _characterPanel.UpPower -= OnTapUpPower;
            _cartPanel.UpCart -= OnTapUpCart;
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

        maxLevel = 10;
        minLevel = 50;
        stepUp = 20;
        currentValue = minLevel;
        _price = 100;

        FillLevels(_fuelLevels);

        maxLevel = 24;
        minLevel = 1;
        stepUp = 1;
        currentValue = minLevel;
        _price = 100;

        FillLevels(_powerLevels);

        maxLevel = 10;
        minLevel = 50;
        stepUp = 20;
        currentValue = minLevel;
        _price = 500;
    }
}
}