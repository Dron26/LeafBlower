using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Service
{
    public class LeaveBlowerStandart : MonoBehaviour
    {
        [SerializeField] private Store _store;

        List<Parametrs> _parametrs = new List<Parametrs>();
        public int Level { get => _level; private set { } }

        private ParticleSystemForceField _forceField;
        private FuelChanger _fuelChanger;
        private int _level;
        private float _stepChangeLevel;
        private float _stepRefuelingLevel;

        private float _directionX;
        private float _directionY;
        private float _directionZ;
        private float _endRang;

        private AirZone _airZone;
        private bool _isThereFuel= true;

        public void Awake()
        {
            _fuelChanger = GetComponentInParent<FuelChanger>();
            _forceField = GetComponentInChildren<ParticleSystemForceField>();

            _airZone = GetComponentInChildren<AirZone>();
            _airZone.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            //_store.UpFuel += OnUpLevel;
            _fuelChanger.ChangeFuel += OnChangeFuel;

        }

        private void OnUpLevel()
        {
            _level++;
        }

        private void SetParametrs(int _level)
        {            
            _stepChangeLevel= _parametrs[_level].StepChangeLevel;
            _stepRefuelingLevel = _parametrs[_level].StepRefuelingLevel;

            _forceField.directionX = _parametrs[_level].DirectionX;
            _forceField.directionY = _parametrs[_level].DirectionY;
            _forceField.directionZ = _parametrs[_level].DirectionZ;
            _forceField.endRange= _parametrs[_level].EndRang;

            _fuelChanger.SetItemParametrs(_stepChangeLevel, _stepRefuelingLevel);
        }

        private void Initialize()
        {
            int maxUpdateLevels = 10;

            _stepChangeLevel = 0.1f;
            _stepRefuelingLevel = 0.2f;

            _directionX = 10;
            _directionY = 10;
            _directionZ = 10;
            _endRang = 0.05f;

            float _stepUpChangeLevel = 0.17f;
            float _stepUpDirection = 1;
            float _stepUpEndRange = 1;

            for (int i = 0; i < maxUpdateLevels; i++)
            {
                _parametrs.Add(new Parametrs( _stepChangeLevel, _stepRefuelingLevel, _directionX, _directionY, _directionZ, _endRang));

                _stepChangeLevel += _stepUpChangeLevel;
                _stepRefuelingLevel++;
                _directionX += _stepUpDirection;
                _directionY += _stepUpDirection;
                _directionZ += _stepUpDirection;
                _endRang += _stepUpEndRange;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WorkPlace workPlace) & _isThereFuel == true)
            {
                _airZone.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out WorkPlace workPlace))
            {
                _airZone.gameObject.SetActive(false);
            }
        }

        private void OnChangeFuel(float fuelLevel)
        {
            if (fuelLevel == 0)
            {
                _isThereFuel = false;
                _airZone.gameObject.SetActive(false);
            }
            else
            {
                _isThereFuel = true;
            }
        }
        private void OnDisable()
        {
            //_store.UpFuel -= OnUpLevel;
            _fuelChanger.ChangeFuel -= OnChangeFuel;
        }

        public void LoadData(int level)
        {
            Level=level;           
            SetParametrs(_level);
        }
    }
}
