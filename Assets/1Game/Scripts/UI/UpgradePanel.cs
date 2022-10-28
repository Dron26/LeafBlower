using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private UpgradePlace _upgradePlace;

        private GameObject _gameObject;

        public UnityAction TapUpFuel;
        public UnityAction TapUpPower;
        public UnityAction TapUpCart;


        private void Start()
        {
            _gameObject = GetComponentInChildren<Image>().gameObject;
            _gameObject.SetActive(false);
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
            _gameObject.SetActive(true);
        }

        private void TapFuel()
        {
            TapUpFuel?.Invoke();
        }

        private void TapPower()
        {
            TapUpPower?.Invoke();
        }

        private void TapCart()
        {
            TapUpCart?.Invoke();
        }
    }
}


