using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class CharacterPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _powerPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        [SerializeField] private SliderPanel _slider;
        [SerializeField] private Image _upIcon;
        public UnityAction UpPower;
        private Button _button;
        private Button _buttonMaxLevel;
        


        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _buttonMaxLevel = GetComponentInChildren<MaxLevelButton>().GetComponent<Button>();
        }
        private void Start()
        {
            _buttonMaxLevel.enabled = false;
            _slider.SetParametr(_upgradeParametrs.MaxPowerLevel);
            _upIcon.enabled = true;
            SetUpdate();
        }

        public void TapPower()
        {
            UpPower?.Invoke();
            SetUpdate();
        }

        private void SetUpdate()
        {
            _powerPrice.text = Convert.ToString(_upgradeParametrs.PowerPrice);
            int level = _upgradeParametrs.PowerLevel;
            _slider.OnUpLevel(level);
            
            if (level==_upgradeParametrs.MaxPowerLevel)
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