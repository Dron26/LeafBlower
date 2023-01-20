using System;
using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class CartPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cartPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        [SerializeField] private SliderPanel _slider;
        [SerializeField] private Image _upIcon;
        
        private Button _buttonMaxLevel;
        public UnityAction UpCart;
        private Button _button;


        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _buttonMaxLevel = GetComponentInChildren<MaxLevelButton>().GetComponent<Button>();
        }

        private void Start()
        {
            _buttonMaxLevel.enabled = false;
            _slider.SetParametr(_upgradeParametrs.MaxCartLevel);
            _upIcon.enabled = true;
            SetUpdate();
        }

        public void TapCart()
        {
            UpCart?.Invoke();
            SetUpdate();
        }

        private void SetUpdate()
        {
            _cartPrice.text = Convert.ToString(_upgradeParametrs.CartPrice);
            int level = _upgradeParametrs.CartLevel;
            _slider.OnUpLevel(level);
            
            if (level==_upgradeParametrs.MaxCartLevel)
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