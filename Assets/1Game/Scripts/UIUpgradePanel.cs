using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIUpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradePlace _upgradePlace;
    
    private GameObject _gameObject;

    private UnityAction TapUpFuel;
    private UnityAction TapUpPower;
    private UnityAction TapUpCart;


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
