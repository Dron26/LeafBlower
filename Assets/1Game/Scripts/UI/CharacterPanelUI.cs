using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Service;
using TMPro;
using UnityEngine.Events;
using System;

namespace UI
{
    public class CharacterPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _powerPrice;
        [SerializeField] private Store _store;

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
            _powerPrice.text = Convert.ToString(_store.PowerPrice);
        }
    }
}