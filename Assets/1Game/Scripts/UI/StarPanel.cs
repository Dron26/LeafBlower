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
    public class StarPanel : MonoBehaviour
    {
         private readonly List<Image> _images= new List<Image>();
        [SerializeField] private StageController _stage;
        public int CountStars => _countStars;
        private int _numberWorkplace;
        private int _countStars;
        private int _countParticleSystems;
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
            _stage.SetStage+=OnSetStage;
        }

        private void Start()
        {
            Fade();
        }
        
        private void OnCatchAllParticle()
        {
            float alfa = 1f;
            
            _countParticleSystems--;
            
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

        private void Fade()
        {
            float alfa = 0.3f;

            foreach (Image image in _images)
            {
                Tween tween = image.DOFade(alfa, 1f);
            }
        }
        private void OnDisable()
        {
            _stage.CatchAllParticle -= OnCatchAllParticle;
            _stage.SetStage+=OnSetStage;
        }
        private void OnSetStage(GameObject stage)
        {
            _countStars = 0;
            _numberWorkplace = 0;
            _countParticleSystems = _stage.CountParticleSystems;
            Fade();
        }
    }
}
