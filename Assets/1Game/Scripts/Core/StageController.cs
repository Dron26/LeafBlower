using System.Collections.Generic;
using _1Game.Scripts.Particle;
using _1Game.Scripts.UI;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private ChangerPanel _changerPanel;
        [SerializeField] private List<Stage> _firstStagesGroup;
        [SerializeField] private List<Stage> _secondStagesGroup;
        [SerializeField] public ExitPanel _exitPanel;

        public int CountParticleSystems => _particleSystems.Count;
        public int SelectNumberStage => _selectNumberStage;
        public int SelectNumberGroup => _selectNumberGroup;
        public int CountFirstStages => _firstStagesGroup.Count;
        public int CountSecondStages => _secondStagesGroup.Count;

        private List<ParticleSystemController> _particleSystems;
        private Character _character;
        private Stage _newStage;
        private int _selectNumberStage;
        private int _selectNumberGroup;
        private bool _isGetParticles;

        public UnityAction<GameObject> SetStage;
        public UnityAction<Character> SetCharacter;
        public UnityAction CatchAllParticle;


        private void Awake()
        {
            _character = GetComponentInChildren<Character>();
            SetCharacter?.Invoke(_character);
        }

        private void OnEnable()
        {
            _changerPanel.SelectSmallTownStage += OnSelectSmallTownStage;
            _exitPanel.SetNextLevel += OnSetNextLevel;
        }

        private void OnCatchAllParticle()
        {
            CatchAllParticle?.Invoke();
        }

        private void OnSelectSmallTownStage(int numberStage, int numberGroup)
        {
            CreateStage(numberStage, numberGroup);
        }

        private void CreateStage(int numberStage, int numberGroup)
        {
            List<Stage> stagesGroup = new List<Stage>();

            if (numberGroup == 0)
            {
                stagesGroup = _firstStagesGroup;
            }
            else if (numberGroup == 1)
            {
                stagesGroup = _secondStagesGroup;
            }

            _newStage = Instantiate(stagesGroup[numberStage], stagesGroup[numberStage].transform.position,
                Quaternion.identity);
            _newStage.transform.SetParent(transform);
            _newStage.SetExitPanel(_exitPanel);
            GetParticleSystems(_newStage);
            _selectNumberStage = numberStage;
            _selectNumberGroup = numberGroup;
            SetStage?.Invoke(_newStage.gameObject);
        }

        private void GetParticleSystems(Stage stage)
        {
            List<ParticleSystemController> _tempSystems = stage.GetParticleList();
            _particleSystems = new();

            for (int i = 0; i < _tempSystems.Count; i++)
            {
                _particleSystems.Add(_tempSystems[i]);
                _particleSystems[i].CatchAllParticle += OnCatchAllParticle;
            }

            _isGetParticles = true;
        }

        private void OnSetNextLevel()
        {
            Destroy(_newStage.gameObject, 1f);
        }


        private void OnDisable()
        {
            _changerPanel.SelectSmallTownStage -= OnSelectSmallTownStage;
            _exitPanel.SetNextLevel -= OnSetNextLevel;

            if (_isGetParticles == true)
            {
                for (int i = 0; i < _particleSystems.Count; i++)
                {
                    _particleSystems[i].CatchAllParticle -= OnCatchAllParticle;
                }
            }
        }
    }
}