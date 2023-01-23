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
        private int _maxFuelLevelUpgrade = 300;
        private bool _isExiteFuelPlace;
        private bool _isWork;

        private float _fuelLevel=50;
        private int _maxFuelLevel = 100;
        private float _stepChangeLevel= 0.1f;
        private float _stepRefuelingLevel;
        private float _stepRefuel = 0.4f;
        private float _maxSteprefuel = 1.2f;
        private float _minLevel = 15f;

        public UnityAction<float> ChangeFuel;
        public UnityAction StartRefuel;
        public UnityAction StopRefuel;
        
        public UnityAction ReachedMinLevel;
        public UnityAction ReachedMaxLevel;

        public UnityAction<float> UpVolumeFuel;

        
        private void OnEnable()
        {
            _upgradeParametrs.UpFuel += OnUpLevel;
        }

        private void Start()
        {
            _isExiteFuelPlace = true;
            
            _isWork = true;
            float waiteTime = Time.fixedDeltaTime;
            float waiteRefuelTime = Time.fixedDeltaTime;
            _waitForSeconds = new WaitForSeconds(waiteTime);
            _waitForRefuelSeconds = new WaitForSeconds(waiteRefuelTime);
        }

        private void OnUpLevel(int value)
        {
            _maxFuelLevel = value;
            _stepRefuelingLevel+=_stepRefuel;
            
            if (_maxFuelLevel==_maxFuelLevelUpgrade)
            {
                _stepRefuelingLevel *= _maxSteprefuel;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FuelTank fuelTank))
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    StartRefuel?.Invoke();
                    StartCoroutine(StartRefueling());
                }
            }
            else if (other.TryGetComponent(out WorkPlace workPlace))
            {
                if (workPlace.IsCleaned==false)
                {
                    StartCoroutine(ChangeFuelLevel());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out FuelTank fuelTank))
            {
                _isExiteFuelPlace = true;
                StopRefuel?.Invoke();
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
                    ReachedMinLevel?.Invoke();
                }

                if (_fuelLevel == 0)
                {
                    _isWork = false;
                }

                yield return _waitForSeconds;
            }
            
            yield break;
        }

        private IEnumerator StartRefueling()
        {
            bool isLevelUp = false;
            bool isFuelMax = false;

            _isExiteFuelPlace = false;
            
            

            while (_isExiteFuelPlace == false)
            {
                if (_fuelLevel < _maxFuelLevel)
                {
                    _fuelLevel += _stepRefuelingLevel;
                }


                if (_fuelLevel > _minLevel & isLevelUp == false)
                {
                    isLevelUp = true;
                }

                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);

                if (_fuelLevel == _maxFuelLevel & isFuelMax == false)
                {
                    ReachedMaxLevel?.Invoke();
                    StopRefuel?.Invoke();
                    isFuelMax = true;
                }

                ChangeFuel?.Invoke(_fuelLevel);

                yield return _waitForRefuelSeconds;
            }

            yield break;
        }

        public void SetItemParametrs(float stepChangeLevel, float stepRefuelingLevel)
        {
            _stepChangeLevel = stepChangeLevel;
            _stepRefuelingLevel = stepRefuelingLevel;
        }

        private void OnDisable()
        {
            _upgradeParametrs.UpFuel -= OnUpLevel;
        }
    }
}