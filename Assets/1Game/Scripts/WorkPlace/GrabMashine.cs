using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Service;

namespace Core
{
    public class GrabMashine : MonoBehaviour
    {
        [SerializeField] private TrashBag _trashBag;
        
        private ParticleSystemController _particleSystem;
        private WorkPlace _workPlace;
        private FinishPointForTrashBag _finishPoint;
        private LeaveInGrab leaveInGrab;
        private GrabMashine _grabMashine;
        private TrashBagIdle _trashBagIdle;
        private CreatePoint _createPoint;
        private Vector3 _trashBagStartSize;
        private Vector3 _trashBagStartPosition;
        private Vector3 _tempSize;
        private Quaternion _rotationTrashBagIdle;
        private float _maxSize;
        private float _stepUpSize;
        private float _maxStepUp;
        private float _stepSizeDown;
        private float _minSizeParticle;
        private int _quantityCathcedParticle;
        private bool _isWork;
        private int _quantityUpSize;
        private int _tempQuantityUpSize;
        private int _quantityStepUp;
        private int _tempQuantityStepUp;
        private int _quantityAllStepUp;
        private WaitForSeconds _waitForSeconds;
        public UnityAction<bool> StartFillng;
        public UnityAction CreateNewTrashBag;
        private int maxVolumeTrashBag;
        private bool isFilling;

        private void Start()
        {
            _workPlace = GetComponentInParent<WorkPlace>();
            _particleSystem= _workPlace.GetComponentInChildren<ParticleSystemController>();
            leaveInGrab = GetComponent<LeaveInGrab>();
            _isWork = true;
            _finishPoint = GetComponentInChildren<FinishPointForTrashBag>();
            _createPoint = GetComponentInChildren<CreatePoint>();
            maxVolumeTrashBag = 200;
            _grabMashine = GetComponentInParent<GrabMashine>();
            float waiteTime = 0.2f;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _trashBagIdle = GetComponentInChildren<TrashBagIdle>();
            _rotationTrashBagIdle = _trashBagIdle.transform.localRotation;
            _trashBagStartSize = _trashBagIdle.transform.localScale;
            _trashBagStartPosition = _trashBagIdle.transform.localPosition;
            _tempSize = _trashBagStartSize;
            _maxSize = 1.13f;
            _maxStepUp = 0.2f;
            _stepUpSize = 0.05f;
            _quantityUpSize = 50;
            _tempQuantityUpSize = _quantityUpSize;
            _quantityStepUp = 4;

            StartCoroutine(CountParticle());
        }

        private IEnumerator ChangeSize()
        {
            isFilling = true;

            while (_tempSize.x <= _trashBagStartSize.x + _maxStepUp)
            {
                _trashBagIdle.transform.localScale = new Vector3(_tempSize.x + _stepUpSize, _tempSize.y + _stepUpSize, _tempSize.z + _stepUpSize);
                _tempSize = _trashBagIdle.transform.localScale;
                yield return _waitForSeconds;
            }

            _tempQuantityStepUp++;
            _quantityAllStepUp--;

            if (_tempQuantityStepUp == _quantityStepUp)
            {
                EndFill();
            }

            StopCoroutine(ChangeSize());
            isFilling = false;
        }

        private IEnumerator CountParticle()
        {
            while (_isWork == true)
            {
                if (_quantityAllStepUp > 0 & isFilling == false)
                {
                    StartFillng?.Invoke(true);
                    StartCoroutine(ChangeSize());
                }
                yield return null;
            }
        }

        private IEnumerator WaitQueue()
        {
            while (_isWork == true)
            {
                if (_quantityAllStepUp == 0 & isFilling == false & _quantityCathcedParticle >= _quantityUpSize)
                {
                    EndFill();
                    _isWork = false;
                }
                else if (_quantityCathcedParticle < _quantityUpSize)
                {
                    _isWork = false;
                }

                yield return null;
            }

            StopCoroutine(WaitQueue());
        }

        private void OnCatchAllParticle()
        {
            StartCoroutine(WaitQueue());
        }

        private void EndFill()
        {

            _trashBagIdle.transform.localScale = _trashBagStartSize;
            _tempSize = _trashBagStartSize;
            _tempQuantityStepUp = 0;
            StartFillng?.Invoke(false);
            CreateTrashBag();
        }

        private void CreateTrashBag()
        {
            TrashBag _newTrashBag = Instantiate(_trashBag, _createPoint.transform.localPosition, Quaternion.identity);
            _newTrashBag.transform.SetParent(transform, false);
            _newTrashBag.GetComponent<TrashBagMover>().SetFirstPosition(_finishPoint.transform.localPosition);
            CreateNewTrashBag?.Invoke();
        }

        private void OnEnable()
        {
            _particleSystem.CatchAllParticle += OnCatchAllParticle;
        }

        private void OnDisable()
        {
            _particleSystem.CatchAllParticle -= OnCatchAllParticle;
        }

        public void OnGetParticle()
        {
            _quantityCathcedParticle++;

            if (_quantityCathcedParticle == _tempQuantityUpSize)
            {
                _quantityAllStepUp++;
                _tempQuantityUpSize += _quantityUpSize;
            }
            else if (_quantityCathcedParticle == maxVolumeTrashBag)
            {
                CreateTrashBag();
            }
        }


    }
}