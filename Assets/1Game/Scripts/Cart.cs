using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    private FinishPointCart _finishPointCart;
    private Scene _scene;
    private WorkPlacesSwitcher _workPlacesSwitcher;

    public float Time { get => _time; set { } }

    private Vector3 _currentPoint;
    private Vector3 point;
    private Tween _tween;
    private Collider _collider;
    private CartTrashBagPicker _cartTrashBagPicker;
    private WaitForSeconds _waitForSeconds;
    private float _time;
    [SerializeField] private Wallet _wallet;
    private bool isMoveFinish;
    public UnityAction StartMove;
    public UnityAction FinishMove;

    private void Awake()
    {
        isMoveFinish = false;
        _workPlacesSwitcher = GetComponentInParent<WorkPlacesSwitcher>();
        _scene = GetComponentInParent<Scene>();
        _finishPointCart = _scene.GetComponentInChildren<FinishPointCart>();
        float waiteTime = 2f;
        _waitForSeconds = new WaitForSeconds(waiteTime);
        _cartTrashBagPicker = GetComponent<CartTrashBagPicker>();
        _time = 5f;
    }

    private void OnEnable()
    {
        _cartTrashBagPicker.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
        _cartTrashBagPicker.BagReachedFinish += OnTrashBagReachedFinish;
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }

    private void Start()
    {
        _currentPoint = transform.position;
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinishPointCart finish))
        {
            _cartTrashBagPicker.ClearCart();
            SetSecondPosition();           
        }
        else if (other.TryGetComponent(out ParkPlace parkPlace))
        {
            FinishMove?.Invoke();
        }
    }


    private void OnTakeMaxQuantityTrashBag()
    {
        point = _finishPointCart.transform.position;
        StartCoroutine(WaitBeforMove());
    }

    private void MovePosition()
    {
        _tween = transform.DOMove(point, _time);
    }

    public void SetSecondPosition()
    {
        isMoveFinish = false;
        _tween.Kill();
        point = _currentPoint;
        MovePosition();
    }

    private IEnumerator WaitBeforMove()
    {
        isMoveFinish = true;
        yield return _waitForSeconds;
        StartMove?.Invoke();
        MovePosition();
        StartCoroutine(TempOffCollider());
        StopCoroutine(WaitBeforMove());
    }

    private IEnumerator TempOffCollider()
    {
        _collider.enabled = false;
        yield return _waitForSeconds;
        _collider.enabled = true;
        StopCoroutine(TempOffCollider());
    }

    private void OnTrashBagReachedFinish()
    {
        int price = 10;

        _wallet.AddResource(price);
    }

    private void OnChangeWorkPlace(Vector3 currentPoint)
    {

        StartCoroutine(WaitMoveFinish(currentPoint));
    }
    private void OnDisable()
    {
        _cartTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;
        _cartTrashBagPicker.BagReachedFinish += OnTrashBagReachedFinish;
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }

    private IEnumerator WaitMoveFinish(Vector3 currentPoint)
    {
        if (isMoveFinish==true)
        {
            _tween.Kill();
            point = _finishPointCart.transform.position; 
            MovePosition();
        }

        while (isMoveFinish == true)
        {
            yield return null;
        }

        _currentPoint = currentPoint;
        SetSecondPosition();
    }
}
