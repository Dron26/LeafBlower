using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
public class WorkPlace : MonoBehaviour
{ 
    public UnityAction<bool> EnterWorkPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemContainer container))
        {
            EnterWorkPlace?.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ItemContainer container))
        {
            EnterWorkPlace?.Invoke(false);
        }
    }

}
}
