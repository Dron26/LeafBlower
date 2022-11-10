using UnityEngine;
using UnityEngine.Events;

public class InsideController : MonoBehaviour
{
    public int _numberPlace;

    public UnityAction<GameObject> CharacterInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            CharacterInside?.Invoke(gameObject);
        }
    }
}
