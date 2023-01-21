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
        
        [SerializeField]private ParkPoint _parkPoint;
        
        [FormerlySerializedAs("_uIParkStat")] [SerializeField]
        private ParkPlaceInfo _uIParkPlaceInfo;

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
            InitializeUIStat();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FinishPoint finish))
            {
               
            }
            else if (other.TryGetComponent(out ParkPlace parkPlace))
            {
                FinishMove?.Invoke();
            }
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

            if (_isMove == true)
            {
                _finishPoint = _finishPosition.transform.position;
                _startPoint = _parkPlacePoint;
            }
            else
            {
                _finishPoint = _parkPlacePoint;
                WaitBeforeMove();
                
            }
        }

        private void OnTakeMaxQuantityTrashBag()
        {
            _startPoint = transform.position;
            _finishPoint = _finishPosition.transform.position;
            StartCoroutine(TempOffCollider());
            StartCoroutine(WaitBeforeMove());
        }
        
        private IEnumerator TempOffCollider()
        {
            _collider.enabled = false;
            yield return _waitForSeconds;
            _collider.enabled = true;
            yield break;
        }
        
        private IEnumerator WaitBeforeMove()
        {
            _isMove = true;
            yield return _waitForSeconds;
            StartMove?.Invoke();
            Move(_finishPoint);
        }

        private IEnumerator WaitFinish(Vector3 position)
        {
            
            
            while (transform.position != position)
            {
                if (transform.position == position)
                {
                    if (position==_finishPoint)
                    {
                        _cartTrashBagPicker.ClearCart();
                        _tween.Kill();
                        Move(_startPoint);
                    }
                    else if (position==_startPoint)
                    {
                        _isMove = false;
                    }
                }
                
                yield return null;
            }
            
            yield break;
        }
        
        private void Move( Vector3 position )
        {
            if (WaitFinish(position)!=null)
            {
                StopCoroutine(WaitFinish(position));
            }
            else
            {
                StartCoroutine(WaitFinish(position));
            }

            _tween = transform.DOMove(position, _time);
        }
        
        private void InitializeUIStat()
        {
            _uIParkPlaceInfo.Initialize(_time);
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