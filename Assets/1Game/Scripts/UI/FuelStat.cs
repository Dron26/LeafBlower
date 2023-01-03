using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.Item;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class FuelStat : MonoBehaviour
    {
        [SerializeField] private FuelChanger _fuelChanger;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;

        private float _maxValue;
        private float _minValue;
        private Slider _slider;

        

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void OnEnable()
        {
            _fuelChanger.ChangeFuel += OnChangeLevel;
            _upgradeParametrs.UpFuel += OnUpLevel;
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
            _upgradeParametrs.UpFuel -= OnUpLevel;
        }

        private void OnChangeLevel(float currentValue)
        {
            _maxValue = _fuelChanger.MaxFueLevel;
            _slider.value = Mathf.Clamp(currentValue, _minValue, _maxValue);
        }

        private void OnUpLevel(int value)
        {
            _maxValue = value;
            _slider.maxValue = _maxValue;
        }
    }
}