using UnityEngine;
using Service;
using Core;

namespace UI
{
    public class UIAlarmPanel : MonoBehaviour
    {
        private CharacterTrashBagPicker _characterTrashBag;


        [SerializeField] private StageController _stageController;
        [SerializeField] private FuelChanger _fuelChanger;
        private bool _isSend;
        private MaxTrashBagAlarm _alarmTrashBagPanel;
        private MinFuelAlarm _minFuelAlarm;

        private void Awake()
        {
            _alarmTrashBagPanel = GetComponentInChildren<MaxTrashBagAlarm>();
            _alarmTrashBagPanel.gameObject.SetActive(false);

            _minFuelAlarm = GetComponentInChildren<MinFuelAlarm>();
            _minFuelAlarm.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _stageController.SetCharacter += OSetCharacter;
            _fuelChanger.EndFuel += OnEndFuel;
        }

        private void OnDisable()
        {
            _stageController.SetCharacter -= OSetCharacter;
            _characterTrashBag.SellTrashBag -= OnSellTrashBag;
            _fuelChanger.EndFuel -= OnEndFuel;
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            if (_isSend == false)
            {
                _alarmTrashBagPanel.gameObject.SetActive(true);
                _isSend = true;
            }
        }

        private void OnEndFuel()
        {
            _minFuelAlarm.gameObject.SetActive(true);
        }

        private void OnSellTrashBag()
        {
            _alarmTrashBagPanel.gameObject.SetActive(false);
            _isSend = false;
        }

        private void OSetCharacter(Character character)
        {
            _characterTrashBag = character.GetComponent<CharacterTrashBagPicker>();
            _characterTrashBag.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _characterTrashBag.SellTrashBag += OnSellTrashBag;
        }
    }
}