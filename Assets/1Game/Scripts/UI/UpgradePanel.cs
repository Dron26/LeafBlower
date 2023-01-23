using System;
using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.WorkPlaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _price;
         [SerializeField] private SliderPanel _slider;
        [SerializeField] private Image _upIcon;
        private UpgradeParametrs _upgradeParametrs;
        private Button _buyUpgradeButton;
        public UnityAction<int> UpgradeParametr;
        private Button _buttonMaxLevel;
        private int _numberUpgrade;
        private int _maxLevel;

        public void Initialize(int numberUpgrade,UpgradeParametrs upgradeParametrs)
        {
            _buyUpgradeButton = GetComponentInChildren<BuyUpgradeButton>().GetComponent<Button>();
            _buttonMaxLevel = GetComponentInChildren<MaxLevelButton>().GetComponent<Button>();
            
            _upgradeParametrs = upgradeParametrs;
            _numberUpgrade = numberUpgrade;
            _maxLevel = _upgradeParametrs.GetMaxLevel(_numberUpgrade);
            _buttonMaxLevel.enabled = false;
            _slider.SetParametr(_maxLevel);
            _upIcon.enabled = true;
            SetUpdate();
        }
        
        public void OnClick()
        {
            UpgradeParametr?.Invoke(_numberUpgrade);
            SetUpdate();
        }

        private void SetUpdate()
        {
            _price.text = Convert.ToString(_upgradeParametrs.GetPrice(_numberUpgrade));
            int level = _upgradeParametrs.GetLevel(_numberUpgrade);
            _slider.OnUpLevel(level);
            
            if (level==_maxLevel)
            {
                ReachleMaxLevel();
            }
        }

        private void ReachleMaxLevel()
        {
            _buyUpgradeButton.enabled = false;
            _buyUpgradeButton.gameObject.SetActive(false);
            _buttonMaxLevel.enabled = true;
            _upIcon.enabled = false;
            
        }
    }
}

