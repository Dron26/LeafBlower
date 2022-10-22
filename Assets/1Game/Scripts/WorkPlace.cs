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

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AirZone airZone))
        {
            EnterWorkPlace?.Invoke(true);
        }
    }
    private void OnTriggerExite(Collider other)
    {
        if (other.TryGetComponent(out WorkPlace workPlace))
        {
            EnterWorkPlace?.Invoke(false);
        }
    }

}
