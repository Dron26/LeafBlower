using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SmallTowm : MonoBehaviour
    {
        [SerializeField]private GroupContainer _groupContainer;
        private ChangerPanel _changerPanel;
        private List<District> _districts = new();
        private List<GroupStages> _groupsStages = new List<GroupStages>();
        private int _countStars;

        private void Awake()
        {
            _changerPanel=GetComponentInParent<ChangerPanel>();

            foreach (GroupStages groupStages in _groupContainer.GetComponentsInChildren<GroupStages>())
            {
                _groupsStages.Add(groupStages);
            }
            
        }

        private void OnEnable()
        {
            foreach (GroupStages groupStages in _groupsStages)
            {
                groupStages.EndStage+=OnEndStage;
            }
        }

        private void Start()
        {
            foreach (District district in transform.GetComponentsInChildren<District>())
            {
                _districts.Add(district);
            }

            InitializeDistrict();

            gameObject.SetActive(false);
        }
        
        private void InitializeDistrict()
        {
            for (int i = 0; i < _districts.Count; i++)
            {
                _districts[i].Initialize(i);
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
    }
}