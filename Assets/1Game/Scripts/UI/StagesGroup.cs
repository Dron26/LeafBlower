using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class StagesGroup : MonoBehaviour
    {
        private List<GroupStages> _groups = new();
        [SerializeField] private List<Image> _images;
        [SerializeField] private WorkPlacesSwitcher _switcher;
        [SerializeField] private StageController _stage;
        
        private int _numberWorkplace;
        private int _countWorkplace;
        
        private void Awake()
        {
            InitializeGroup();
            _numberWorkplace = 0;
        }

        private void Start()
        {
            _countWorkplace = _switcher.WorkPlaceCount;
            float alfa = 0.5f;
            
            SetActiveGroup();
            gameObject.SetActive(false);
            

            for (int i = 0; i < _images.Count; i++)
            {
                Tween tween = _images[i].DOFade(alfa, 1f);
            }
        }

        public void SetGroup(int numberGroup)
        {
            _groups[numberGroup].gameObject.SetActive(true);
        }

        private void InitializeGroup()
        {
            foreach (GroupStages group in transform.GetComponentsInChildren<GroupStages>())
            {
                _groups.Add(group);
            }
        }

        private void SetActiveGroup()
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                _groups[i].gameObject.SetActive(false);
            }
        }
        
        private void SetStageStars()
        {
            
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
            
            if (_numberWorkplace==0|_numberWorkplace==1)
            {
                Tween tween = _images[_numberWorkplace].DOFade(alfa, 1f);
                _numberWorkplace++;
            }
            else if (_numberWorkplace == _countWorkplace)
            {
                Tween tween = _images[_numberWorkplace].DOFade(alfa, 1f);
            }
        }
    }
}
