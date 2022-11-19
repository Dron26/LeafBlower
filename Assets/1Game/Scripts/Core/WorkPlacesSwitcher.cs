using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Service;

namespace Core
{
    public class WorkPlacesSwitcher : MonoBehaviour
    {
        private List<WorkPlace> _workPlaces=new List<WorkPlace>();
        [HideInInspector] private List<InsideController> _insideControllers = new List<InsideController>();

        public UnityAction<GameObject> ChangeWorkPlace;
        private int firstStart = 1;

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
                InsideController insideController = new InsideController();
                Vector3 stayPoint = new Vector3();

                _insideControllers.Add(insideController = _workPlaces[i].GetComponentInChildren<InsideController>());
                _insideControllers[i].CharacterInside += OnCharacterInside;
            }
        }

        
    }
}
