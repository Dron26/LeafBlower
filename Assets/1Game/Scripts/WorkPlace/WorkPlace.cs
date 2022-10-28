using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkPlace : MonoBehaviour
{ 
    public UnityAction<bool> EnterWorkPlace;
    
    void Start()
    {
        
    }

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
