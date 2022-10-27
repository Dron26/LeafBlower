using UnityEngine;
using UnityEngine.Events;

public class UpgradePlace : MonoBehaviour
{
    public UnityAction EnterPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            EnterPlace?.Invoke();
        }   
    }
}
