using System.Collections.Generic;
using _1Game.Scripts.Core;
using UnityEngine;

namespace _1Game.Scripts.Item
{
    public class LeaveBlowerAdvanced : MonoBehaviour
    {
 private Store _store;

        List<Parametrs> _parametrs = new List<Parametrs>();
        public int Level { get => _level; private set { } }
        public float MaxFueLevell { get => _maxFuelLevel; private set { } }

        private ParticleSystemForceField _forceField;
        private FuelChanger _fuelChanger;
        private int _level;
        private float _maxFuelLevel;
        private float _stepChangeLevel;
        private float _stepRefuelingLevel;

        private float _directionX;
        private float _directionY;
        private float _directionZ;
        private float _endRang;


        public void Awake()
        {
            _fuelChanger = GetComponentInParent<FuelChanger>();
            _forceField = GetComponentInChildren<ParticleSystemForceField>();
        }

        private void OnUpFuel()
        {
            _level++;
        }

        public void LoadParametr()
        {
            _level = 0;

            SetParametrs(_level);
        }

        private void SetParametrs(int _level)
        {
            _stepChangeLevel = _parametrs[_level].StepChangeLevel;
            _stepRefuelingLevel = _parametrs[_level].StepRefuelingLevel;

            _forceField.directionX = _parametrs[_level].DirectionX;
            _forceField.directionY = _parametrs[_level].DirectionY;
            _forceField.directionZ = _parametrs[_level].DirectionZ;
            _forceField.endRange = _parametrs[_level].EndRang;

            _fuelChanger.SetItemParametrs( _stepChangeLevel, _stepRefuelingLevel);
        }

        private void Initialize()
        {
            int maxUpdateLevels = 20;

            _stepChangeLevel = 0.2f;
            _stepRefuelingLevel = 0.4f;

            _directionX = 20f;
            _directionY = 20f;
            _directionZ = 20f;
            _endRang = 1.5f;

            float _stepUpChangeLevel = 0.27f;
            float _stepUpDirection = 1f;
            float _stepUpEndRange = 0.2f;

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

        public void SetStore(Store store)
        {
            _store = store;
        }

        public void LoadData(int level)
        {
            Level = level;
            SetParametrs(_level);
        }
    }
}