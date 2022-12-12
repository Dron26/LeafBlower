using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core;

namespace Service
{
    public class FuelChanger : MonoBehaviour
    {
        [SerializeField] private Store _store;
        public int MaxFueLevel => _maxFuelLevel; 
        public float FuelLevel => _fuelLevel;
        
        public int Level => _level;
        

        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _waitForRefuelSeconds;

        private bool _isEnterWorkPlace;
        private bool _isExiteFuelPlace;
        private bool _isWork;

        private int _level;
        private float _fuelLevel;
        private int _maxFuelLevel=100;
        private float _stepChangeLevel;
        private float _stepRefuelingLevel;
        private float _minLevel = 15f;

        public UnityAction<float> ChangeFuel;
        public UnityAction<bool> ReachedMinLevel;
        public UnityAction ReachedMaxLevel;

        public UnityAction<float> UpVolumeFuel;

        private void OnEnable()
        {
            _store.UpFuel += OnUpLevel;
        }

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

        private void OnUpLevel(int value,int level)
        {
            _maxFuelLevel = value;
            _level = level;
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

                if ( _fuelLevel < _minLevel)
                {
                    ReachedMinLevel?.Invoke(true);
                }

                yield return _waitForSeconds;
            }
        }

        private IEnumerator StartRefueling()
        {
            bool isLevelUp = false;
            bool isFuelMax = false;
            _isExiteFuelPlace = false;
            ReachedMinLevel?.Invoke(false);

            while (_isExiteFuelPlace == false)
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    _fuelLevel += _stepRefuelingLevel;
                }
                

                if (_fuelLevel > _minLevel& isLevelUp==false)
                {
                    ReachedMinLevel?.Invoke(false);
                    isLevelUp = true;
                }
                
                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);
                
                if (_fuelLevel == _maxFuelLevel&isFuelMax==false)
                {
                    ReachedMaxLevel?.Invoke();
                    isFuelMax = true;
                }
                ChangeFuel?.Invoke(_fuelLevel);

                yield return _waitForRefuelSeconds;
            }
        }

        public void SetItemParametrs( float stepChangeLevel, float stepRefuelingLevel)
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
            _store.UpFuel -= OnUpLevel;
        }
    }
}
