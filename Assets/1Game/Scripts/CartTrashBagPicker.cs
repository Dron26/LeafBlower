using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core;

namespace Service
{
public class CartTrashBagPicker : MonoBehaviour
{
    private CharacterTrashBagPicker _trashBagPicker;
        private StageController _stageController;
    private int _quantityPickedTrashBag;
    private int _maxQuantityPickedTrashBag;
    private int _maxQuantityTrashBagInLevel;
    private Stack<TrashBag> _pickedTrashBags;
    private int _quantityInRow;
    private int _maxQuantityInRow;
    private float stepInRow;
    private TrashBagStorePoint _storePoint;
    private MainPointForTrashBag _mainPointForTrashBag;
    private TrashBagMover _trashBagMover;
    public UnityAction TakeTrashBag;
    public UnityAction SallAllTrashBag;
    public UnityAction TakeMaxQuantityTrashBag;
    public UnityAction<Vector3> SetPosition;
    public UnityAction BagReachedFinish;
    private WaitForSeconds _waitForSeconds;
    private Vector3 _mainPoint;
    private Vector3 _startPositionStorePoint;
    private Vector3 _removePositionStorePoint;
    private List<Vector3> _changePointStore;
    private Collider _collider;
    private bool _canTake;
    private Cart _cart;
    private float _time;

    private void Awake()
    {
        _cart = GetComponent<Cart>();
        _time = 1f;
    }
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
        _removePositionStorePoint = _storePoint.transform.localPosition;

    }


    private void OnEnable()
    {
            _stageController.SetCharacter += OnSetCharacter;
        _trashBagPicker.SallTrashBag += OnSallTrashBag;
        _cart.FinishMove += OnFinishMove;
    }

    private void OnDisable()
    {
            _stageController.SetCharacter -= OnSetCharacter;
            _trashBagPicker.SallTrashBag -= OnSallTrashBag;
        _cart.FinishMove -= OnFinishMove;
    }



    private void OnSallTrashBag(TrashBag trashBag)
    {
        if (_pickedTrashBags.Count != _maxQuantityPickedTrashBag& _canTake==true)
        {
            _quantityPickedTrashBag++;
            float waiteTime = 2f;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _pickedTrashBags.Push(trashBag);

            trashBag.transform.SetParent(transform, true);
            _collider = trashBag.GetComponent<Collider>();
            _collider.enabled = false;
            _trashBagMover = trashBag.GetComponent<TrashBagMover>();
            _trashBagMover.ReachedFinish += OnReachedFinish;
            ChangeWay();
            TakeTrashBag?.Invoke(); 
            
            if (_pickedTrashBags.Count == _maxQuantityPickedTrashBag)
            {
                _canTake = false;
                StartCoroutine(WaitFillingAll());              
            }
            
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
            _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y + stepUpLevel, _storePoint.transform.localPosition.z);
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

        _trashBagMover.SetSecondPosition(point, _mainPoint, _time);
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

    public void ClearCart()
    {
        foreach (TrashBag trashBag in _pickedTrashBags)
        {
            Destroy(trashBag.gameObject);
        }

        _pickedTrashBags.Clear();
        _quantityInRow = 0;
        _quantityPickedTrashBag = 0;
        _storePoint.transform.localPosition= _removePositionStorePoint;
        _startPositionStorePoint = _storePoint.transform.localPosition;
        SetPoint();
    }

    private void OnFinishMove()
    {
        _canTake = true;
    }

    private IEnumerator WaitFillingAll()
    {
        yield return _waitForSeconds;
        TakeMaxQuantityTrashBag?.Invoke();
        StopCoroutine(WaitFillingAll());
    }


    private void OnReachedFinish()
    {
        BagReachedFinish?.Invoke();
    }

        private void OnSetCharacter(Character character)
        {
            _trashBagPicker = character.gameObject.GetComponent<CharacterTrashBagPicker>();
        }
}
}