using System.Collections.Generic;
using _1Game.Scripts.Core;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class StarPanel : MonoBehaviour
    {
        [SerializeField] private StageController _stage;
        public int CountStars => _countStars;
        public int CountClearWorkPlaces=>_countClearWorkPlaces;
        private readonly List<Star> _stars = new();
        
        private int _numberWorkplace;
        private int _countStars;
        private int _countClearWorkPlaces;
        private float _alfa;
        private float _time;

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
            _stage.SetStage += OnSetStage;
        }

        private void OnCatchAllParticle()
        {
            _alfa = 1f;
            _countClearWorkPlaces--;

            if (_numberWorkplace == 0 | _numberWorkplace == 1)
            {
                _stars[_numberWorkplace].Fade(_alfa, _time);
                _stars[_numberWorkplace].ChangeSize();
                _numberWorkplace++;
                _countStars++;
            }
            else if (_countClearWorkPlaces == 0)
            {
                _stars[_numberWorkplace].Fade(_alfa, _time);
                _stars[_numberWorkplace].ChangeSize();
                _countStars++;
            }
        }
        
        private void OnDisable()
        {
            _stage.CatchAllParticle -= OnCatchAllParticle;
            _stage.SetStage += OnSetStage;
        }

        private void OnSetStage(GameObject stage)
        {
            _countStars = 0;
            _numberWorkplace = 0;
            _countClearWorkPlaces = _stage.CountParticleSystems;
            InitializeStars();
        }

        private void InitializeStars()
        {
            _alfa = 0.3f;
            ;
            _time = 1f;

            foreach (Star star in _stars)
            {
                star.Fade(_alfa, _time);
            }
        }
    }
}