using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int Money { get => _amountResource; private set { } }
    public int MaxMoney { get => _maxAmountResource; private set { } }

    protected private int _amountResource;
    protected private int _maxAmountResource;
    public virtual event UnityAction<int> ChangeAmountResource;

    public UnityAction<int> ChangeResource;

    private void Start()
    {
        _maxAmountResource = 100000000;
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

    public void Initialization(int money, int maxMoney)
    {
        _amountResource = money;
        _maxAmountResource = maxMoney;
    }

    public void LoadParametr(int money, int maxMoney)
    {
        _amountResource = money;
        _maxAmountResource = maxMoney;
    }
}
