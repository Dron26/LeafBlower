using System.Collections;
using _1Game.Scripts.Empty;
using _1Game.Scripts.Particle;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class GrabMashine : MonoBehaviour
    {
        [SerializeField] private TrashBag _trashBag;

        private ParticleSystemController _particleSystem;
        private WorkPlace _workPlace;
        private FinishPointForTrashBag _finishPoint;
        private TrashBagIdle _trashBagIdle;
        private CreatePoint _createPoint;
        private Vector3 _trashBagStartSize;
        private Vector3 _tempSize;
        private WaitForSeconds _waitForSeconds;

        private float _stepUpSize;
        private float _maxStepUp;
        private int _quantityUpSize;
        private int _tempQuantityUpSize;
        private int _quantityStepUp;
        private int _tempQuantityStepUp;
        private int _quantityAllStepUp;
        private int _quantityCathcedParticle;
        private int _maxVolumeTrashBag;
        private bool _isWork;
        private bool _isFilling;

        public UnityAction<bool> StartFillng;
        public UnityAction CreateNewTrashBag;

        private void Awake()
        {
            _workPlace = GetComponentInParent<WorkPlace>();
            _particleSystem = _workPlace.GetComponentInChildren<ParticleSystemController>();
            _finishPoint = GetComponentInChildren<FinishPointForTrashBag>();
            _createPoint = GetComponentInChildren<CreatePoint>();
            _trashBagIdle = GetComponentInChildren<TrashBagIdle>();
        }
        
        private void OnEnable()
        {
            _particleSystem.CatchAllParticle += OnCatchAllParticle;
        }

        private void Start()
        {
            float waiteTime = 0.2f;
            _isWork = true;
            _maxVolumeTrashBag = 200;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _trashBagStartSize = _trashBagIdle.transform.localScale;
            _tempSize = _trashBagStartSize;
            _maxStepUp = 0.2f;
            _stepUpSize = 0.05f;
            _quantityUpSize = 50;
            _tempQuantityUpSize = _quantityUpSize;
            _quantityStepUp = 4;

            StartCoroutine(CountParticle());
        }

        private IEnumerator ChangeSize()
        {
            _isFilling = true;

            while (_tempSize.x <= _trashBagStartSize.x + _maxStepUp)
            {
                _trashBagIdle.transform.localScale = new Vector3(_tempSize.x + _stepUpSize, _tempSize.y + _stepUpSize,
                    _tempSize.z + _stepUpSize);
                _tempSize = _trashBagIdle.transform.localScale;
                yield return _waitForSeconds;
            }

            _tempQuantityStepUp++;
            _quantityAllStepUp--;

            if (_tempQuantityStepUp == _quantityStepUp)
            {
                EndFill();
            }

            _isFilling = false;
            yield break;
        }

        private IEnumerator CountParticle()
        {
            while (_isWork == true)
            {
                if (_quantityAllStepUp > 0 & _isFilling == false)
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
                if (_quantityAllStepUp == 0 & _isFilling == false & _quantityCathcedParticle >= _quantityUpSize)
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

            yield break;
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


        private void OnDisable()
        {
            _particleSystem.CatchAllParticle -= OnCatchAllParticle;
            StopCoroutine(CountParticle());
        }

        public void OnGetParticle()
        {
            _quantityCathcedParticle++;

            if (_quantityCathcedParticle == _tempQuantityUpSize)
            {
                _quantityAllStepUp++;
                _tempQuantityUpSize += _quantityUpSize;
            }
            else if (_quantityCathcedParticle == _maxVolumeTrashBag)
            {
                CreateTrashBag();
            }
        }
    }
}