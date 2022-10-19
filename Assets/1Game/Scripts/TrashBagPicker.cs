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
    private List<Vector3> _changePointStore;


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
        _localPositionMainPoint = _mainPoint.transform.localPosition;
        _wayPoint.Add(_localPositionMainPoint);
        _wayPoint.Add(_localPositionStorePoint);


        _changePointStore = new List<Vector3>();

        float stepInRow = 0.3f;
        float stepinSecondRow = -0.4f;

        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {
            if (_pickedTrashBag.Count <= _maxQuantityPickedTrashBag)
            {
                _pickedTrashBag.Push(trashBag);
                trashBag.transform.SetParent(transform, true);
                
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

        _quantityInRow++;


        

        if (_quantityInRow>maxTrashBagInLevel)
        {
            float stepUpLevel = 0.6f;
            _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y + stepUpLevel, _storePoint.transform.localPosition.z);
            _changePointStore.Clear();
            SetPoint();
            _quantityInRow =1;
            _wayPoint[1] = _changePointStore[_quantityInRow - 1];
        }
        else
        {
            _wayPoint[1] = _changePointStore[_quantityInRow - 1];
        }


        Vector3  point= new Vector3( );
        point = _wayPoint[1];
        Move(trashBag, point);
      
    }

    private void Move(TrashBag trashBag, Vector3 point)
    {
        isPositionChange = false;
        float time = 0.5f;


            Tween tween = trashBag.transform.DOLocalMove(_wayPoint[0], time);

            while (isPositionChange == false)
            {
                if (trashBag.transform.localPosition == _wayPoint[0])
                {
                    isPositionChange = true;
                     tween = trashBag.transform.DOLocalMove(point, time);
                    trashBag.transform.rotation = Quaternion.identity;
                }
            }

        trashBag.transform.SetParent(transform, false);

    }


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

