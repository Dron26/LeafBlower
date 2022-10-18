using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashBagPicker : MonoBehaviour
{

    private TrashBagStorePoint _storePoint;
    private MainPointForTrashBag _mainPoint;

    public UnityAction TakeTrashBag;
    public UnityAction TakeMaxQuantityTrashBag;

    private List<TrashBag> _pickedTrashBag;
    private Vector3[] _wayPoint;
    private TrashBag _trashBag;
    private Vector3 _localPositionTrashBag;
    private Vector3 _localPositionStorePoint;
    private Vector3 _localPositionMainPoint;
    private Vector3 vector3;


    private int _quantityAllTrashBag;
    private int _quantityPickedTrashBag;
    private int _maxQuantityPickedTrashBag;


    private int _quantityInRow;
    private int _quantityRow;
    private int _quantityLevel;
    private int _maxQuantityLevel;



    private bool isPositionChange;

    private void Start()
    {
        _quantityLevel = 0;
        _quantityRow = 0;
        _maxQuantityLevel = 8;
        isPositionChange = false;
        _maxQuantityPickedTrashBag = 8;
        _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        _mainPoint = GetComponentInChildren<MainPointForTrashBag>();
        _localPositionStorePoint = _storePoint.transform.localPosition;
        _localPositionMainPoint = _mainPoint.transform.localPosition;
        _wayPoint[0] = _localPositionMainPoint;
        _wayPoint[1] = _localPositionStorePoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {
            if (_pickedTrashBag.Count <= _maxQuantityPickedTrashBag)
            {
                _pickedTrashBag.Add(trashBag);
                _trashBag = trashBag;
                _localPositionTrashBag = trashBag.transform.localPosition;
                trashBag.transform.SetParent(transform, false);
                StartCoroutine(ChangeWay());
                TakeTrashBag?.Invoke();
            }
            else
            {
                TakeMaxQuantityTrashBag?.Invoke();
            }

        }
    }

    private void UpPickUpQuantity(int quantity)
    {
        _maxQuantityPickedTrashBag = quantity;
    }

    private IEnumerator ChangeWay()
    {
        float stepInRow = 1.7f;
        float stepinSecondrow = -0.4f;
        float stepUpLevel = 1.17f;

        if (_quantityInRow == 1)
        {
            _quantityInRow++;
            _wayPoint[1] = new Vector3(_localPositionStorePoint.x + stepInRow, _localPositionStorePoint.y, _localPositionStorePoint.z);
        }
        else if (_quantityInRow == 1 & _quantityRow < 2)
        {

            _quantityInRow--;
            _wayPoint[1] = new Vector3(_localPositionStorePoint.x - stepInRow, _localPositionStorePoint.y, _localPositionStorePoint.z - stepinSecondrow);
        }
        else
        {
            _quantityRow = 0;
            _quantityInRow--;
            _quantityLevel++;
            _wayPoint[1] = new Vector3(_localPositionStorePoint.x - stepInRow, _localPositionStorePoint.y + stepUpLevel, _localPositionStorePoint.z + stepinSecondrow);
        }

        yield return null;

        StartCoroutine(Move());
        StopCoroutine(ChangeWay());
    }

    private IEnumerator Move()
    {
        float time = 1f;
        _quantityRow++;

        for (int i = 0; i < _wayPoint.Length; i++)
        {
            Tween tween = _trashBag.transform.DOLocalMove(_wayPoint[0], time);

            while (isPositionChange == false)
            {
                if (_trashBag.transform.position == _wayPoint[0])
                {
                    isPositionChange = true;
                }

                yield return null;
            }
        }

        StopCoroutine(Move());
    }
}

