using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class FuelPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fuelPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;

        public UnityAction UpFuel;

        private void Start()
        {
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
        }
    }
}