using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class Store : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgradePanel;


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
        TapUpFuel?.Invoke();
    }

    private void OnTapUpPower()
    {
        TapUpPower?.Invoke();
    }

    private void OnTapUpCart()
    {
        TapUpCart?.Invoke();
    }


}
