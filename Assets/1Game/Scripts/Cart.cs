using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [SerializeField] private FinishPointCart _finishPointCart;

    public float Time { get => _time; set { } }

    private Vector3 _startPoint;
    private Vector3 _finishPoint;
    private Tween _tween;
    private Collider _collider;
    private CartTrashBagPicker _trashBagPicker;

    private float _time;

    public UnityAction StartMove;

    private void Awake()
    {
        _trashBagPicker = GetComponent<CartTrashBagPicker>();
        _time = 5f;
    }


    private void Start()
    {
        _finishPoint = _finishPointCart.transform.localPosition;
        _startPoint = transform.localPosition;
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _trashBagPicker.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out FinishPointCart finish))
        {
            SetSecondPosition();
            _trashBagPicker.ClearCart();
        }
    }



    private void OnDisable()
    {
        _trashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;
    }

    private void OnTakeMaxQuantityTrashBag()
    {
        StartMove?.Invoke();
        Vector3 point = _finishPoint;
        MovePosition(point);
    }

    private void MovePosition(Vector3 point)
    {
        _tween = transform.DOLocalMove(point, _time);
    }

    public void SetSecondPosition()
    {
        _tween.Kill();
        Vector3 point = _startPoint;
        MovePosition(point);
    }

    private void OnCartEnter()
    {

    }
}
