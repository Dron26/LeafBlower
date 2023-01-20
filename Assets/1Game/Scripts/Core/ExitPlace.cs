using _1Game.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class ExitPlace : MonoBehaviour
    {
        public UnityAction<bool> EnterPlace;
        [SerializeField] private CharacterPanel _characterPanel;
        
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
