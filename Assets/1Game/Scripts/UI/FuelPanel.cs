using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class FuelPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fuelPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        [SerializeField] private SliderPanel _slider;
        [SerializeField] private Image _upIcon;

        private Button _button;
        public UnityAction UpFuel;
        private Button _buttonMaxLevel;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _buttonMaxLevel = GetComponentInChildren<MaxLevelButton>().GetComponent<Button>();
        }
        
        private void Start()
        {
            _buttonMaxLevel.enabled = false;
            _slider.SetParametr(_upgradeParametrs.MaxFuelLevel);
            _upIcon.enabled = true;
            SetUpdate();
        }

        public void TapFuel()
        {
            UpFuel?.Invoke();
            SetUpdate();
        }

        private void SetUpdate()
        {
            _fuelPrice.text = Convert.ToString(_upgradeParametrs.FuelPrice);
            int level = _upgradeParametrs.FuelLevel;
            _slider.OnUpLevel(level);

            if (level==_upgradeParametrs.MaxFuelLevel)
            {
                ReachleMaxLevel();
            }
        }

        private void ReachleMaxLevel()
        {
            _button.interactable=false;
                _buttonMaxLevel.enabled = true;
                _upIcon.enabled = false;
        }
    }
}