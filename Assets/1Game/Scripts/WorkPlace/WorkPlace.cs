using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    public class WorkPlace : MonoBehaviour
    {
        [SerializeField] public List<WorkPlaceObject> _objects;

        public UnityAction<bool> EnterWorkPlace;

        private void Start()
        {
            ChangeStateWorkPlaceObjects(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                EnterWorkPlace?.Invoke(true);
                ChangeStateWorkPlaceObjects(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                EnterWorkPlace?.Invoke(false);
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