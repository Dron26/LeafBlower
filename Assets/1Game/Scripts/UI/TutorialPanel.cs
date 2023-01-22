using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private Panel _hello;
        [SerializeField] private Panel _goWorkPlace;
        [SerializeField] private Panel _clearWorkPlace;
        [SerializeField] private Panel _reachedWorkPlace;
        [SerializeField] private Panel _takeTrashBag;
        [SerializeField] private Panel _reachedCart;
        [SerializeField] private Panel _endFuel;
        [SerializeField] private Panel _fullFuel;
        [SerializeField] private Panel _reachedFuelPlace;
        [SerializeField] private Panel _alarmPanel;
        [SerializeField] private Image _screenDim;
        
        
        private Color _colorScreen;
        private float _waitTime;
        private float _speedChange = 1.2f;


        public UnityAction OnSetScreenDim; 
        public UnityAction TutorialClose;
        public UnityAction TutorialCompleted;
        public UnityAction SetStartPosition;
        
        
        private void Awake()
        {
            _colorScreen = _screenDim.color;
            _screenDim.raycastTarget = true;
        }

        public void OnEndHello()
        {
            StartCoroutine(ChangeColorEnter(_goWorkPlace));
        }
        
        public void OnClearWorkPlace()
        {
            StartCoroutine(ChangeColorEnter(_clearWorkPlace));
        }

        public void OnReachedWorkPlace()
        {
            StartCoroutine(ChangeColorEnter(_reachedWorkPlace));
        }
        
        public void OnTakeTrashBag()
        {
            StartCoroutine(ChangeColorEnter(_takeTrashBag));
        }
        
        public void OnReachedCart()
        {
            StartCoroutine(ChangeColorEnter(_reachedCart));
        }
        
        public void PanelTurnOn(Panel activated)
        {
            StartCoroutine(ChangeColorEnter(_takeTrashBag));
        }
        
        public void PanelTurnOff(Panel activated)
        {
            StartCoroutine(ChangeColorExit(activated));
        }
        
        public void OnEndFuel()
        {
            StartCoroutine(ChangeColorEnter(_endFuel));
        }

        public void OnReachedFuelPlace()
        {
            StartCoroutine(ChangeColorEnter(_reachedFuelPlace));
        }
        
        public void OnFullFuel()
        {
            StartCoroutine(ChangeColorEnter(_fullFuel));
        }
        
        public IEnumerator ChangeColorExit(Panel activated)
        {
            activated.gameObject.SetActive(false);

            while (_colorScreen.a > 0)
            {
                _waitTime = Time.fixedDeltaTime;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a -= _waitTime * _speedChange;
                _screenDim.color = _colorScreen;
            }

            _screenDim.raycastTarget = false;
            yield break;
        }

        public IEnumerator ChangeColorEnter(Panel active)
        {
            GameObject panelExit = active.gameObject;
            _screenDim.raycastTarget = false;

            while (_colorScreen.a < 0.6)
            {
                _waitTime = Time.fixedDeltaTime * _speedChange;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a += _waitTime;
                _screenDim.color = _colorScreen;
            }

            if (_colorScreen.a >= 0.6)
            {
                _screenDim.raycastTarget = true;
                OnSetScreenDim?.Invoke();
            }
            
            panelExit.SetActive(true);
            yield break;
        }
        
        public IEnumerator WaitChangePanel()
        {
            float time = 100;
            
            while (time> 0)
            {
                _waitTime = Time.fixedDeltaTime;
                yield return new WaitForSeconds(_waitTime);
                time -=  _speedChange;
                
                if (time <= 0)
                {
                    StartCoroutine(ChangeColorEnter(_hello));
                }
            }
            
            yield break;
        }

        public void SetScreenDim()
        {
            OnSetScreenDim?.Invoke();
        }
        
        public void InitializePanel()
        {
            StartCoroutine(WaitChangePanel());
        }
        
        
        public void OnTutorialClose()
        {
            TutorialClose?.Invoke();
        }

        public void OnTutorialCompleted()
        {
            TutorialCompleted?.Invoke();
        }

   
    }
}
