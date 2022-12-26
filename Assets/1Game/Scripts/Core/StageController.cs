using UnityEngine;
using UI;
using System.Collections.Generic;
using UnityEngine.Events;
using Service;
using UnityEngine.Serialization;

namespace Core
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private ChangerPanel _changerPanel;
        [SerializeField] private List<Stage> _FirstStagesGroup;
        [SerializeField] private List<Stage> _SecondStagesGroup;
        
        [SerializeField] public ExitPanelUI _exitPanel;

        public int CountParticleSystems => _particleSystems.Count;
        public int SelectNumberStage => _selectNumberStage;
        public int SelectNumberGroup => _selectNumberGroup;
        public int CountFirstStages=> _FirstStagesGroup.Count;
        public int CountSecondStages => _SecondStagesGroup.Count;
        
        private List<ParticleSystemController> _particleSystems;
        private Character _character;

        private Stage newStage;

        public UnityAction<GameObject> SetStage;
        public UnityAction<Character> SetCharacter;
        public UnityAction CatchAllParticle;
        
        
        private int _selectNumberStage;
        private int _selectNumberGroup;

        private bool isGetParticles;
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

        private void OnSelectSmallTownStage(int numberStage,int numberGroup )
        {

            CreateStage(numberStage,numberGroup);
        }

        private void CreateStage(int numberStage,int numberGroup)
        {
            List<Stage> stagesGroup= new List<Stage>();

            if (numberGroup == 0)
            {
                stagesGroup = _FirstStagesGroup;
            }
            else if (numberGroup == 1)
            {
                stagesGroup = _SecondStagesGroup;
            }

            newStage = Instantiate(stagesGroup[numberStage], stagesGroup[numberStage].transform.position, Quaternion.identity);
            newStage.transform.SetParent(transform);
            newStage.SetExitPanel(_exitPanel);
            GetParticleSystems(newStage);
            _selectNumberStage = numberStage; 
            _selectNumberGroup = numberGroup;
            SetStage?.Invoke(newStage.gameObject);
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

            isGetParticles = true;
        }

        private void OnSetNextLevel()
        {
            Destroy(newStage.gameObject,1f);
        }
        

        private void OnDisable()
        {
            _changerPanel.SelectSmallTownStage -= OnSelectSmallTownStage;

            
            _exitPanel.SetNextLevel -= OnSetNextLevel;

            if (isGetParticles==true)
            {
                for (int i = 0; i < _particleSystems.Count; i++)
                {
                    _particleSystems[i].CatchAllParticle -= OnCatchAllParticle;
                }
            }      
        }
    }
}
