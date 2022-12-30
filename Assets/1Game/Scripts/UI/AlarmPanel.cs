using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.Item;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class AlarmPanel : MonoBehaviour
    {
        [SerializeField] private StageController _stageController;
        [SerializeField] private FuelChanger _fuelChanger;

        private CharacterTrashBagPicker _characterTrashBag;
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
            _stageController.SetCharacter += OnSetCharacter;
            _fuelChanger.ReachedMinLevel += StartAlarm;
        }

        private void OnDisable()
        {
            _stageController.SetCharacter -= OnSetCharacter;
            _characterTrashBag.SallAllTrashBag -= OnSellTrashBag;
            _fuelChanger.ReachedMinLevel -= StartAlarm;
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            if (_isSend != false) return;
            _alarmTrashBagPanel.gameObject.SetActive(true);
            _isSend = true;
        }

        private void StartAlarm(bool isWork)
        {
            _minFuelAlarm.gameObject.SetActive(isWork);
        }

        private void OnSellTrashBag()
        {
            _alarmTrashBagPanel.gameObject.SetActive(false);
            _isSend = false;
        }

        private void OnSetCharacter(Core.Character character)
        {
            _characterTrashBag = character.GetComponent<CharacterTrashBagPicker>();
            _characterTrashBag.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _characterTrashBag.SallAllTrashBag += OnSellTrashBag;
        }
    }
}