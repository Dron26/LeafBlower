using UnityEngine;
using System.Timers;

namespace UI
{
    public class UIAlarmMessager : MonoBehaviour
    {
        [SerializeField] private CharacterTrashBagPicker _characterTrashBag;
        private bool _isSend;
        private AlarmPanel _alarmPanel;

        private void Awake()
        {
            _alarmPanel = GetComponentInChildren<AlarmPanel>();
            _alarmPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _characterTrashBag.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _characterTrashBag.SellTrashBag += OnSellTrashBag;
        }

        private void OnDisable()
        {
            _characterTrashBag.SellTrashBag += OnSellTrashBag;
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            if (_isSend == false)
            {
                _alarmPanel.gameObject.SetActive(true);
                _isSend = true;
            }
        }

        private void OnSellTrashBag()
        {
            _alarmPanel.gameObject.SetActive(false);
            _isSend = false;
        }
    }
}