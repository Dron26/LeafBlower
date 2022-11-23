using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Service;

namespace UI
{
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
        private int _levelName;

        private void Awake()
        {
            _levelName = Animator.StringToHash("Level");
            _slider = GetComponentInChildren<Slider>();
        }
        public void OnEnable()
        {
            _fuelChanger.ChangeFuel += OnChangeLevel;
        }

        private void Start()
        {
            _minValue = 0;
            _isWork = true;
            _maxValue = _fuelChanger.MaxFueLevell;
            _currentValue = _fuelChanger.FuelLevel;
            _slider.maxValue = _maxValue;
            _slider.minValue = _minValue;
            _slider.value = _slider.maxValue;
        }

        public void OnDisable()
        {
            _fuelChanger.ChangeFuel -= OnChangeLevel;
        }

        private void OnChangeLevel(float currentValue)
        {
            _slider.value = Mathf.Clamp(currentValue, _minValue, _maxValue);
        }
    }
}

