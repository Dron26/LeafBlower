using System.Collections;
using System.Collections.Generic;
using _1Game.Scripts.Empty;
using _1Game.Scripts.WorkPlaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class CharacterTrashBagPicker : MonoBehaviour
    {
        [SerializeField] private ParkPlace _parkPlace;
        [SerializeField] private Cart _cart;

        private UpgradeParametrs _upgradeParametrs;
        private CartTrashBagPicker _cartPiker;
        private TrashBagStorePoint _storePoint;
        private MainPointForTrashBag _mainPointForTrashBag;
        private TrashBagMover _trashBagMover;
        private Stack<TrashBag> _pickedTrashBags;
        private Vector3 _mainPoint;
        private Vector3 _localPositionStorePoint;
        private List<Vector3> _changePointStore;
        private WaitForSeconds _waitForSeconds;

        private int _cartTrashBagsReceivedCount;
        private int _quantityPickedTrashBag;
        private int _maxPickedQuantity;
        private int _quantityInLevel;
        private int _numberLevel;
        private float _time;
        private readonly float _stepUpLevel = 0.4f;
        private bool _canSell;


        public UnityAction TakeTrashBag;
        public UnityAction<TrashBag> SallTrashBag;
        public UnityAction TakeMaxQuantityTrashBag;
        public UnityAction SallAllTrashBag;

        private void Awake()
        {
            _upgradeParametrs = GetComponentInParent<UpgradeParametrs>();
            _cartPiker = _cart.gameObject.GetComponent<CartTrashBagPicker>();
            _storePoint = GetComponentInChildren<TrashBagStorePoint>();
            _mainPointForTrashBag = _storePoint.GetComponentInChildren<MainPointForTrashBag>();
        }

        private void OnEnable()
        {
            _cart.FinishMove += OnFinishCart;
            _upgradeParametrs.UpPower += OnUpLevel;
        }

        private void Start()
        {
            float stepInRow = 0.3f;
            float stepinSecondRow = -0.4f;
            float timeToSell = 0.1f;
            Transform transformStorePoint = _storePoint.transform;
            Vector3 localPositionStorePoint = transformStorePoint.localPosition;

            _time = 0.2f;
            _pickedTrashBags = new Stack<TrashBag>();
            _changePointStore = new List<Vector3>();
            _canSell = true;
            _waitForSeconds = new WaitForSeconds(timeToSell);
            _mainPoint = _mainPointForTrashBag.transform.localPosition;

            _changePointStore.Add(new Vector3(localPositionStorePoint.x, localPositionStorePoint.y,
                localPositionStorePoint.z));
            _localPositionStorePoint = _changePointStore[0];
            _changePointStore.Add(new Vector3(localPositionStorePoint.x + stepInRow, localPositionStorePoint.y,
                localPositionStorePoint.z));
            _changePointStore.Add(new Vector3(localPositionStorePoint.x, localPositionStorePoint.y,
                localPositionStorePoint.z + stepinSecondRow));
            _changePointStore.Add(new Vector3(localPositionStorePoint.x + stepInRow, localPositionStorePoint.y,
                localPositionStorePoint.z + stepinSecondRow));
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
                _numberLevel++;
                Vector3 storePointLocalPosition = _storePoint.transform.localPosition;
                storePointLocalPosition = new Vector3(storePointLocalPosition.x,
                    storePointLocalPosition.y + _stepUpLevel, storePointLocalPosition.z);
                _storePoint.transform.localPosition = storePointLocalPosition;
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
            Transform transformStorePoint = _storePoint.transform;
            Vector3 localPositionStorePoint = transformStorePoint.localPosition;

            _changePointStore.Clear();
            _changePointStore.Add(new Vector3(localPositionStorePoint.x, localPositionStorePoint.y,
                localPositionStorePoint.z));
            _changePointStore.Add(new Vector3(localPositionStorePoint.x + stepInRow, localPositionStorePoint.y,
                localPositionStorePoint.z));
            _changePointStore.Add(new Vector3(localPositionStorePoint.x, localPositionStorePoint.y,
                localPositionStorePoint.z + stepinSecondRow));
            _changePointStore.Add(new Vector3(localPositionStorePoint.x + stepInRow, localPositionStorePoint.y,
                localPositionStorePoint.z + stepinSecondRow));
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
            _parkPlace.CartEnter -= OnFinishCart;
            _upgradeParametrs.UpPower -= OnUpLevel;
        }

        private IEnumerator SellBags()
        {
            _canSell = false;
            _cartTrashBagsReceivedCount = _cartPiker.TrashBagsReceivedCount;

           int quantity = _cartTrashBagsReceivedCount>=_pickedTrashBags.Count ?  _pickedTrashBags.Count:_cartTrashBagsReceivedCount ;
            
            for (int i = 0; i < quantity; i++)
            {

                yield return _waitForSeconds;

                _pickedTrashBags.TryPop(out TrashBag trashBag);
                SallTrashBag?.Invoke(trashBag);
                _quantityPickedTrashBag=_pickedTrashBags.Count;
                _quantityInLevel--;

                if (_quantityInLevel == 0 & _numberLevel > 0)
                {
                    _storePoint.transform.localPosition = new Vector3(_storePoint.transform.localPosition.x,
                        _storePoint.transform.localPosition.y - _stepUpLevel, _storePoint.transform.localPosition.z);
                    SetPoint();
                    _quantityInLevel = 4;
                }

                if (_pickedTrashBags.Count == 0)
                {
                    _quantityPickedTrashBag = 0;
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

                if (_quantityPickedTrashBag > _maxPickedQuantity) return;
                _pickedTrashBags.Push(trashBag);
                trashBag.transform.SetParent(transform, true);
                _trashBagMover = trashBag.GetComponent<TrashBagMover>();
                trashBag.ChangeMaterial();
                ChangeWay(trashBag);
                TakeTrashBag?.Invoke();
            }
            else
            {
                TakeMaxQuantityTrashBag?.Invoke();
            }
        }

        private void OnUpLevel(int value)
        {
            _maxPickedQuantity = value;
        }
    }
}