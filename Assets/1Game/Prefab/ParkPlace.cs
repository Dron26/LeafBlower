using UnityEngine;
using UnityEngine.Events;

public class ParkPlace : MonoBehaviour
{
    public UnityAction CartEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinishPointCart finishPoint))
        {
            CartEnter?.Invoke();
        }
    }
}
