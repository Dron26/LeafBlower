using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIParkStat : MonoBehaviour
{ 
    [SerializeField]private Cart _cart;

    private float _maxValue;
    private float _minValue;
    private Slider _slider;

    private void Start()
    {
        _minValue = 0;
        _slider = GetComponent<Slider>();
        _maxValue = _cart.Time;
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
        _slider.value = _minValue;

    }

    private void OnEnable()
    {
        _cart.StartMove += OnStartMove;
    }

    private void OnDisable()
    {
        _cart.StartMove -= OnStartMove;
    }

    private void OnStartMove()
    {
        _slider.DOValue(_maxValue, (_maxValue+ _maxValue), false);

    }

    public void Initialization(float time)
    {
        _maxValue = time;
    }
}