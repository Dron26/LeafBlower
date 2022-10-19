using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TrashBagMover : MonoBehaviour
{
    public Vector3 Point { get => _point; set { } }  
    private Vector3 _endPoint =new Vector3();
    private Vector3 _point;
    private bool _isPositionChange;
    private Vector3 _mainPoint;
    private Tween _startTween;

    private void Start()
    {
        _point = new Vector3();
        _mainPoint = new Vector3();
        MoveBeforTake();
    }

    private void MoveBeforTake()
    {
        int minPositionX = -4;
        int maxPositionZ = 5;
        int positionX = Random.Range(minPositionX, 0);
        int positionZ = Random.Range(0, maxPositionZ);
        float time = 1f;

         _startTween = transform.DOLocalMove(new Vector3(_endPoint.x+ positionX, _endPoint.y, _endPoint.z+ positionZ), time);
    }

    public void SetPaositionBeforTake(Vector3 vector3)
    {
        _endPoint = vector3;
    }

    public void SetPaositionAfterTake(Vector3 point, Vector3 mainPoint)
    {
        _startTween.Kill(true);
        _point = point;
        _mainPoint = mainPoint;
        StartCoroutine(MoveAfterTake());
    }

    private IEnumerator MoveAfterTake()
    {
        _isPositionChange = false;
        float time = 0.5f;
        Tween tween = transform.DOLocalMove(_mainPoint, time);

        while (_isPositionChange == false)
        {
            if (transform.localPosition == _mainPoint)
            {
                _isPositionChange = true;
                tween = transform.DOLocalMove(Point, time);
                transform.rotation = Quaternion.identity;
            }

            yield return null;
        }

        StopCoroutine(MoveAfterTake());
    }
}

