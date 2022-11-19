using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Service
{
    public class ChangerColorWorkPlace : MonoBehaviour
    {        
        [SerializeField] private List<Image> _images;
        [SerializeField] private StageController _stage;

        private int _numberWorkplace;

        private void Awake()
        {
            _numberWorkplace = 0;
        }

        private void Start()
        {
            float alfa = 0.5f;

            for (int i = 0; i < _images.Count; i++)
            {
                Tween tween = _images[i].DOFade(alfa, 1f);
            }
        }

        private void OnEnable()
        {
            _stage.CatchAllParticle += OnCatchAllParticle;
        }

        private void OnDisable()
        {
            _stage.CatchAllParticle -= OnCatchAllParticle;
        }

        private void OnCatchAllParticle()
        {
            float alfa = 1f;

            Tween tween = _images[_numberWorkplace].DOFade(alfa, 1f);

            _numberWorkplace++;
        }
    }
}

