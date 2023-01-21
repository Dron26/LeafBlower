using System;
using System.Collections.Generic;
using _1Game.Scripts.Core;
using _1Game.Scripts.Core.SaveLoad.Data;
using _1Game.Scripts.Empty;
using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class StagesPanel : MonoBehaviour
    {
        [SerializeField] private StageData _stageData;

        private ChangerPanel _changerPanel;
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
                group.Initialize(_changerPanel, _stageData, numberGroup);
                group.gameObject.SetActive(false);
                _groups.Add(group);
                numberGroup++;
            }
        }
    }
}