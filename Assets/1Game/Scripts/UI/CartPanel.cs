using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class CartPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cartPrice;
        [SerializeField] private UpgradeParametrs _upgradeParametrs;
        
        public UnityAction UpCart;

        private void Start()
        {
            SetUpdate();
        }
        
        public void TapCart()
        {
            UpCart?.Invoke();
        }

        private void SetUpdate()
        {
            _cartPrice.text = Convert.ToString(_upgradeParametrs.CartPrice);
        }
    }
}