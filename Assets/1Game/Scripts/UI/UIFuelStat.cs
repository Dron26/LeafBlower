using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Service;

namespace UI
{
[RequireComponent(typeof(Animator))]

public class UIFuelStat : MonoBehaviour
{
    [SerializeField] private FuelChanger _fuelChanger;

    private float _valueCurrent = 50;
    private float _maxValue;
    private float _minValue;
    private float _currentValue;
    private Slider _slider;
    private bool _isWork;
    private WaitForSeconds _waitForSeconds;

    private Animator _animator;
    private int _levelName;

    private void Awake()
    {
       _animator = GetComponent<Animator>();
        _levelName = Animator.StringToHash("Level");
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        
        _isWork = true;
        _maxValue = _fuelChanger.MaxFueLevell;
        _currentValue = _fuelChanger.FuelLevel;
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
        _slider.value = _currentValue;
        _animator.SetFloat(_levelName, _slider.value);
    }

    public void OnEnable()
    {
        _fuelChanger.ChangeFuel += OnChangeLevel;
    }

    public void OnDisable()
    {
        _fuelChanger.ChangeFuel -= OnChangeLevel;
    }


    private void OnChangeLevel(float currentValue)
    {
        _slider.value = Mathf.Clamp( currentValue, _minValue, _maxValue);

        _animator.SetFloat(_levelName, _slider.value);
    }


}
}

