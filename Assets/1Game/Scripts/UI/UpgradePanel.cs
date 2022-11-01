using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private TMP_Text _fuelPrice;
        [SerializeField] private TMP_Text _powerPrice;
        [SerializeField] private TMP_Text _cartPrice;

        private Store _store;
        public UnityAction TapUpFuel;
        public UnityAction TapUpPower;
        public UnityAction TapUpCart;
        public UnityAction Close;

        private void Awake()
        {
            _store = GetComponentInParent<Store>();
        }

        private void OnEnable()
        {
            _store.BuyUpdate += SetUpdate;
        }

        private void Start()
        {          
            SetUpdate();
        }

        private void OnDisable()
        {
            _store.BuyUpdate += SetUpdate;
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
            Close?.Invoke();
        }

        private void SetUpdate()
        {
            _fuelPrice.text = _store.FuelLevel.ToString();
            _powerPrice.text = _store.PowerLevel.ToString();
            _cartPrice.text = _store.CartLevel.ToString();
        }
    }
}


