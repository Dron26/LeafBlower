using UnityEngine;
using UI;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core
{
public class StageController : MonoBehaviour
{
        [SerializeField] private ChangerPanel _changerPanel;
        [SerializeField] private List<Stage> _smallTownStages;
        private List<GameObject> _smallTownStagesObject;
        private Character _character;

        public UnityAction<GameObject> SetStage;
        public UnityAction<Character> SetCharacter;

        private void Awake()
        {
            _character = GetComponentInChildren<Character>();
            SetCharacter?.Invoke(_character);
            _smallTownStagesObject = new List<GameObject>();

            for (int i = 0; i < _smallTownStages.Count; i++)
            {     
                _smallTownStagesObject.Add(_smallTownStages[i].gameObject);
            }

        }

        private void OnEnable()
        {
            _changerPanel.SelectSmallTownStage += OnSelectSmallTownStage;
        }

        private void OnDisable()
        {
            _changerPanel.SelectSmallTownStage -= OnSelectSmallTownStage;
        }

        private void OnSelectSmallTownStage(int number)
        {
            TurnOffStages();
            _smallTownStagesObject[number].SetActive(true);
            SetStage?.Invoke(_smallTownStagesObject[number]);
        }

        private void TurnOffStages()
        {
            foreach (GameObject stage in _smallTownStagesObject)
            {
                stage.SetActive(false);
            }
        }
    }
}
