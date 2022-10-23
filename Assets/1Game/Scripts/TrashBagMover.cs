using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TrashBagMover : MonoBehaviour
{
    public Vector3 Point { get => _firstPoint; set { } }
    private Vector3 _endPoint = new Vector3();
    private Vector3 _firstPoint;
    private bool _isPositionChange;
    private Vector3 _mainPoint;
    private Tween _startTween;
    private Collider _collider;
    private ParticleSystem _particleSystem;
    float _time;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _particleSystem =GetComponent<ParticleSystem>();
        _firstPoint = new Vector3();
        _mainPoint = new Vector3();
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
        float time = 1f;
        _startTween = transform.DOLocalMove(_endPoint, time);
        StartCoroutine(TurnOnCollider());
    }

    public void SetSecondPosition(Vector3 firstPoint, Vector3 mainPoint,float time)
    {
        _firstPoint = firstPoint;
        _mainPoint = mainPoint;
        time = time;
        StartCoroutine(MoveSecondPosition());
    }

    private IEnumerator MoveSecondPosition()
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
            }
            else if (transform.localPosition == Point)
            {
                _particleSystem.Play();
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


}

