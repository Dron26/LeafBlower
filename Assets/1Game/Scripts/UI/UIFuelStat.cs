using Core;
using UnityEngine;
using UnityEngine.UI;
using Service;

namespace UI
{
    public class UIFuelStat : MonoBehaviour
    {
        [SerializeField] private FuelChanger _fuelChanger;
        [SerializeField] private Store _store;
        
        private float _maxValue;
        private float _minValue;
        private Slider _slider;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }
        
        public void OnEnable()
        {
            _fuelChanger.ChangeFuel += OnChangeLevel;
            _store.UpFuel += OnUpLevel;
        }

        private void Start()
        {
            _minValue = 0;
            _maxValue = _fuelChanger.MaxFueLevel;
            _slider.maxValue = _maxValue;
            _slider.minValue = _minValue;
            _slider.value = _slider.maxValue;
        }

        public void OnDisable()
        {
            _fuelChanger.ChangeFuel -= OnChangeLevel;
            _store.UpFuel -= OnUpLevel;
        }

        private void OnChangeLevel(float currentValue)
        {
            _maxValue = _fuelChanger.MaxFueLevel;
            _slider.value = Mathf.Clamp(currentValue, _minValue, _maxValue);
        }
        
        private void OnUpLevel(int value,int level)
        {
            _maxValue = value;
        }
    }
}

