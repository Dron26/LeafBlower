using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TrashBagMover : MonoBehaviour
{
    public Vector3 Point { get => _firstPoint; set { } }
    private Vector3 _endPoint = new Vector3();
    private Vector3 _firstPoint;
    private Vector3 _transitPoint;
    private bool _isPositionChange;
    private Tween _startTween;
    private Collider _collider;
    float _time;
    private int numberFinishPoint;
    private int finishPoint;
    private WaitForSeconds _waitForSeconds;

    public UnityAction ReachedFinish;

    private void Start()
    {
        finishPoint = 2;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;

        _firstPoint = new Vector3();
        _transitPoint = new Vector3();
        MoveFirstPosition();     
    }

    public void SetFirstPosition(Vector3 vector3)
    {
        _endPoint = vector3;
        int minPositionX = -4;
        int maxPositionZ = 5;
        int positionX = Random.Range(minPositionX, 0);
        int positionZ = Random.Range(0, maxPositionZ);
        _endPoint = new Vector3(_endPoint.x + positionX, _endPoint.y, _endPoint.z + positionZ);
    }

    private void MoveFirstPosition()
    {
        float time = 0.5f;
        _startTween = transform.DOLocalMove(_endPoint, time);
        StartCoroutine(TurnOnCollider());
    }

    public void SetSecondPosition(Vector3 firstPoint, Vector3 transitPoint, float time)
    {
        _collider.enabled = false;
        _firstPoint = firstPoint;
        _transitPoint = transitPoint;
        _time = time;
        StartCoroutine(MoveSecondPosition());
    }

    private IEnumerator MoveSecondPosition()
    {
        numberFinishPoint++;
        _isPositionChange = false;
        Tween tween = transform.DOLocalMove(_transitPoint, _time);

        while (_isPositionChange == false)
        {
            if (transform.localPosition == _transitPoint)
            {
                _isPositionChange = true;
                tween = transform.DOLocalMove(Point, _time);

                if (numberFinishPoint==finishPoint)
                {
                    _waitForSeconds = new WaitForSeconds(_time);
                    StartCoroutine(ReachedFinishPoint());
                }
               
            }

            yield return null;
        }

        StopCoroutine(MoveSecondPosition());
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

    private IEnumerator ReachedFinishPoint()
    {
        yield return _waitForSeconds;
        ReachedFinish?.Invoke();
        StopCoroutine(ReachedFinishPoint());
    }
}




