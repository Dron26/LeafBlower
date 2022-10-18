using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashBagPicker : MonoBehaviour
{
   
    private TrashBagStorePoint _storePoint;
    private MainPointForTrashBag _mainPoint;
    public UnityAction TakeTrashBag;
    private List<TrashBag> _pickedTrashBag;
    private TrashBag _trashBag;
    private Vector3 _localPositionTrashBag;
    private Vector3 _localPositionStorePoint;
    private Vector3 _localPositionMainPoint;

    private int _quantityAllTrashBag;
    private int _quantityPickedTrashBag;
    private int _maxQuantityPickedTrashBag;


    private int _quantityInRow;
    private int _quantityRow;
    private int _quantityLevel;


    private void Start()
    {
        _maxQuantityPickedTrashBag = 8;
        _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        _mainPoint = GetComponentInChildren<MainPointForTrashBag>();
        _localPositionStorePoint = _storePoint.transform.localPosition;
        _localPositionMainPoint = _mainPoint.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrashBag>(out TrashBag trashBag))
        {
            if(_quantityPickedTrashBag<=_maxQuantityPickedTrashBag)
            {
                _quantityPickedTrashBag++;
                _trashBag = trashBag;
                _localPositionTrashBag = trashBag.transform.localPosition;
                trashBag.transform.SetParent(transform, false);
                StartCoroutine(PickTrashBag());
                TakeTrashBag?.Invoke();
            }

            
        }
    }


    private IEnumerator PickTrashBag()
    {
        while (_quantityPickedTrashBag>0)
        {

            yield return null;
        }


        StopCoroutine(PickTrashBag());
    }

    private void UpPickUpQuantity(int quantity)
    {
        _maxQuantityPickedTrashBag = quantity;
    }

    private IEnumerator MoveTrashBag()
    {

        while (_localPositionTrashBag != _localPositionStorePoint)
        {
            if (_quantityAllStepUp == 0 & isFilling == false)
            {
                EndFill();
            }
            yield return null;
        }

        StopCoroutine(PickTrashBag());
    }

}

