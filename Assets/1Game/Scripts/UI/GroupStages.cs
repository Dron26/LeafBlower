using System.Collections.Generic;
using System.Linq;
using _1Game.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class GroupStages : MonoBehaviour
    {
        private ChangerPanel _changerPanel;
        private StageData _stageData;
        private List<StagePanel> _uIStages = new();
        
        private int _numberGroup;
        private int _countStars;

        public UnityAction<int> EndStage;

        private void Start()
        {
            InitializeStages();
        }

        private List<StagePanel> GetStages()
        {
            return _uIStages.ToList();
        }

        public void Initialize(ChangerPanel changerPanel, StageData stageData, int number)
        {
            _changerPanel = changerPanel;
            _stageData = stageData;
            _numberGroup = number;
        }

        private void InitializeStages()
        {
            foreach (StagePanel stage in transform.GetComponentsInChildren<StagePanel>())
            {
                _uIStages.Add(stage);
            }

            InitializeStage();
            SetStars();
        }

        private void InitializeStage()
        {
            for (int i = 0; i < _uIStages.Count; i++)
            {
                _uIStages[i].Initialize(i, _numberGroup);
            }
        }

        public void SetStars()
        {
            for (int i = 0; i < _uIStages.Count; i++)
            {
                _countStars = _stageData.GetStars(i, _numberGroup);
                _uIStages[i].GetComponentInChildren<StarGroup>().SetStars(_countStars);
                UnLockStage(i, _countStars);
            }
        }

        private void UnLockStage(int numberUIStage, int countStars)
        {
            int countAllStars = 3;

            if (_countStars == countAllStars & numberUIStage + 1 < _uIStages.Count)
            {
                _uIStages[numberUIStage + 1].SetLock(false);
            }
            else if (numberUIStage + 1 == _uIStages.Count)
            {
                EndStage?.Invoke(_numberGroup);
            }
        }
    }
}