using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class InsideController : MonoBehaviour
    {
        public int _numberPlace;

        public UnityAction<GameObject> CharacterInside;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                CharacterInside?.Invoke(gameObject);
            }
        }
    }
}