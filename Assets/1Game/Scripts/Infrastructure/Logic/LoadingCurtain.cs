using System;
using System.Collections;
using Infrastructure.BaseMonoCache.Code.MonoCache;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _1Game.Scripts.Infrastructure.Logic
{
    [DisallowMultipleComponent]
    public class LoadingCurtain : MonoCache
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _sliderText;
        public event Action FinishedShow;
   
        public CanvasGroup _canvasGroup;
    
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.interactable=false;
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
    
        public void Hide() => StartCoroutine(Delay());
    
    
        private IEnumerator Delay()
        {

            while (_slider.value < 100)
            {
                var delay = Random.Range(0.05f, 0.1f);
                _slider.value += 4.1f;
                _sliderText.text = ($"...{Math.Round(_slider.value, 1)}%");
                yield return new WaitForSeconds(delay);
            }

            DisableCurtain();
        }
    
        private void DisableCurtain()
        {
            FinishedShow?.Invoke(); 
            _slider.value = 0;
            _canvasGroup.alpha = 0;
        }
        // private IEnumerator Delay()
        // {
        //   while (_canvasGroup.alpha > 0)
        //   {
        //     _canvasGroup.alpha -= 0.03f;
        //     yield return new WaitForSeconds(0.03f);
        //   }
        //   
        //   gameObject.SetActive(false);
        // }
    }
}