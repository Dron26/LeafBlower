using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class ExitPlace : MonoBehaviour
    {
        public UnityAction<bool> EnterPlace;

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
