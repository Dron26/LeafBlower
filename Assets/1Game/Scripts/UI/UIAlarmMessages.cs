using System.Collections;
using UnityEngine;
using System.Timers;

public class UIAlarmMessages : MonoBehaviour
{
    [SerializeField] private CharacterTrashBagPicker _characterTrashBag;

    private AlarmPanel _alarmPanel;
    private  Timer _aTimer;

    private void Awake()
    {
        _aTimer = new Timer(3000);
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
        _alarmPanel.gameObject.SetActive(true);
    }

    private void OnSellTrashBag()
    {
        _alarmPanel.gameObject.SetActive(false);
    }
}
