using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitePanel : MonoBehaviour
{
    [SerializeField] private ExitPlace _exitPlaceOnStage;
   
    private ExitePanel _exitePanel;

    private UnityAction SetNextLevel;

    private void Awake()
    {
        _exitePanel.GetComponent<ExitePanel>();
    }

    private void Start()
    {
        _exitePanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _exitPlaceOnStage.EnterPlace += OnEnterPlace;

    }

    private void OnDisable()
    {
        _exitPlaceOnStage.EnterPlace -= OnEnterPlace;
    }

    private void OnEnterPlace(bool isEnterPlace)
    {
        _exitePanel.gameObject.SetActive(isEnterPlace);
    }

    public void OnTapBattonYes()
    {
        SetNextLevel?.Invoke();
    }
}
