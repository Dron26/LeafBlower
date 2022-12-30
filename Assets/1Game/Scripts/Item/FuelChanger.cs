using System.Collections;
using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Item
{
    public class FuelChanger : MonoBehaviour
    {
        [SerializeField] private UpgradeParametrs _upgradeParametrs;

        public int MaxFueLevel => _maxFuelLevel;
        public float FuelLevel => _fuelLevel;

        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _waitForRefuelSeconds;

        private bool _isExiteFuelPlace;
        private bool _isWork;
        private bool _isStartRefuil;

        private float _fuelLevel;
        private int _maxFuelLevel = 100;
        private float _stepChangeLevel;
        private float _stepRefuelingLevel;
        private float _minLevel = 15f;

        public UnityAction<float> ChangeFuel;
        public UnityAction<bool> ReachedMinLevel;
        public UnityAction ReachedMaxLevel;

        public UnityAction<float> UpVolumeFuel;

        private void OnEnable()
        {
            _upgradeParametrs.UpFuel += OnUpLevel;
        }

        private void Start()
        {
            _isStartRefuil = false;
            _isExiteFuelPlace = true;
            _fuelLevel = _maxFuelLevel;
            _isWork = true;
            _stepChangeLevel = 0.1f;
            _stepRefuelingLevel = 0.4f;
            float waiteTime = Time.fixedDeltaTime;
            float waiteRefuelTime = Time.fixedDeltaTime;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _waitForRefuelSeconds = new WaitForSeconds(waiteRefuelTime);
        }

        private void OnUpLevel(int value)
        {
            _maxFuelLevel = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FuelTank fuelTank))
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    StartCoroutine(StartRefueling());
                }
            }
            else if (other.TryGetComponent(out WorkPlace workPlace))
            {
                StartCoroutine(ChangeFuelLevel());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out FuelTank fuelTank))
            {
                _isExiteFuelPlace = true;
                StopCoroutine(StartRefueling());
            }
            else if (other.TryGetComponent(out WorkPlace workPlace))
            {
                _isWork = false;
                StopCoroutine(ChangeFuelLevel());
            }
        }

        private IEnumerator ChangeFuelLevel()
        {
            _isWork = true;

            while (_isWork)
            {
                if (_fuelLevel > 0)
                {
                    _fuelLevel -= _stepChangeLevel;
                    _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);

                    ChangeFuel?.Invoke(_fuelLevel);
                }

                if (_fuelLevel < _minLevel)
                {
                    ReachedMinLevel?.Invoke(true);
                }

                if (_fuelLevel == 0)
                {
                    _isWork = false;
                }

                yield return _waitForSeconds;
            }
        }

        private IEnumerator StartRefueling()
        {
            bool isLevelUp = false;
            bool isFuelMax = false;

            _isStartRefuil = true;
            _isExiteFuelPlace = false;

            while (_isExiteFuelPlace == false)
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    _fuelLevel += _stepRefuelingLevel;
                }


                if (_fuelLevel > _minLevel & isLevelUp == false)
                {
                    ReachedMinLevel?.Invoke(false);
                    isLevelUp = true;
                }

                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);

                if (_fuelLevel == _maxFuelLevel & isFuelMax == false)
                {
                    ReachedMaxLevel?.Invoke();
                    isFuelMax = true;
                }

                ChangeFuel?.Invoke(_fuelLevel);

                yield return _waitForRefuelSeconds;
            }

            _isStartRefuil = false;
        }

        public void SetItemParametrs(float stepChangeLevel, float stepRefuelingLevel)
        {
            _stepChangeLevel = stepChangeLevel;
            _stepRefuelingLevel = stepRefuelingLevel;
        }

        public void SetFuelParametrs(int maxFuelLevel)
        {
            _maxFuelLevel = maxFuelLevel;
            _fuelLevel = _maxFuelLevel;
        }

        private void OnDisable()
        {
            _upgradeParametrs.UpFuel -= OnUpLevel;
        }
    }
}