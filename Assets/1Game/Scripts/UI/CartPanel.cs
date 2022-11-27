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
    public class CartPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cartPrice;
        [SerializeField] private CartTrashBagPicker  _cart;
        public UnityAction UpCart;

        private void Start()
        {
            SetUpdate();
        }
        public void TapFuel()
        {
            UpCart?.Invoke();
        }

        private void SetUpdate()
        {
            _cartPrice.text = Convert.ToString(_cart.Level);
        }
    }
}