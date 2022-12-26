using System.IO;
using UnityEngine;

namespace Core
{
    public class GameInitializator:MonoBehaviour
    { 
        [SerializeField] private Wallet _wallet;
        public bool IsFirstStart { get => _isFirstStart;set { } }

        private bool _isFirstStart;
        private Load _load;
        private Save _save;

        private void Awake()
        {
            if (_isFirstStart == false)
            {
                Debug.Log("StartSecondGame");
                _isFirstStart = true;
            }
            else
            {
                Initialize();
                SetFirstStart();
            }

        }

        private void InitializeWallet()
        {
            int amountMoney = 0;
            int maxAmountMoney = 100000000;
            _wallet.Initialization(amountMoney, maxAmountMoney);
        }

        private void Initialize()
        {
            InitializeWallet();
        }

        private void SetFirstStart()
        {
        }
    }
    
}