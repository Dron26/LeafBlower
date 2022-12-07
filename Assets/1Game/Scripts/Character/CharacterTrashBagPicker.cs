using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core;
using UI;
namespace Service
{
    public class CharacterTrashBagPicker : MonoBehaviour
    {

        [SerializeField] private ParkPlace _parkPlace;
        [SerializeField] private Cart _cart;
        [SerializeField] private CharacterPanelUI _characterPanel;
        [SerializeField] private Store _store;
        public int MaxPickedBag => _maxPickedQuantity;

        public int MaxBagInLevel => _maxQuantityInLevel;

        private CartTrashBagPicker _cartPiker;
        private TrashBagStorePoint _storePoint;
        private MainPointForTrashBag _mainPointForTrashBag;
        private TrashBagMover _trashBagMover;
        private WorkPlacesSwitcher _workPlacesSwitcher;
        private List<WorkPlace> _workPlaces;
        private StageController _stageController;
        private Stack<TrashBag> _pickedTrashBags;
        private Vector3 _mainPoint;
        private Vector3 _localPositionStorePoint;
        private List<Vector3> _changePointStore;
        private List<Vector3> _changePointCart;
        private WaitForSeconds _waitForSeconds;

        private int _cartTrashBagsReceivedCount;
        private int _quantityAllTrashBag;
        private int _quantityPickedTrashBag;
        private int _maxPickedQuantity;
        private int _quantityInLevel;
        private int _maxQuantityInLevel=4;
        private int numberLevel;
        private float _time;
        private float stepUpLevel = 0.4f;
        private bool _isTakedMaxQuantity;
        private bool _canSell;
        

        public UnityAction TakeTrashBag;
        public UnityAction<TrashBag> SallTrashBag;
        public UnityAction TakeMaxQuantityTrashBag;
        public UnityAction SallAllTrashBag;
        public UnityAction<Vector3> SetPosition;

        private void Awake()
        {
            _cartPiker = _cart.gameObject.GetComponent<CartTrashBagPicker>();
            _stageController = GetComponentInParent<StageController>();
            _storePoint = GetComponentInChildren<TrashBagStorePoint>();
        }

        private void OnEnable()
        {
            //_stageController.SetStage += OnSetStage;
            _cart.FinishMove += OnFinishCart;
            _store.UpPower += OnUpLevel;
        }

        private void Start()
        {
            _time = 0.2f;

            _mainPointForTrashBag = _storePoint.GetComponentInChildren<MainPointForTrashBag>();
            _pickedTrashBags = new Stack<TrashBag>();
            _changePointStore = new List<Vector3>();
            _canSell = true;
            float stepInRow = 0.3f;
            float stepinSecondRow = -0.4f;
            float _timeToSell = 0.1f;
            _waitForSeconds = new WaitForSeconds(_timeToSell);

            _mainPoint = _mainPointForTrashBag.transform.localPosition;

            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
            _localPositionStorePoint = _changePointStore[0];
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));

            //Initialize();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TrashBag trashBag))
            {
                PikedTrashBag(trashBag);
            }

            if (other.TryGetComponent(out Cart cart) & _pickedTrashBags.Count != 0)
            {
                SellTrashBags();
            }
        }

        private void ChangeWay(TrashBag trashBag)
        {
            int maxTrashBagInLevel = 4;
            Vector3 point = new Vector3();
            _quantityInLevel++;

            if (_quantityInLevel > maxTrashBagInLevel)
            {
                numberLevel++;
                _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y + stepUpLevel, _storePoint.transform.localPosition.z);
                SetPoint();
                _quantityInLevel = 1;
                point = _changePointStore[_quantityInLevel - 1];
            }
            else
            {
                point = _changePointStore[_quantityInLevel - 1];
            }

            _trashBagMover.SetSecondPosition(point, _mainPoint, _time);
        }

        private void SetPoint()
        {
            float stepInRow = 0.3f;
            float stepinSecondRow = -0.4f;
            _changePointStore.Clear();
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z));
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
            _changePointStore.Add(new Vector3(_storePoint.transform.localPosition.x + stepInRow, _storePoint.transform.localPosition.y, _storePoint.transform.localPosition.z + stepinSecondRow));
        }

        private void SellTrashBags()
        {
            if (_canSell == true)
            {
                StartCoroutine(SellBags());
            }
        }

        private void OnFinishCart()
        {
            _canSell = true;
        }

        private void OnDisable()
        {
            //_stageController.SetStage -= OnSetStage;
            _parkPlace.CartEnter -= OnFinishCart;
            _store.UpPower = OnUpLevel;
        }

        private IEnumerator SellBags()
        {
            _canSell = false;
            _cartTrashBagsReceivedCount = _cartPiker.TrashBagsReceivedCount;

            int quantity;           
            if (_pickedTrashBags.Count> _cartTrashBagsReceivedCount)
            {
                quantity = _cartTrashBagsReceivedCount;
            }
            else
            {
                quantity = _pickedTrashBags.Count;
            }

            for (int i = 0; i < quantity; i++)
            {
                yield return _waitForSeconds;

                _pickedTrashBags.TryPop(out TrashBag trashBag);
                SallTrashBag?.Invoke(trashBag);
                _quantityPickedTrashBag --;
                _quantityInLevel--;

                if (_quantityInLevel == 0&numberLevel>0)
                {
                    _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x, _storePoint.transform.localPosition.y - stepUpLevel, _storePoint.transform.localPosition.z);
                    SetPoint();
                    _quantityInLevel = 4;
                }

                if (_pickedTrashBags.Count == 0)
                {
                    _quantityPickedTrashBag =0;
                    _quantityInLevel = 0;
                    _storePoint.transform.localPosition = _localPositionStorePoint;
                    SallAllTrashBag?.Invoke();
                    SetPoint();
                }
            }                              

            _canSell = true;
        }

        private void PikedTrashBag(TrashBag trashBag)
        {
            if (_pickedTrashBags.Count != _maxPickedQuantity)
            {
                _quantityPickedTrashBag++;

                if (_quantityPickedTrashBag <= _maxPickedQuantity)
                {

                    _pickedTrashBags.Push(trashBag);
                    trashBag.transform.SetParent(transform, true);
                    _trashBagMover = trashBag.GetComponent<TrashBagMover>();
                    trashBag.ChangeMaterial();
                    ChangeWay(trashBag);
                    TakeTrashBag?.Invoke();
                }
            }
            else
            {
                TakeMaxQuantityTrashBag?.Invoke();
            }
        }

        public void OnUpLevel(int valume,int level)
        {
            _maxPickedQuantity = valume;
        }

        //public void Initialize()
        //{

        //    const int maxUpStep = 10;
        //    const int minQuantity = 4;
        //    const int stepOnQuamtity = 2;
        //    int numberLevel = 0;
        //    const int maxlevel = 5;
        //    const int maxQuantityInLevel = 4;

        //    _maxQuantityInLevel = minQuantity;
        //    _maxPickedQuantity = minQuantity;

        //    for (int i = 0; i < maxUpStep; i++)
        //    {
        //        _maxQuantityInLevel += stepOnQuamtity;
        //        _maxPickedQuantity += stepOnQuamtity;

        //        if (_maxQuantityInLevel == maxQuantityInLevel)
        //        {
        //            numberLevel++;
        //            _maxQuantityInLevel = 0;
        //        }
        //    }
        //}
    }
}

