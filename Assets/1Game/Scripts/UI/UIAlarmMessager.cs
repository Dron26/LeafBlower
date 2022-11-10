using UnityEngine;
using Service;
using Core;

namespace UI
{
    public class UIAlarmMessager : MonoBehaviour
    {
        private CharacterTrashBagPicker _characterTrashBag;


        [SerializeField] private StageController _stageController;
        private bool _isSend;
        private AlarmPanel _alarmPanel;

        private void Awake()
        {
            _alarmPanel = GetComponentInChildren<AlarmPanel>();
            _alarmPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _stageController.SetCharacter += OSetCharacter;
            
        }

        private void OnDisable()
        {
            _stageController.SetCharacter -= OSetCharacter;
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

        private void OSetCharacter(Character character)
        {
            _characterTrashBag = character.GetComponent<CharacterTrashBagPicker>();
            _characterTrashBag.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _characterTrashBag.SellTrashBag += OnSellTrashBag;
        }
    }
}