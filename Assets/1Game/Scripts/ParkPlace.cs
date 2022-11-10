using UnityEngine;
using UnityEngine.Events;
using Core;

namespace Service
{
public class ParkPlace : MonoBehaviour
{
    public UnityAction CartEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cart cart))
        {
            CartEnter?.Invoke();
        }
    }
}
}