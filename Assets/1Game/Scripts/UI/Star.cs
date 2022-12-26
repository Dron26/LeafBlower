using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    
    public class Star : MonoBehaviour
    {
        
        private Animator _animator;
        private int _hashNameCanChange = Animator.StringToHash("CanChange");
        private Image _image;
        
        private void Awake()
        {_animator=GetComponent<Animator>();
            _image=GetComponent<Image>();
        }

        public void Fade(float alfa,float time)
        {
            Tween tween = _image.DOFade(alfa, time);
        }
        
        public void ChangeSize()
        {
            _animator.SetBool(_hashNameCanChange,true);
        }
    }

}