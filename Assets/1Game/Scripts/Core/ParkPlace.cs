using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
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