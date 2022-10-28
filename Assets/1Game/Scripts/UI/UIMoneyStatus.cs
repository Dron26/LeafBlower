using System;
using TMPro;
using UnityEngine;

public class UIMoneyStatus : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TMP_Text _amount;

    private void Awake()
    {
        _amount = gameObject.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        UpdateInfo(_wallet.Money);
    }

    private void OnEnable()
    {
        _wallet.ChangeResource += UpdateInfo;
    }

    private void OnDisable()
    {
        _wallet.ChangeResource -= UpdateInfo;
    }

    private void UpdateInfo(int amount)
    {
        _amount.text = Convert.ToString(amount);
    }
}
