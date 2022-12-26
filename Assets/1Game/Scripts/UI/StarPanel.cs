using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using Service;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{

    public class StarPanel : MonoBehaviour
    {
         private readonly List<Star> _stars= new List<Star>();
        [SerializeField] private StageController _stage;
        public int CountStars => _countStars;
        private int _numberWorkplace;
        private int _countStars;
        private int _countParticleSystems;
        float alfa ;
        float time;
        private void Awake()
        {
            foreach (Star star in transform.GetComponentsInChildren<Star>())
            {
                _stars.Add(star);
            }

            InitializeStars();
        }

        private void OnEnable()
        {
            _stage.CatchAllParticle += OnCatchAllParticle;
            _stage.SetStage+=OnSetStage;
        }


        
        private void OnCatchAllParticle()
        {
            alfa = 1f;
            _countParticleSystems--;
            
            if (_numberWorkplace==0|_numberWorkplace==1)
            {
                _stars[_numberWorkplace].Fade(alfa, time);
                _stars[_numberWorkplace].ChangeSize();
                _numberWorkplace++;
                _countStars++;
            }
            else if ( _countParticleSystems==0)
            {
                _stars[_numberWorkplace].Fade(alfa, time);
                _stars[_numberWorkplace].ChangeSize();
                _countStars++;
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
            InitializeStars();
        }

        private void InitializeStars()
        {
            alfa = 0.3f;;
            time=1f;
            
            foreach (Star star in _stars)
            {
                star.Fade(alfa, time);
            }
        }
    }
}
