using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using Empty;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;


namespace UI
{
    public class StagesPanel : MonoBehaviour
    {
        private ChangerPanel _changerPanel;
        [SerializeField] private StageData _stageData;
        
        
        private List<GroupStages> _groups = new();
        private GroupContainer _container;
        
        private void Awake()
        {
            _changerPanel = GetComponentInParent<ChangerPanel>();
            _container = GetComponentInChildren<GroupContainer>();
            InitializeGroup();
        }
        
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void SetGroup(int numberGroup)
        {
            _groups[numberGroup].gameObject.SetActive(true);
        }

        private void InitializeGroup()
        {
            int numberGroup = 0;
            foreach (GroupStages group in _container.GetComponentsInChildren<GroupStages>())
            {
                group.Initialize(_changerPanel,_stageData,numberGroup);
                group.gameObject.SetActive(false);
                _groups.Add(group);
                numberGroup++;
            }
        }

    }
}
