using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core;

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
    private float _fuelLevel;
    private float _maxFuelLevel;
    private float _stepChangeLevel;
    private float _stepRefuelingLevel;
    private bool _isWrk;

    public UnityAction<float> ChangeFuel;
    public UnityAction EndFuel;

    public void Awake()
    {
        _isExiteFuelPlace = true;
        _maxFuelLevel = 100;
        _fuelLevel = _maxFuelLevel;
    }

    private void Start()
    {
        _isWrk = true;
        _fuelLevel = _maxFuelLevel;
        _stepChangeLevel = 0.1f;
        _stepRefuelingLevel = 0.2f;
        StartCoroutine(ChangeFuelLevel());
        float waiteTime = Time.fixedDeltaTime;
        float waiteRefuelTime = Time.fixedDeltaTime;

        _waitForSeconds = new WaitForSeconds(waiteTime);
        _waitForRefuelSeconds = new WaitForSeconds(waiteRefuelTime);
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
            OnEnterWorkPlacek(true);
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
            OnEnterWorkPlacek(false);
        }
    }

    private IEnumerator ChangeFuelLevel
        ()
    {
        while (_isWrk)
        {
            if (_isEnterWorkPlace & _fuelLevel > 0)
            {
                _fuelLevel -= _stepChangeLevel;
                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);

                ChangeFuel?.Invoke(_fuelLevel);
            }
            yield return _waitForSeconds;
        }
    }

    private void OnEnterWorkPlacek(bool isWork)
    {
        _isEnterWorkPlace = isWork;
    }

    private IEnumerator StartRefueling()
    {
        _isExiteFuelPlace = false;

        while (_isExiteFuelPlace == false)
        {
            if (_fuelLevel < _maxFuelLevel)
            {
                _fuelLevel += _stepRefuelingLevel;
                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);
                ChangeFuel?.Invoke(_fuelLevel);
            }

            yield return _waitForRefuelSeconds;

        }
    }

        public void Initialize()
    {
        _maxFuelLevel = 20;
    }

}
}
