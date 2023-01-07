using System;
using _1Game.Scripts.Empty;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class District : MonoBehaviour
    {
        private Lock _lock;
        private GameObject _districkLock;
        private Button _button;
        private Slider _slider;
        private TMP_Text _countStars;
        private bool _isLocked;
        private int _number;
        private int _countStages;
        private int _maxStarCount;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _isLocked = true;
            _lock = GetComponentInChildren<Lock>();
            _slider=GetComponentInChildren<Slider>();
            _countStars = _slider.GetComponentInChildren<TMP_Text>();
            _countStars.text = Convert.ToString(_slider.value + "/" + _maxStarCount);
        }

        public void Initialize(int number,int countStages)
        {
            _countStages=countStages;
            _maxStarCount = _countStages;
            _slider.maxValue = _countStages;
            _number = number;

            if (_number == 0)
            {
                _isLocked = false;
            }

            SetLock(_isLocked);
        }

        public void SetLock(bool value)
        {
            _isLocked = value;
            _districkLock = _lock.gameObject;
            _districkLock.SetActive(_isLocked);
            _button.enabled = !_isLocked;

        }

        public void SetStars(int countStars)
        {
            _slider.value = Mathf.Clamp(countStars,0,_maxStarCount);
            SetTextSlider();
        }

        private void SetTextSlider()
        {
            _countStars.text = Convert.ToString(_slider.value + "/" + _maxStarCount);
        }
    }
}