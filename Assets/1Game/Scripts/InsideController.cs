using UnityEngine;
using UnityEngine.Events;

public class InsideController : MonoBehaviour
{
    public UnityAction<bool> CharacterInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            CharacterInside?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            CharacterInside?.Invoke(false);
        }
    }

}
