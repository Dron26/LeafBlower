using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitPlace : MonoBehaviour
{
    public UnityAction<bool> EnterPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            EnterPlace?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            EnterPlace?.Invoke(false);
        }
    }
}
