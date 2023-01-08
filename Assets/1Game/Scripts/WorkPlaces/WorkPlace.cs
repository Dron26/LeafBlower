using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class WorkPlace : MonoBehaviour
    {
        public UnityAction<bool> EnterWorkPlace;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                EnterWorkPlace?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                EnterWorkPlace?.Invoke(false);
            }
        }
    }
}