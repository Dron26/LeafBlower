using UnityEngine;
using UI;
using System.Collections.Generic;
using UnityEngine.Events;
using Service;

namespace Core
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private ChangerPanel _changerPanel;
        [SerializeField] private List<Stage> _stages;
        private List<ParticleSystemController> _particleSystems;

        private Character _character;

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
        }

        private void OnDisable()
        {
            _changerPanel.SelectSmallTownStage -= OnSelectSmallTownStage;

            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].CatchAllParticle -= OnCatchAllParticle;
            }
        }

        private void OnCatchAllParticle()
        {
            CatchAllParticle?.Invoke();
        }

        private void OnSelectSmallTownStage(int number)
        {
            CreateStage(number);
        }

        private void CreateStage(int number)
        {
            Stage newStage = Instantiate(_stages[number], _stages[number].transform.position, Quaternion.identity);
            newStage.transform.SetParent(transform);
            SetStage?.Invoke(newStage.gameObject);
            GetParticleSystems(newStage);
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
        }
    }
}
