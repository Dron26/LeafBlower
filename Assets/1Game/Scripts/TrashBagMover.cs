using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagMover : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private FinishPointForTrashBag _finishPoint;
    private Vector3 _endPoint;


    private void Start()
    {


        _endPoint = _finishPoint.transform.localPosition;
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _meshRenderer.enabled = true;

        Move();
    }

    private void OnStartFilling(bool isWork)
    {
        _meshRenderer.enabled = isWork;
    }

    private void Move()
    {
        int minPositionX = -4;
        int maxPositionZ = 6;
        int positionx = Random.Range(minPositionX, 0);
        int positionx = Random.Range(minPositionX, 0);

        Tween tween= transform.DOLocalMove(new Vector3(, , 4), 1);
    }
}
Vector3(4.5999999, -0.5, -3.70000005)
