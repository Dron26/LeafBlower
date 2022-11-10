using UnityEngine;
using UnityEngine.Events;

namespace Service
{
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
}