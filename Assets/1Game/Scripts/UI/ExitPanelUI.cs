using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

public class ExitPanelUI : MonoBehaviour
{
    [SerializeField] private ExitPlace _exitPlaceOnStage;
   
    private Panel _panel;

    public UnityAction SetNextLevel;
    public UnityAction UpdateData;

    private void Awake()
    {
        _panel=GetComponentInChildren<Panel>();
    }

    private void Start()
    {
        _panel.gameObject.SetActive(false);
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
        _panel.gameObject.SetActive(isEnterPlace);
    }

    public void OnTapBattonYes()
    {
        UpdateData?.Invoke();
        SetNextLevel?.Invoke();
    }
}

