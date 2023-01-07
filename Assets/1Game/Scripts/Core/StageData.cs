using System.Collections.Generic;
using _1Game.Scripts.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    [RequireComponent(typeof(StageController))]
    public class StageData : MonoBehaviour
    {
        [SerializeField] private StarPanel _starPanel;

        private List<Dictionary<int, int>> _stagesStarGroup = new();

        private List< int> _countStages = new List<int>();
        public int CountStars => _countStars;
        public int NumberStage => _numberStage;

        private StageController _stageController;
        private ExitPanel _exitPanel;
        
        private int _countStars;
        private int _numberStage;
        private int _numberGroup;

        private void Awake()
        {
            _stageController = GetComponent<StageController>();
            _exitPanel = _stageController.GetComponent<StageController>()._exitPanel;
        }

        private void OnEnable()
        {
            _exitPanel.UpdateData += OnSetNextLevel;
        }

        private void Start()
        {
            SetGroupStage();
            InitializeStarGroup();
        }

        private void OnSetNextLevel()
        {
            _numberStage = _stageController.SelectNumberStage;
            _numberGroup = _stageController.SelectNumberGroup;
            _countStars = _starPanel.CountStars;
            SetStars(_numberStage, _numberGroup, _countStars);
        }

        private void SetGroupStage()
        {
            int count = _stageController.CountGroup;

            for (int i = 0; i < count; i++)
            {
                _stagesStarGroup.Add(new Dictionary<int, int>());
            }

            for (int i = 0; i < _stagesStarGroup.Count; i++)
            {
                _countStages.Add(_stageController.GetCountStages(i));
            }
        }
        
        private void InitializeStarGroup()
        {
            for (int i = 0; i < _stagesStarGroup.Count; i++)
            {
                int countStages = _countStages[i];
                
                for (int j = 0; j < countStages; j++)
                {
                    _stagesStarGroup[i][j] = 0;
                } 
            }
        }

        private void SetStars(int numberStage, int numberGroup, int countStars)
        {
            _stagesStarGroup[numberGroup][numberStage] = countStars;
        }

        public int GetStars(int numberStage, int numberGroup)
        {
            int countStars = _stagesStarGroup[numberGroup][numberStage];
            
            return countStars;
        }

        private void OnDisable()
        {
            _exitPanel.UpdateData -= OnSetNextLevel;
        }
    }
}