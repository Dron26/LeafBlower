using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class Wallet : MonoBehaviour
    {
        public long Money { get => _amountResource; private set { } }
        public long MaxMoney { get => _maxAmountResource; private set { } }

        protected private long _amountResource;
        protected private long _maxAmountResource;

        public UnityAction<long> ChangeResource;

        private void Start()
        {
            _maxAmountResource = 100000000;
            _amountResource = 90;
        }

        public bool RemoveResource(int purchasePrice)
        {
            bool isPurchaseCompleted = false;

            if (purchasePrice <= _amountResource)
            {
                _amountResource -= purchasePrice;
                isPurchaseCompleted = true;

                ChangeResource?.Invoke(_amountResource);
            }

            return isPurchaseCompleted;
        }

        public void AddResource(int price)
        {
            if (price > 0)
            {
                _amountResource += price;

                if (_amountResource >= _maxAmountResource)
                {
                    _amountResource = _maxAmountResource;
                }

                ChangeResource?.Invoke(_amountResource);
            }
        }

        public void Initialization(long money, long maxMoney)
        {
            _amountResource = money;
            _maxAmountResource = maxMoney;
        }

        public void LoadParametr(long money, long maxMoney)
        {
            _amountResource = money;
            _maxAmountResource = maxMoney;
        }
    }
}
