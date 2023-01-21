using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class CharacterTutorial : MonoBehaviour
    {
        public UnityAction ReachedPoint; 
        public UnityAction ReachedCart; 
        public UnityAction ReachedRefuel; 
    
        private bool _isTutorialCompleted;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cart _cart))
            {
                ReachedCart?.Invoke();
            }
            
            else if (other.TryGetComponent(out WorkPlaceTutorialPoint point))
            {
                ReachedPoint?.Invoke();
            }
            else if (other.TryGetComponent(out WorkplaceTutorialSecondPoint secondPoint))
            {
                secondPoint.gameObject.SetActive(false);
                ReachedPoint?.Invoke();
            }
            else if (other.TryGetComponent(out FuelTank fuelTank))
            {
                ReachedRefuel?.Invoke();
            }
        }
    }
}