using System.Collections;
using System.Collections.Generic;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
    public class CartTrashBagPicker : MonoBehaviour
    {
        private UpgradeParametrs _upgradeParametrs;

        public int TrashBagsReceivedCount => _trashBagsReceivedCount;
        public int MaxPickedBag => _maxPickedQuantity;

        public int MaxBagInLevel => _maxQuantityInLevel;

        private CharacterTrashBagPicker _trashBagPicker;
        private StageController _stageController;
        
        private TrashBagStorePoint _storePoint;
        private TrashBagStore _trashBagStore;
        private MainPointForTrashBag _mainPointForTrashBag;
        private TrashBagMover _trashBagMover;
        private Stack<TrashBag> _pickedTrashBags;
        private WaitForSeconds _waitForSeconds;
        private Cart _cart;
        private List<Vector3> _changePointStore;
        private Vector3 _mainPoint;
        private Vector3 _storeLocalPosition;
        private Vector3 _removePositionStorePoint;
        private Collider _collider;

        private int _quantityPickedTrashBag;
        private int _maxPickedQuantity;
        private int _maxQuantityInLevel;
        private int _count;
        private int _maxQuantityInRow;
        private int _trashBagsReceivedCount;
        private float stepInRow;
        private float _time = 0.5f;
        private bool _canTake;

        public UnityAction TakeTrashBag;
        public UnityAction TakeMaxQuantityTrashBag;
        public UnityAction BagReachedFinish;

        private void Awake()
        {
            _maxQuantityInLevel = 8;
            _upgradeParametrs = GetComponentInParent<UpgradeParametrs>();
            _trashBagsReceivedCount = 0;
            _cart = GetComponent<Cart>();
            _stageController = GetComponentInParent<StageController>();
            _trashBagPicker = _stageController.GetComponentInChildren<CharacterTrashBagPicker>();
            _storePoint = GetComponentInChildren<TrashBagStorePoint>();
            _mainPointForTrashBag = GetComponentInChildren<MainPointForTrashBag>();
            _trashBagStore=GetComponentInChildren<TrashBagStore>();
        }

        private void Start()
        {
            _maxQuantityInRow = 3;
            _pickedTrashBags = new Stack<TrashBag>();
            _changePointStore = new List<Vector3>();

            _storeLocalPosition = _storePoint.transform.localPosition;
            _mainPoint = _mainPointForTrashBag.transform.localPosition;
            stepInRow = 0.6f;

            _changePointStore.Add(new Vector3(_storeLocalPosition.x, _storeLocalPosition.y, _storeLocalPosition.z));
            _changePointStore.Add(new Vector3(_storeLocalPosition.x + stepInRow, _storeLocalPosition.y,
                _storeLocalPosition.z));
            _removePositionStorePoint = _storePoint.transform.localPosition;

            _trashBagsReceivedCount = _maxPickedQuantity - _pickedTrashBags.Count;
        }

        private void OnEnable()
        {
            _stageController.SetCharacter += OnSetCharacter;
            _trashBagPicker.SallTrashBag += OnSallTrashBag;
            _cart.FinishMove += OnFinishMove;
            _upgradeParametrs.UpCart += OnUpLevel;
        }

        private void OnDisable()
        {
            _stageController.SetCharacter -= OnSetCharacter;
            _trashBagPicker.SallTrashBag -= OnSallTrashBag;
            _cart.FinishMove -= OnFinishMove;
            _upgradeParametrs.UpCart -= OnUpLevel;
        }

        private void OnSallTrashBag(TrashBag trashBag)
        {
            if (_pickedTrashBags.Count != _maxPickedQuantity & _canTake == true)
            {
                _quantityPickedTrashBag++;
                float waiteTime = 2f;
                _waitForSeconds = new WaitForSeconds(waiteTime);
                _pickedTrashBags.Push(trashBag);

                trashBag.transform.SetParent(_trashBagStore.transform, true);
                _collider = trashBag.GetComponent<Collider>();
                _collider.enabled = false;
                _trashBagMover = trashBag.GetComponent<TrashBagMover>();
                _trashBagMover.ReachedFinish += OnReachedFinish;
                ChangeWay();
                TakeTrashBag?.Invoke();
                _trashBagsReceivedCount = _maxPickedQuantity - _pickedTrashBags.Count;

                if (_pickedTrashBags.Count == _maxPickedQuantity)
                {
                    _canTake = false;
                    StartCoroutine(WaitFillingAll());
                }
            }
        }

        private void ChangeWay()
        {
            float stepUpLevel = 0.4f;
            float stepinSecondRow = -0.4f;
            Vector3 point = new Vector3();

            _count++;

            if (_count == _maxQuantityInLevel)
            {
                Vector3 localPosition = _storePoint.transform.localPosition;
                localPosition = new Vector3(localPosition.x, localPosition.y + stepUpLevel, localPosition.z);
                _storePoint.transform.localPosition = localPosition;
                _storeLocalPosition = localPosition;
                _count = 1;
            }
            else if (_count == _maxQuantityInRow)
            {
                Vector3 localPosition = _storePoint.transform.localPosition;
                localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z + stepinSecondRow);
                _storePoint.transform.localPosition = localPosition;
                SetPoint();
                _count = 1;
            }

            point = _changePointStore[_count - 1];
            _trashBagMover.SetSecondPosition(point, _mainPoint, _time);
        }

        private void SetPoint()
        {
            _changePointStore.Clear();
            Vector3 localPosition = _storePoint.transform.localPosition;
            _changePointStore.Add(new Vector3(localPosition.x,
                localPosition.y, localPosition.z));
            _changePointStore.Add(new Vector3(localPosition.x + stepInRow,
                localPosition.y, localPosition.z));
        }

        private void OnUpLevel(int valume)
        {
            _maxPickedQuantity = valume;
            ChangeStorePosition();
        }

        public void ClearCart()
        {
            foreach (TrashBag trashBag in _pickedTrashBags)
            {
                Destroy(trashBag.gameObject);
            }

            _pickedTrashBags.Clear();
            _count = 0;
            _quantityPickedTrashBag = 0;
            _storePoint.transform.localPosition = _removePositionStorePoint;
            _storeLocalPosition = _storePoint.transform.localPosition;
            _trashBagsReceivedCount = _maxPickedQuantity;
            SetPoint();
        }

        private void OnFinishMove()
        {
            _canTake = true;
        }

        private IEnumerator WaitFillingAll()
        {
            yield return _waitForSeconds;
            TakeMaxQuantityTrashBag?.Invoke();
            yield break;
        }

        private void OnReachedFinish()
        {
            BagReachedFinish?.Invoke();
        }

        private void OnSetCharacter(Character character)
        {
            _trashBagPicker = character.gameObject.GetComponent<CharacterTrashBagPicker>();
        }

        public void LoadData()
        {
        }

        public void Initialize()
        {
            const int maxUpStep = 10;
            const int minQuantity = 6;
            const int stepOnQuamtity = 2;
            int numberLevel = 0;
            const int maxlevel = 5;
            const int maxQuantityInLevel = 12;

            _maxQuantityInLevel = minQuantity;
            _maxPickedQuantity = minQuantity;

            for (int i = 0; i < maxUpStep; i++)
            {
                _maxQuantityInLevel += stepOnQuamtity;
                _maxPickedQuantity += stepOnQuamtity;

                if (_maxQuantityInLevel == maxQuantityInLevel)
                {
                    numberLevel++;
                    _maxQuantityInLevel = 0;
                    _maxPickedQuantity = minQuantity;
                }
            }
        }

        private void ChangeStorePosition()
        {

            float upStep = 0.08f;
                Vector3 localPosition =_trashBagStore.transform.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y , localPosition.z+upStep);
            _trashBagStore.transform.localPosition = localPosition;
        }
    }
}