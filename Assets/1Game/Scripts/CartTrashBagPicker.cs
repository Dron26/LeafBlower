using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CartTrashBagPicker : MonoBehaviour
{
   [SerializeField] private CharacterTrashBagPicker _trashBagPicker;
    private TrashBag _trashBag;
    private int _quantityPickedTrashBag;
    private int _maxQuantityPickedTrashBag;
    private int _maxQuantityTrashBagInLevel;
    private Stack<TrashBag> _pickedTrashBags;
    private int _quantityInRow;
    private int _maxQuantityInRow;
    private  float stepInRow;
    private TrashBagStorePoint _storePoint;
    private MainPointForTrashBag _mainPointForTrashBag;
    private TrashBagMover _trashBagMover;
    public UnityAction TakeTrashBag;
    public UnityAction SallAllTrashBag;
    public UnityAction TakeMaxQuantityTrashBag;
    public UnityAction<Vector3> SetPosition;

    private Vector3 _mainPoint;
    private Vector3 _startPositionStorePoint;
    private List<Vector3> _changePointStore;

    private void Start()
    {
        _maxQuantityInRow = 3;
        _maxQuantityPickedTrashBag = 8;
        _maxQuantityTrashBagInLevel = 8;
        _pickedTrashBags = new Stack<TrashBag>();
        _changePointStore = new List<Vector3>();
        _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        _mainPointForTrashBag = _storePoint.GetComponentInChildren<MainPointForTrashBag>();
        _startPositionStorePoint = _storePoint.transform.localPosition;
        _mainPoint = _mainPointForTrashBag.transform.localPosition;
        stepInRow = 0.6f;
        float stepinSecondRow = -0.4f;

        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
    }


    private void OnEnable()
    {
        _trashBagPicker.SallTrashBag += OnSallTrashBag;
    }

    private void OnDisable()
    {
        _trashBagPicker.SallTrashBag -= OnSallTrashBag;
    }

    private void OnSallTrashBag(TrashBag trashBag)
    {
        _trashBag = trashBag;

        if (_pickedTrashBags.Count <= _maxQuantityPickedTrashBag)
        {
            _quantityPickedTrashBag++;
        }

        if (_quantityPickedTrashBag <= _maxQuantityPickedTrashBag)
        {         
            trashBag.transform.SetParent(transform, true);
            _trashBagMover = trashBag.GetComponent<TrashBagMover>();

            ChangeWay();
            TakeTrashBag?.Invoke();
        }
        else
        {
            TakeMaxQuantityTrashBag?.Invoke();
        }
    }


    private void ChangeWay()
    {
        float stepUpLevel = 0.4f;
        float stepinSecondRow = -0.4f;
        Vector3 point = new Vector3();
        _quantityInRow++;

        if (_quantityInRow == _maxQuantityTrashBagInLevel)
        {
            _storePoint.transform.localPosition = _startPositionStorePoint;
            _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y + stepUpLevel, _storePoint.transform.localPosition.z );
            _startPositionStorePoint = _storePoint.transform.localPosition;
             _quantityInRow = 1;
        }
        else if (_quantityInRow == _maxQuantityInRow)
        {
            _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow);
            SetPoint();
            _quantityInRow = 1;
        }

            point = _changePointStore[_quantityInRow - 1];

        _trashBagMover.SetSecondPosition(point, _mainPoint);
    }

    private void SetPoint()
    {
        _changePointStore.Clear();
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
        _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
    }

    private void SetMaxTrashBagInLevel(int quantity)
    {
        _maxQuantityTrashBagInLevel = quantity;
    }

    private void UpPickUpQuantity(int quantity)
    {
        _maxQuantityPickedTrashBag = quantity;
    }
}