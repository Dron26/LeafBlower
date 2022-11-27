using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Service
{
    public class FuelChanger : MonoBehaviour
    {
        public float MaxFueLevell { get => _maxFuelLevel; private set { } }
        public float FuelLevel { get => _fuelLevel; private set { } }

        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _waitForRefuelSeconds;

        private bool _isContact;
        private bool _isEnterWorkPlace;
        private bool _isExiteFuelPlace;
        private bool _isWork;

        private float _fuelLevel;
        private float _maxFuelLevel = 100;
        private float _allMaxFuelLevel;
        private float _stepChangeLevel;
        private float _stepRefuelingLevel;
        private float _minLevel = 15f;

        public UnityAction<float> ChangeFuel;
        public UnityAction<bool> ReachedMinLevel;

        private void Start()
        {
            _isExiteFuelPlace = true;
            _fuelLevel = _maxFuelLevel;
            _isWork = true;
            _stepChangeLevel = 0.1f;
            _stepRefuelingLevel = 0.4f;
            float waiteTime = Time.fixedDeltaTime;
            float waiteRefuelTime = Time.fixedDeltaTime;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _waitForRefuelSeconds = new WaitForSeconds(waiteRefuelTime);

            StartCoroutine(ChangeFuelLevel());
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
                OnEnterWorkPlace(true);
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
                OnEnterWorkPlace(false);
            }
        }

        private void OnEnterWorkPlace(bool isWork)
        {
            _isEnterWorkPlace = isWork;
        }

        private IEnumerator ChangeFuelLevel()
        {
            while (_isWork)
            {
                if (_isEnterWorkPlace & _fuelLevel > 0)
                {
                    _fuelLevel -= _stepChangeLevel;
                    _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);

                    ChangeFuel?.Invoke(_fuelLevel);
                }

                if (_fuelLevel< _minLevel)
                {
                    ReachedMinLevel?.Invoke(true);
                }

                yield return _waitForSeconds;
            }
        }

        private IEnumerator StartRefueling()
        {
            _isExiteFuelPlace = false;
            ReachedMinLevel?.Invoke(false);

            while (_isExiteFuelPlace == false)
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    _fuelLevel += _stepRefuelingLevel;
                    _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);
                    ChangeFuel?.Invoke(_fuelLevel);
                }

                if (_fuelLevel > _minLevel)
                {
                    ReachedMinLevel?.Invoke(false);
                }

                yield return _waitForRefuelSeconds;
            }
        }

        public void SetItemParametrs( float stepChangeLevel, float stepRefuelingLevel)
        {
            _stepChangeLevel = stepChangeLevel;
            _stepRefuelingLevel = stepRefuelingLevel;
        }

        public void SetFuelParametrs(float maxFuelLevel)
        {
            _maxFuelLevel = maxFuelLevel;
            _fuelLevel = _maxFuelLevel;
        }
    }
}
