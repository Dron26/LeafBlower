using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashBagPicker : MonoBehaviour
{
    private TrashBagStorePoint _storePoint;
    private MainPointForTrashBag _mainPointForTrashBag;
    private TrashBagMover _trashBagMover;
    public UnityAction TakeTrashBag;
    public UnityAction TakeMaxQuantityTrashBag;
    public UnityAction <Vector3> SetPosition;

    private Stack<TrashBag> _pickedTrashBag;
    private Vector3 _mainPoint;
    private Vector3 _localPositionStorePoint;
    private List<Vector3> _changePointStore;

    private int _quantityAllTrashBag;
    private int _quantityPickedTrashBag;
    private int _maxQuantityPickedTrashBag;

    private int _quantityInRow;
    private int _quantityRow;
    private int _quantityLevel;
    private int _maxQuantityLevel;
    private bool _isPositionChange;



    private void Start()
    {
        _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        _mainPointForTrashBag = _storePoint.GetComponentInChildren<MainPointForTrashBag>();
        _pickedTrashBag = new Stack<TrashBag>();
        _changePointStore = new List<Vector3>();

        float stepInRow = 0.3f;
        float stepinSecondRow = -0.4f;

        _quantityLevel = 0;
        _quantityRow = 0;
        _maxQuantityLevel = 8;
        _isPositionChange = false;
        _maxQuantityPickedTrashBag = 8;

        _localPositionStorePoint = _storePoint.transform.localPosition;
        _mainPoint = _mainPointForTrashBag.transform.localPosition;

        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {
            _quantityPickedTrashBag++;

            if (_quantityPickedTrashBag <= _maxQuantityPickedTrashBag)
            {               
                _pickedTrashBag.Push(trashBag);
                trashBag.transform.SetParent(transform, true);
                _trashBagMover = trashBag.GetComponent<TrashBagMover>();
                ChangeWay(trashBag);
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

    private void ChangeWay(TrashBag trashBag)
    {
        int maxTrashBagInLevel = 4;
        Vector3 point = new Vector3();
        _quantityInRow++;

        if (_quantityInRow > maxTrashBagInLevel)
        {
            float stepUpLevel = 0.4f;
            _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y + stepUpLevel, _storePoint.transform.localPosition.z);
            _changePointStore.Clear();
            SetPoint();
            _quantityInRow = 1;
            point = _changePointStore[_quantityInRow - 1];
        }
        else
        {
            point = _changePointStore[_quantityInRow - 1];
        }

        _trashBagMover.SetPaositionAfterTake(point, _mainPoint);
    }

    //private IEnumerator Move(TrashBag trashBag)
    //{
    //    _isPositionChange = false;
    //    float time = 0.5f;
    //    Tween tween = trashBag.transform.DOLocalMove(_mainPoint, time);

    //    while (_isPositionChange == false)
    //    {
    //        if (trashBag.transform.localPosition == _mainPoint)
    //        {
    //            _isPositionChange = true;
    //            tween = trashBag.transform.DOLocalMove(trashBag.Point, time);
    //            trashBag.transform.rotation = Quaternion.identity;
    //        }

    //        yield return null;
    //    }

    //    trashBag.transform.SetParent(transform, false);
    //    StopCoroutine(Move(trashBag));
    //}

    private void SetPoint()
    {
        float stepInRow = 0.3f;
        float stepinSecondRow = -0.4f;

        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
    }
}

