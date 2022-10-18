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


    private Stack<TrashBag> _pickedTrashBag ;
    private List<Vector3> _wayPoint;
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
        _wayPoint = new List<Vector3>();
        _pickedTrashBag = new Stack<TrashBag>();
        _quantityLevel = 0;
        _quantityRow = 0;
        _maxQuantityLevel = 8;
        isPositionChange = false;
        _maxQuantityPickedTrashBag = 8;
        _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        _mainPoint = _storePoint.GetComponentInChildren<MainPointForTrashBag>();
        _localPositionStorePoint = _storePoint.transform.localPosition;
        _localPositionMainPoint = _mainPoint.transform.position;
        _wayPoint.Add(_localPositionMainPoint);
        _wayPoint.Add(_localPositionStorePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {
            if (_pickedTrashBag.Count <= _maxQuantityPickedTrashBag)
            {
                _pickedTrashBag.Push(trashBag);
                //trashBag.transform.SetParent(transform, false);
                StartCoroutine(ChangeWay(trashBag));
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

    private IEnumerator ChangeWay(TrashBag trashBag)
    {
        float stepInRow = 1.7f;
        float stepinSecondrow = -0.4f;
        float stepUpLevel = 1.17f;

        if (_quantityInRow == 1)
        {
            _quantityInRow++;
            _wayPoint[1] = new Vector3(_storePoint.transform.position.x + stepInRow, _storePoint.transform.position.y, _storePoint.transform.position.z);
        }
        else if (_quantityInRow == 1 & _quantityRow < 2)
        {

            _quantityInRow--;
            _wayPoint[1] = new Vector3(_storePoint.transform.position.x - stepInRow, _storePoint.transform.position.y, _storePoint.transform.position.z - stepinSecondrow);
        }
        else
        {
            _quantityRow = 0;
            _quantityInRow--;
            _quantityLevel++;
            _wayPoint[1] = new Vector3(_storePoint.transform.position.x - stepInRow, _storePoint.transform.position.y + stepUpLevel, _storePoint.transform.position.z + stepinSecondrow);
        }

        yield return null;

        StartCoroutine(Move(trashBag));
        StopCoroutine(ChangeWay(trashBag));
    }

    private IEnumerator Move(TrashBag trashBag)
    {
        float time = 1f;
        _quantityRow++;

        for (int i = 0; i < _wayPoint.Count; i++)
        {
            Tween tween = trashBag.transform.DOMove(_wayPoint[i], time);

            while (isPositionChange == false)
            {
                if (trashBag.transform.position == _wayPoint[0])
                {
                    isPositionChange = true;
                }

                yield return null;
            }
            trashBag.transform.SetParent(transform, false);
        }

        StopCoroutine(Move(trashBag));
    }
}

