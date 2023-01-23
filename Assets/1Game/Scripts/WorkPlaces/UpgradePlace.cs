using _1Game.Scripts.Core;
using _1Game.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class UpgradePlace : MonoBehaviour
    {
        
        public UnityAction <bool> EnterPlace;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                EnterPlace?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                EnterPlace?.Invoke(false);
            }
        }
    }
}