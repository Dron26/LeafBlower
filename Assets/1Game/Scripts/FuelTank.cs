using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private List<ParticleSystemController> _particleSystemControllers;
    [SerializeField] private List<WorkPlace> _workPlaces;
    [SerializeField] private Character _character;

    public float MaxFueLevell { get => _maxFuelLevel; private set { } }
    public float FuelLevel { get => _fuelLevel; private set { } }
    
    private WaitForSeconds _waitForSeconds;
    private bool _isContact;
    private bool _isEnterWorkPlace;
    private bool _isExiteFuelPlace;
    private float _fuelLevel;
    private float _maxFuelLevel;
    private float _stepChangeLevel;
    private bool _isWrk;
    public UnityAction<float> ChangeFuel;


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
        _stepChangeLevel = 0.2f;
        StartCoroutine(ChangeFuelLevel());
        float waiteTime = 0.1f;
        _waitForSeconds = new WaitForSeconds(waiteTime);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _particleSystemControllers.Count; i++)
        {
            _particleSystemControllers[i].ContactAirZone += OnContactAirZone;
        }

        for (int i = 0; i < _particleSystemControllers.Count; i++)
        {
            _workPlaces[i].EnterWorkPlace += OnEnterWorkPlacek;
        }

    }

    private void OnDisable()
    {
        for (int i = 0; i < _particleSystemControllers.Count; i++)
        {
            _particleSystemControllers[i].ContactAirZone -= OnContactAirZone;
        }

        for (int i = 0; i < _particleSystemControllers.Count; i++)
        {
            _workPlaces[i].EnterWorkPlace += OnEnterWorkPlacek;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            StartCoroutine(StartRefueling());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _isExiteFuelPlace = true;
        }
    }

    private void OnContactAirZone(bool isContact)
    {
        _isContact = isContact;
    }

    private IEnumerator ChangeFuelLevel()
    {
        while (_isWrk)
        {
            while (_isEnterWorkPlace& _isContact)
            {
                if (_fuelLevel > 0)
                {
                    _fuelLevel -= _stepChangeLevel;
                    _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);
                }

                ChangeFuel?.Invoke(_fuelLevel);

                yield return _waitForSeconds;
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

        while (_isExiteFuelPlace==false)
        {
            if (_fuelLevel < _maxFuelLevel)
            {
                _fuelLevel += _stepChangeLevel;
                _fuelLevel = Mathf.Clamp(_fuelLevel, 0, _maxFuelLevel);
            }

            yield return _waitForSeconds;
        }

        StopCoroutine(StartRefueling());
    }


    public void Initialize()
    {
        _maxFuelLevel = 100;
    }

    public void LoadParametr(int countReelsKilledZombi)
    {
       
    }

}
