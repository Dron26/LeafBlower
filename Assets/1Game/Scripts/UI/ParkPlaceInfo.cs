using System.Collections;
using _1Game.Scripts.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class ParkPlaceInfo : MonoBehaviour
    {
        [SerializeField] private Cart _cart;

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
            StartCoroutine(WaiteEndMove());
            _slider.DOValue(_maxValue, (_maxValue + _maxValue), false);
        }

        public void Initialize(float time)
        {
            _maxValue = time;
        }

        private IEnumerator WaiteEndMove()
        {
            while (_slider.value < _maxValue)
            {
                yield return null;
            }

            _slider.value = 0;
            yield break;
        }
    }
}