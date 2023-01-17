using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] private Image _screenDim;
        
        private Color _colorScreen;
        private float _waitTime;
        private float _speedChange = 1.2f;
        private void Awake()
        {
            _colorScreen = _screenDim.color;
            _screenDim.raycastTarget = true;
        }

        private void Start()
        {
            StartCoroutine(WaitChangePanel());
        }
        
        
        public void OnClearWorkPlace()
        {
            StartCoroutine(ChangeColorEnter(_clearWorkPlace));
        }
        
        public void OnReachedWorkPlace()
        {
            StartCoroutine(ChangeColorExit(_reachedWorkPlace));
        }
        
        public void OnTakeTrashBag()
        {
            StartCoroutine(ChangeColorExit(_takeTrashBag));
        }
        
        public void OnReachedCart()
        {
            StartCoroutine(ChangeColorExit(_reachedCart));
        }
        
        public void PanelTurnOn(Panel activated)
        {
            StartCoroutine(ChangeColorExit(_takeTrashBag));
        }
        
        public void PanelTurnOff(Panel active)
        {
            StartCoroutine(ChangeColorEnter(active));
        }
        
        
        public IEnumerator ChangeColorExit(Panel active)
        {
            GameObject panelExit = active.gameObject;
            _screenDim.raycastTarget = true;

            while (_colorScreen.a < 0.6)
            {
                _waitTime = Time.fixedDeltaTime * _speedChange;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a += _waitTime;
                _screenDim.color = _colorScreen;
            }

            panelExit.SetActive(true);
            yield break;
        }

        public IEnumerator ChangeColorEnter(Panel activated)
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
                    StartCoroutine(ChangeColorExit(_hello));
                }
            }
            
            yield break;
        }
    }
}
