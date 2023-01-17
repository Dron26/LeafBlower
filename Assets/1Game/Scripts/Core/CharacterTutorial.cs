using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class CharacterTutorial : MonoBehaviour
    {
        public UnityAction ReachedPoint; 
        public UnityAction ReachedCart; 
    
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cart _cart))
            {
                ReachedCart?.Invoke();
            }
            
            else if (other.TryGetComponent(out WorkPlaceTutorialPoint point))
            {
                point.gameObject.SetActive(false);
                ReachedPoint?.Invoke();
            }
            else if (other.TryGetComponent(out WorkplaceTutorialSecondPoint secondPoint))
            {
                secondPoint.gameObject.SetActive(false);
                ReachedPoint?.Invoke();
            }
            
        }
    }
}