using System.Collections.Generic;
using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class LocationController : MonoBehaviour
    {
        [SerializeField] private  List<WorkPlaceObject> _objects;

        public UnityAction<GameObject> CharacterInside;
        
        private void Start()
        {
            ChangeStateWorkPlaceObjects(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                CharacterInside?.Invoke(gameObject);
                ChangeStateWorkPlaceObjects(true);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                ChangeStateWorkPlaceObjects(false);
            }
        }
        
        private void ChangeStateWorkPlaceObjects(bool isWork)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].gameObject.SetActive(isWork);
            }
        }
        


    }
}