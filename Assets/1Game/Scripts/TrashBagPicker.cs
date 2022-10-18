using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagPicker : MonoBehaviour
{
    []
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {

        }
    }



}

