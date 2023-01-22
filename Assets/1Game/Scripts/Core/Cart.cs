using System.Collections;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using DG.Tweening;
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

        [FormerlySerializedAs("_uIParkStat")] [SerializeField]
        private FinishPoint _finishPosition;

        private WorkPlacesSwitcher _workPlacesSwitcher;
        private StageController _stageController;
        private Vector3 _startPoint;
        private Vector3 _parkPlacePoint;
        private Vector3 _finishPoint;
        private Tween _tween;
        private Collider _collider;
        private CartTrashBagPicker _cartTrashBagPicker;
        private WaitForSeconds _waitForSeconds;
        private Coroutine _corountine;
        public float Time => _time;
        private const float _time = 5f;
        private bool _isMove;

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
            _finishPosition = insideControllers.GetComponentInChildren<FinishPoint>();
            _parkPlacePoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;

            _finishPoint = _finishPosition.transform.position;
            _startPoint = _parkPlacePoint;

            if (_isMove == false)
            {
                StartCoroutine(WaitBeforeMove(_parkPlacePoint));
            }
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            _startPoint = transform.position;
            _finishPoint = _finishPosition.transform.position;
            StartCoroutine(WaitBeforeMove(_finishPoint));
        }

        private IEnumerator TempOffCollider()
        {
            while (this.gameObject != null)
            {
                if (_isMove==false)
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

        private IEnumerator WaitBeforeMove(Vector3 position)
        {
            yield return _waitForSeconds;
            StartMove?.Invoke();
            Move(position);
        }

        private IEnumerator WaitFinish(Vector3 position)
        {
            _isMove = true;

            while (transform.position != position)
            {
                if (transform.position == _finishPoint)
                {
                    _cartTrashBagPicker.ClearCart();
                    _tween.Kill();
                    Move(_startPoint);
                }

                yield return null;
            }

            _isMove = false;
            yield break;
        }

        private void Move(Vector3 position)
        {
            if (_corountine != null)
            {
                StopCoroutine(_corountine);
            }

            _corountine = StartCoroutine(WaitFinish(position));


            _tween = transform.DOMove(position, _time);
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