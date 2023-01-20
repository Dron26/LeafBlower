using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.Item;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class AlarmPanel : MonoBehaviour
    {
        [SerializeField] private FuelChanger _fuelChanger;
        [SerializeField] private CharacterTrashBagPicker _characterTrashBag;
        
        private MaxTrashBagAlarm _alarmTrashBagPanel;
        private MinFuelAlarm _minFuelAlarm;

        private bool _isSend;

        private void Awake()
        {
            _alarmTrashBagPanel = GetComponentInChildren<MaxTrashBagAlarm>();
            _alarmTrashBagPanel.gameObject.SetActive(false);
            _minFuelAlarm = GetComponentInChildren<MinFuelAlarm>();
            _minFuelAlarm.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _characterTrashBag.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _characterTrashBag.SallAllTrashBag += OnSellTrashBag;
            _fuelChanger.ReachedMinLevel += StartAlarm;
        }

        private void OnDisable()
        {
            _characterTrashBag.SallAllTrashBag -= OnSellTrashBag;
            _fuelChanger.ReachedMinLevel -= StartAlarm;
            _characterTrashBag.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            if (_isSend != false) return;
            _alarmTrashBagPanel.gameObject.SetActive(true);
            _isSend = true;
        }

        private void StartAlarm()
        {
            _minFuelAlarm.gameObject.SetActive(true);
        }

        private void OnSellTrashBag()
        {
            _alarmTrashBagPanel.gameObject.SetActive(false);
            _isSend = false;
        }
    }
}