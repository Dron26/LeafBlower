using UnityEngine;
using UnityEngine.Events;

public class UpgradePlace : MonoBehaviour
{
    public UnityAction EnterPlace;
    public UnityAction ExitPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            EnterPlace?.Invoke();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            ExitPlace?.Invoke();
        }
    }
}
