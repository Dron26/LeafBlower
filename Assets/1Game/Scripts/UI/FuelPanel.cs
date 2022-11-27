using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Service;
using System;

namespace UI
{
    public class FuelPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fuelPrice;
        [SerializeField] private FuelTank _fuelLevel;
        public UnityAction UpFuel;

        private void Start()
        {
            SetUpdate();
        }
        public void TapFuel()
        {
            UpFuel?.Invoke();
        }

        private void SetUpdate()
        {
            _fuelPrice.text = Convert.ToString(_fuelLevel.MaxFueLevel);
        }
    }
}

