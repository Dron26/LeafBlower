using System;
using System.Collections;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _1Game.Scripts.Core
{
    [RequireComponent(typeof(CartTrashBagPicker))]
    [RequireComponent(typeof(ChangerCartSize))]
    public class Cart : MonoBehaviour
    {
        [SerializeField] private ParkPoint _parkPoint;

        private FinishPoint _finishPosition;
        private GameObject _insideControllers;
        private WorkPlacesSwitcher _workPlacesSwitcher;
        private StageController _stageController;
        private Vector3 _startPoint;
        private Vector3 _parkPlacePoint;
        private Vector3 _finishPoint;
        private Tween _tween;
        private Collider _collider;
        private CartTrashBagPicker _cartTrashBagPicker;
        private WaitForSeconds _waitForSeconds;
        private Coroutine _waiteMove;
        private Coroutine _waiteMoveSell;
        private Coroutine _waiteMoveParkPlace;
        public float Time => _time;
        private const float _time = 5f;
        private bool _isMove;
        private bool _isMoveSell;

        public UnityAction StartMove;
        public UnityAction FinishMove;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _stageController = GetComponentInParent<StageController>();
            _isMove = false;

            float waiteTime = 2f;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _cartTrashBagPicker = GetComponent<CartTrashBagPicker>();
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
            _cartTrashBagPicker.TakeMaxQuantityTrashBag += OnTakeMaxQuantityTrashBag;
        }

        private void Start()
        {
            _startPoint = transform.position;
            _isMove = false;

            StartCoroutine(TempOffCollider());
        }

        private void OnSetStage(GameObject stage)
        {
            _workPlacesSwitcher = stage.GetComponentInChildren<WorkPlacesSwitcher>();
            _workPlacesSwitcher.ChangeWorkPlace += OnChangeWorkPlace;
        }

        private void OnChangeWorkPlace(GameObject insideControllers)
        {
            WaitMoveFinish(insideControllers);
        }

        private void WaitMoveFinish(GameObject insideControllers)
        {
            _insideControllers = insideControllers;
            _finishPosition = insideControllers.GetComponentInChildren<FinishPoint>();
            _parkPlacePoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;

            _finishPoint = _finishPosition.transform.position;
            _startPoint = transform.position;

            if (_startPoint != _parkPlacePoint && _isMove == false)
            {
                StartCoroutine(WaitBeforeMove());
            }
            else
            {
                if (_waiteMove != null)
                {
                    StopCoroutine(_waiteMove);
                }

                if (_isMoveSell == false)
                {
                    _waiteMove = StartCoroutine(WaitFinishMove(_parkPlacePoint));
                    _tween.Kill();
                    _tween = transform.DOMove(_parkPlacePoint, _time);
                    _isMove = true;
                }
            }
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            _startPoint = transform.position;
            _finishPoint = _finishPosition.transform.position;
            _isMove = true;
            StartCoroutine(TempOffCollider());
            StartCoroutine(WaitBeforeMoveToSell());
        }

        private IEnumerator TempOffCollider()
        {
            while (this.gameObject != null)
            {
                if (_isMove == false)
                {
                    _collider.enabled = true;
                }
                else
                {
                    _collider.enabled = false;
                }

                yield return _waitForSeconds;
            }

            yield break;
        }

        private IEnumerator WaitBeforeMove()
        {
            yield return _waitForSeconds;
            Move();
            yield break;
        }

        private IEnumerator WaitBeforeMoveToSell()
        {
            yield return _waitForSeconds;
            MoveToSell();
            yield break;
        }


        private IEnumerator WaitFinishMove(Vector3 position)
        {
            while (transform.position != position)
            {
                yield return null;
            }

            _tween.Kill();
            _isMove = false;
            yield break;
        }

        private void Move()
        {
            if (_waiteMove != null)
            {
                StopCoroutine(_waiteMove);
            }

            if (_isMove == false)
            {
                _waiteMove = StartCoroutine(WaitFinishMove(_parkPlacePoint));
                _tween.Kill();
                _tween = transform.DOMove(_parkPlacePoint, _time);
                _isMove = true;
            }
        }

        private IEnumerator WaitMoveSell(Vector3 position)
        {
            while (transform.position != position)
            {
                yield return null;
            }

            _cartTrashBagPicker.ClearCart();
            _tween.Kill();
            _isMove = false;
            _isMoveSell = false;
            Move();
            yield break;
        }

        private void MoveToSell()
        {
            if (_waiteMoveSell != null)
            {
                StopCoroutine(_waiteMoveSell);
            }

            _waiteMoveSell = StartCoroutine(WaitMoveSell(_finishPoint));
            _tween.Kill();
            _tween = transform.DOMove(_finishPoint, _time);
            _isMoveSell = true;
        }


        private void OnDisable()
        {
            _cartTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMaxQuantityTrashBag;

            if (_workPlacesSwitcher != null)
            {
                _workPlacesSwitcher.ChangeWorkPlace -= OnChangeWorkPlace;
            }
        }
    }
}