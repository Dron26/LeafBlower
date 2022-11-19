using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Service;
using UI;

namespace Core
{
    public class Cart : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private UIParkStat _uIParkStat;
        private FinishPoint _finishPoint;
        private WorkPlacesSwitcher _workPlacesSwitcher;
        private StageController _stageController;
        public float Time { get => _time; set { } }

        private Vector3 _currentPoint;
        private Vector3 point;
        private Tween _tween;
        private Collider _collider;
        private CartTrashBagPicker _cartTrashBagPicker;
        private WaitForSeconds _waitForSeconds;
        private const float _time = 5f;
        private bool isMoveFinish;

        public UnityAction StartMove;
        public UnityAction FinishMove;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _stageController = GetComponentInParent<StageController>();
            isMoveFinish = false;


            float waiteTime = 2f;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _cartTrashBagPicker = GetComponent<CartTrashBagPicker>();
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
            _cartTrashBagPicker.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
            _cartTrashBagPicker.BagReachedFinish += OnTrashBagReachedFinish;
        }

        private void Start()
        {
            _currentPoint = transform.position;
            InitializeUIStat();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FinishPoint finish))
            {
                _cartTrashBagPicker.ClearCart();
                SetSecondPosition();
            }
            else if (other.TryGetComponent(out ParkPlace parkPlace))
            {
                FinishMove?.Invoke();
            }
        }

        private void InitializeUIStat()
        {
            _uIParkStat.Initialize(_time);
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            point = _finishPoint.transform.position;
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

        private void OnChangeWorkPlace(GameObject insideControllers)
        {
            StartCoroutine(WaitMoveFinish(insideControllers));
        }

        private void OnDisable()
        {
            _cartTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;
            _cartTrashBagPicker.BagReachedFinish -= OnTrashBagReachedFinish;

            if (_workPlacesSwitcher != null)
            {
                _workPlacesSwitcher.ChangeWorkPlace -= OnChangeWorkPlace;
            }
        }

        private IEnumerator WaitMoveFinish(GameObject insideControllers)
        {
            _finishPoint = insideControllers.GetComponentInChildren<FinishPoint>();
            Vector3 currentPoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;

            if (isMoveFinish == true)
            {
                _tween.Kill();
                point = _finishPoint.transform.position;
                MovePosition();
            }
            else
            {
                _currentPoint = currentPoint;
                SetSecondPosition();
            }
            yield return null;
        }

        private void OnSetStage(GameObject stage)
        {
            _workPlacesSwitcher = stage.GetComponentInChildren<WorkPlacesSwitcher>();
            _workPlacesSwitcher.ChangeWorkPlace += OnChangeWorkPlace;
        }
    }
}
