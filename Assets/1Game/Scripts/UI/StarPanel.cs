using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using Service;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Empty
{
    public class StarGroup : MonoBehaviour
    {
         private readonly List<Image> _images= new List<Image>();
        [SerializeField] private StageController _stage;
        public int CountStars => _countStars;
        private int _numberWorkplace;
        private int _countStars;
        private int _countParticleSystems => _stage.CountParticleSystems;
        private Image _image;
        
        private void Awake()
        {
            
            foreach (Image image in transform.GetComponentsInChildren<Image>())
            {
                _images.Add(image);
            }
        }

        private void OnEnable()
        {
            _stage.CatchAllParticle += OnCatchAllParticle;
        }

        private void Start()
        {
            float alfa = 0.3f;

            foreach (Image image in _images)
            {
                Tween tween = image.DOFade(alfa, 1f);
            }
        }
        
        private void OnCatchAllParticle()
        {
            float alfa = 1f;
            
            if (_numberWorkplace==0|_numberWorkplace==1)
            {
                Tween tween = _images[_numberWorkplace].DOFade(alfa, 1f);
                _numberWorkplace++;
                _countStars++;
            }
            else if ( _countParticleSystems==0)
            {
                Tween tween = _images[_numberWorkplace].DOFade(alfa, 1f);
                _countStars++;
            }
        }

        private void OnDisable()
        {
            _stage.CatchAllParticle -= OnCatchAllParticle;
        }
    }
}
