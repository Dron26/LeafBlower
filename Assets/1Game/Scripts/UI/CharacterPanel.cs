using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class CharacterPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _powerPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;

        public UnityAction UpPower;

        private void Start()
        {
            SetUpdate();
        }

        public void TapPower()
        {
            UpPower?.Invoke();
        }

        private void SetUpdate()
        {
            _powerPrice.text = Convert.ToString(_upgradeParametrs.PowerPrice);
        }
    }
}