using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagIdle : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField ]private GrabMashine _grabMashine;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    private void OnEnable()
    {
        _grabMashine.StartFillng += OnStartFilling;
    }

    private void OnDisable()
    {
        _grabMashine.StartFillng -= OnStartFilling;
    }

    private void OnStartFilling(bool isWork)
    {
        _meshRenderer.enabled = isWork;
    }
}
