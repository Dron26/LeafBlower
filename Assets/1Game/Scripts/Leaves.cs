using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent<AirZone>(out AirZone airZone))
        {
            Debug.Log("Contact");
        }
    }
}
