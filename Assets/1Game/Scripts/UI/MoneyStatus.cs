using System;
using _1Game.Scripts.Core;
using TMPro;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    [RequireComponent(typeof(Animator))]
    public class MoneyStatus : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Store _store;

        private Animator _animator;
        private TMP_Text _amount;
        private int isWorkHashName;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            isWorkHashName = Animator.StringToHash("IsWork");
            _amount = gameObject.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            UpdateInfo(_wallet.Money);
        }

        private void OnEnable()
        {
            _wallet.ChangeResource += UpdateInfo;
            _store.EmptyWallet += OnEmptyWallet;
        }

        private void OnDisable()
        {
            _wallet.ChangeResource -= UpdateInfo;
            _store.EmptyWallet -= OnEmptyWallet;
        }

        private void UpdateInfo(long amount)
        {
            _amount.text = Convert.ToString(amount);
        }

        private void OnEmptyWallet()
        {
            _animator.SetBool(isWorkHashName, true);
        }
    }
}