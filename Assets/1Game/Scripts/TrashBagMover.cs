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
    private Collider _collider;

    private void Start()
    {
        _point = new Vector3();
        _mainPoint = new Vector3();
        MoveBeforTake();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    private void MoveBeforTake()
    {
        int minPositionX = -4;
        int maxPositionZ = 5;
        int positionX = Random.Range(minPositionX, 0);
        int positionZ = Random.Range(0, maxPositionZ);
        float time = 1f;
        _endPoint = new Vector3(_endPoint.x + positionX, _endPoint.y, _endPoint.z + positionZ);
         _startTween = transform.DOLocalMove(_endPoint, time);
        StartCoroutine(TurnOnCollider()); 
    }

    public void SetPaositionBeforTake(Vector3 vector3)
    {
        _endPoint = vector3;
    }

    public void SetPaositionAfterTake(Vector3 point, Vector3 mainPoint)
    {
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


    private IEnumerator TurnOnCollider()
    {
        while (transform.position != _endPoint)
        {
            if (transform.localPosition == _endPoint)
            {
                _collider.enabled = true;
                StopCoroutine(TurnOnCollider());
            }
           
            yield return null;
        }

       
    }
}

