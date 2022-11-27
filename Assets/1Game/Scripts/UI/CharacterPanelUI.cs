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
        [SerializeField] private CharacterTrashBagPicker _character;

        public UnityAction UpPower;

        private void Start()
        {
            SetUpdate();
        }
        public void TapFuel()
        {
            UpPower?.Invoke();
        }

        private void SetUpdate()
        {
            _powerPrice.text = Convert.ToString(_character.Level);
        }
    }
}