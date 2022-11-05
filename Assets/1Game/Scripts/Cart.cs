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
    private Vector3 _finishPoint;
    private Tween _tween;
    private Collider _collider;
    private CartTrashBagPicker _cartTrashBagPicker;
    private WaitForSeconds _waitForSeconds;
    private float _time;
    [SerializeField]private Wallet _wallet;

    public UnityAction StartMove;
    public UnityAction FinishMove;

    private void Awake()
    {
        _workPlacesSwitcher= GetComponentInParent<WorkPlacesSwitcher>();
        _scene = GetComponentInParent<Scene>();
        _finishPointCart = _scene.GetComponentInChildren<FinishPointCart>();
        float waiteTime = 2f;
        _waitForSeconds = new WaitForSeconds(waiteTime);
        _cartTrashBagPicker = GetComponent<CartTrashBagPicker>();
        _time = 5f;
    }


    private void Start()
    {
        _finishPoint = _finishPointCart.transform.position;
        _currentPoint = transform.position;
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _cartTrashBagPicker.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
        _cartTrashBagPicker.BagReachedFinish += OnTrashBagReachedFinish;
        _workPlacesSwitcher.ChangeWorkPlace += OnChangeWorkPlace;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FinishPointCart finish))
        {
            SetSecondPosition();
            _cartTrashBagPicker.ClearCart();
        }
        else if (other.TryGetComponent(out ParkPlace parkPlace))
        {
            FinishMove?.Invoke();
        }
    }

    private void OnDisable()
    {
        _cartTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;
        _cartTrashBagPicker.BagReachedFinish += OnTrashBagReachedFinish;
        _workPlacesSwitcher.ChangeWorkPlace += OnChangeWorkPlace;
    }

    private void OnTakeMaxQuantityTrashBag()
    {
        StartCoroutine(Wait());
    }

    private void MovePosition(Vector3 point)
    {
        _tween = transform.DOMove(point, _time);
    }

    public void SetSecondPosition()
    {
        _tween.Kill();
        Vector3 point = _currentPoint;
        MovePosition(point);
    }

    private IEnumerator Wait()
    {
        yield return _waitForSeconds;
        _collider.enabled = false;
        StartMove?.Invoke();
        Vector3 point = _finishPoint;
        MovePosition(point);
       
        StartCoroutine(TempOffCollider());
        StopCoroutine(Wait());

    }

    private IEnumerator TempOffCollider()
    {
        yield return _waitForSeconds;
        _collider.enabled = true ;
        StopCoroutine(TempOffCollider());
    }

    private void OnTrashBagReachedFinish()
    {
        int price = 10;

        _wallet.AddResource(price);
    }

    private void OnChangeWorkPlace(Vector3 currentPoint)
    {
        _currentPoint = currentPoint;
    }
}
