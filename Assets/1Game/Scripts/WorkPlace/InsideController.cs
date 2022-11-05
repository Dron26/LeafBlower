using UnityEngine;
using UnityEngine.Events;

public class InsideController : MonoBehaviour
{
    public int _numberPlace;

    public UnityAction<bool,int> CharacterInside;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            CharacterInside?.Invoke(true, _numberPlace);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            CharacterInside?.Invoke(false, _numberPlace);
        }
    }

}
