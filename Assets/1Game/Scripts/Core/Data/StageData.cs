using System.Collections.Generic;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using UnityEngine;

namespace _1Game.Scripts.Core.SaveLoad.Data
{
    [RequireComponent(typeof(StageController))]
    public class StageData : MonoBehaviour
    {
        [SerializeField] private StarPanel _starPanel;
[SerializeField]private Character _character;
[SerializeField] private Cart _cart;
[SerializeField] private FuelPlace _fuelPlace;

private List<Dictionary<int, int>> _stagesStarGroup = new();
        [SerializeField] private TutorialPanel _tutorialPanel;
        private List< int> _countStages = new List<int>();
        
        public int CountStars => _countStars;
        public int NumberStage => _numberStage;
        public bool IsTutorialCompleted=> _isTutorialCompleted;

        private StageController _stageController;
        private ExitPanel _exitPanel;
        
        private int _countStars;
        private int _numberStage;
        private int _numberGroup;
        private bool _isTutorialCompleted;

        private void Awake()
        {
            _stageController = GetComponent<StageController>();
            _exitPanel = _stageController.GetComponent<StageController>()._exitPanel;
        }

        private void OnEnable()
        {
            _exitPanel.UpdateData += OnSetNextLevel;
            _tutorialPanel.TutorialClose += SetFinishTutorial;
            _tutorialPanel.TutorialCompleted += SetFinishTutorial;
            
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

        public List<Dictionary<int, int>> GetStagesStarGroup()
        {
            List<Dictionary<int, int>> _tempStagesStarGroup = new();
         
         for (int i = 0; i < _stagesStarGroup.Count; i++)
         {
             _tempStagesStarGroup.Add(_stagesStarGroup[i]);
         }
             
         return _tempStagesStarGroup;
        }

        public void  SetStagesStarGroup(List<Dictionary<int, int>> tempStagesStarGroup)
        {
            if(tempStagesStarGroup == null)
            {
                Debug.LogError("tempStagesStarGroup is null");
                return;
            }
            for (int i = 0; i < tempStagesStarGroup.Count; i++)
            {
                _stagesStarGroup.Add(tempStagesStarGroup[i]);
            }
        }
        
        private void OnDisable()
        {
            _exitPanel.UpdateData -= OnSetNextLevel;
        }

        public void SetFinishTutorial()
        {
            _tutorialPanel.TutorialClose -= SetFinishTutorial;
            _tutorialPanel.TutorialCompleted -= SetFinishTutorial;
            _isTutorialCompleted=true;
        }
    }
}