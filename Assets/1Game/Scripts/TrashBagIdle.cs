using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagIdle : MonoBehaviour
{
    private MeshRenderer _meshRenderer; 

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    private void OnStartFilling(bool isWork)
    {
        _meshRenderer.enabled = isWork;
    }
}
