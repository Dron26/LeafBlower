using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFuelStat : MonoBehaviour
{
    [SerializeField] private FuelTank _fuelTank;

    private float _valueCurrent = 50;
    private float _maxValue;
    private float _minValue;
    private float _currentValue;
    private Slider _slider;
    private bool _isWork;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _isWork = true;
        _maxValue = _fuelTank.MaxFueLevell;
        _currentValue = _fuelTank.FuelLevel;
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
        _slider.value = _currentValue;
    }

    public void OnEnable()
    {
        _fuelTank.ChangeFuel += OnChangeLevel;
    }

    public void OnDisable()
    {
        _fuelTank.ChangeFuel -= OnChangeLevel;
    }


    private void OnChangeLevel(float currentValue)
    {
        _slider.value = Mathf.Clamp( currentValue, _minValue, _maxValue);
    }


}
