using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class Wallet : MonoBehaviour
    {
        public long Money { get => _amountResource; private set { } }
        public long MaxMoney { get => _maxAmountResource; private set { } }

        protected private long _amountResource= 0;
        protected private long _maxAmountResource= 100000000;

        public UnityAction<long> ChangeResource;

        public bool RemoveResource(long purchasePrice)
        
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

        public void AddResource(long price)
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

   

        public void LoadParametrs(long money, long maxMoney)
        {
            _amountResource = money;
            _maxAmountResource = maxMoney;
        }
        
        
        
    }
}
