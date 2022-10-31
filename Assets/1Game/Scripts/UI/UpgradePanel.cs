using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private TMP_Text _fuelPrice;
        [SerializeField] private TMP_Text _powerPrice;
        [SerializeField] private TMP_Text _cartPrice;



        private Store _store;
        private GameObject _panel;

        public UnityAction TapUpFuel;
        public UnityAction TapUpPower;
        public UnityAction TapUpCart;

        private void Start()
        {
            _store = GetComponentInParent<Store>();
            _panel = GetComponentInChildren<Image>().gameObject;
            _panel.SetActive(false);

            _fuelPrice.text = _store.FuelLevel.ToString();
            _powerPrice.text = _store.PowerLevel.ToString();
            _cartPrice.text = _store.CartLevel.ToString();

        }

        private void OnEnable()
        {
            _upgradePlace.EnterPlace += OnChangeStatePanel;
        }

        private void OnDisable()
        {
            _upgradePlace.EnterPlace -= OnChangeStatePanel;
        }

        private void OnChangeStatePanel()
        {
            _panel.SetActive(true);
        }

        public void TapFuel()
        {
            TapUpFuel?.Invoke();
        }

        public void TapPower()
        {
            TapUpPower?.Invoke();
        }

        public void TapCart()
        {
            TapUpCart?.Invoke();
        }

        public void TapClose()
        {
            _panel.SetActive(false);
        }
    }
}


