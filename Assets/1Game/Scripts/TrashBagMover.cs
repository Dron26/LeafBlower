using DG.Tweening;
using UnityEngine;

public class TrashBagMover : MonoBehaviour
{
    private Collider _collider;
    
    private Vector3 _endPoint =new Vector3();



    private void Start()
    {
        _collider = GetComponent<Collider>();
        Move();
    }

    private void Move()
    {
        int minPositionX = -4;
        int maxPositionZ = 5;
        int positionX = Random.Range(minPositionX, 0);
        int positionZ = Random.Range(0, maxPositionZ);
        float time = 1f;

        Tween tween= transform.DOLocalMove(new Vector3(_endPoint.x+ positionX, _endPoint.y, _endPoint.z+ positionZ), time);
    }

    public void Initialize(Vector3 vector3)
    {
        _endPoint = vector3;
    }


}
//+_endPoint   "(-60.13, 0.07, 12.71)" UnityEngine.Vector3
//+position    "(-61.14, 1.82, 12.03)" UnityEngine.Vector3

//+localPosition   "(0.49, 1.75, -1.12)"   UnityEngine.Vector3

