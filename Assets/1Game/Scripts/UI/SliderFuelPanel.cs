using _1Game.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class SliderFuelPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        
        private Slider _slider;

        private float _maxValue;
        private float _minValue;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void OnEnable()
        {
            _upgradeParametrs.UpFuel += OnUpLevel;
        }

        private void Start()
        {
            _minValue = 0;
            _maxValue = _upgradeParametrs.MaxFuelLevel;
            _slider.maxValue = _maxValue;
            _slider.minValue = _minValue;
            _slider.value = _slider.minValue;
        }

        private void OnUpLevel(int value)
        {
            _slider.value = Mathf.Clamp(value, _minValue, _maxValue);
        }

        public void OnDisable()
        {
            _upgradeParametrs.UpFuel -= OnUpLevel;
        }
    }
}