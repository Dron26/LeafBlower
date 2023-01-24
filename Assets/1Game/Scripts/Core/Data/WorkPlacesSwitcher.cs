using System.Collections.Generic;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class WorkPlacesSwitcher : MonoBehaviour
    {
        public int WorkPlaceCount => _workPlaces.Count;
        private List<WorkPlace> _workPlaces = new List<WorkPlace>();

        [HideInInspector] private List<LocationController> _insideControllers = new List<LocationController>();

        public UnityAction<GameObject> ChangeWorkPlace;

        private void Start()
        {
            InitializeWorkPlaces();
        }

        private void OnDisable()
        {
            for (int i = 0; i < _workPlaces.Count; i++)
            {
                _insideControllers[i].CharacterInside -= OnCharacterInside;
            }
        }

        private void OnCharacterInside(GameObject insideController)
        {
            ChangeWorkPlace?.Invoke(insideController);
        }

        public List<WorkPlace> GetWorkPlaces()
        {
            List<WorkPlace> tempPlaces = new List<WorkPlace>();

            foreach (WorkPlace place in _workPlaces)
            {
                tempPlaces.Add(place);
            }

            return tempPlaces;
        }

        private void InitializeWorkPlaces()
        {
            foreach (WorkPlace place in transform.GetComponentsInChildren<WorkPlace>())
            {
                _workPlaces.Add(place);
            }

            for (int i = 0; i < _workPlaces.Count; i++)
            {
                LocationController locationController = new LocationController();

                _insideControllers.Add(locationController = _workPlaces[i].GetComponentInChildren<LocationController>());
                _insideControllers[i].CharacterInside += OnCharacterInside;
            }
        }
    }
}