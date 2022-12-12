using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;


namespace UI
{
    public class StagesGroup : MonoBehaviour
    {
        private List<GroupStages> _groups = new();
        private WorkPlacesSwitcher _switcher;
        
        private void Awake()
        {
            InitializeGroup();
        }

        
        
        private void Start()
        {
            SetActiveGroup();
            gameObject.SetActive(false);
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
    }
}
