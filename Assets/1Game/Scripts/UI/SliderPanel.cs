using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class SliderPanel : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Slider _slider;
        private float _maxValue;
        private float _minValue;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void SetParametr(int value)
        {
            _minValue = 0;
            _maxValue = value;
            _slider.maxValue = _maxValue;
            _slider.minValue = _minValue;
            _slider.value = _slider.minValue;
            _image.enabled = false;
        }

        public void OnUpLevel(int value)
        {
            _slider.value = Mathf.Clamp(value, _minValue, _maxValue);
            
            if (_slider.value>=1&_image.enabled == false)
            {
                _image.enabled = true;
            }
        }
    }
}