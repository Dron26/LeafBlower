using System.Collections.Generic;
using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class SmallTowm : MonoBehaviour
    {
        [SerializeField] private List<GroupStages> _groupsStages;
        [SerializeField] private GroupDistrict _groupDistrict;

        private List<District> _districts = new();
        private int _countDistrictStars;
        [SerializeField] private StageData _stageData;
        
        private void Awake()
        {
            foreach (District district in _groupDistrict.GetComponentsInChildren<District>())
            {
                _districts.Add(district);
            }
        }

        private void OnEnable()
        {
            foreach (GroupStages groupStages in _groupsStages)
            {
                groupStages.EndStage += OnEndStage;
            }
        }

        private void Start()
        {
            InitializeDistrict();
            gameObject.SetActive(false);
        }

        private void InitializeDistrict()
        {
            for (int i = 0; i < _districts.Count; i++)
            {
                _districts[i].Initialize(i,_groupsStages[i].CountStages);
            }
        }

        private void UnLockStage(int numberGroup)
        {
            if (numberGroup < _districts.Count)
            {
                _districts[numberGroup].SetLock(false);
            }
        }

        private void OnEndStage(int numberGroup)
        {
            UnLockStage(numberGroup + 1);
        }

        public void SetStars()
        {
            int maxCoumtStarsInStage = 3;
            
            for (int i = 0; i < _groupsStages.Count; i++)
            {
                _countDistrictStars = 0;
                
                for (int j = 0; j < _groupsStages[i].CountStages; j++)
                {
                    int _countStars = _stageData.GetStars(j, i);
                    
                    if (_countStars==maxCoumtStarsInStage)
                    {
                        _countDistrictStars++;
                    }
                }
                
                _districts[i].SetStars(_countDistrictStars);
            }
        }
        
    }
}