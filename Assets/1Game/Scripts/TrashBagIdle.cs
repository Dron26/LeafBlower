using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagIdle : MonoBehaviour
{
     private GrabMashine _grabMashine;
    private MeshRenderer _meshRenderer; 

    private void Start()
    {
        _grabMashine = GetComponent<GrabMashine>();
    }

    private void OnEnable()
    {
        _grabMashine.StartFillng += OnStartFilling;
    }

    private void OnDisable()
    {
        _grabMashine.StartFillng -= OnStartFilling;
    }

    private void OnStartFilling()
    {

    }
}
