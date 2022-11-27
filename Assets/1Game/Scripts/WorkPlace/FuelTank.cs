using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Events;
using UI;
namespace Service
{
    public class FuelTank : MonoBehaviour
    {
        [SerializeField] private FuelChanger _changer;
        [SerializeField] private FuelPanel _fuelPanel;
        public float MaxFueLevel { get => _maxFuelLevel; private set { } }

        List<float> _parametrs = new List<float>();

        private float _maxFuelLevel;
        private int _level;

        public UnityAction<float>  UpVolumeFuel;

        private void OnEnable()
        {
            _fuelPanel.UpFuel += OnUpLevel;
        }

        private void Start()
        {
            Initialize();
        }

        private void OnUpLevel()
        {
            _level++;
            _maxFuelLevel = _parametrs[_level];
            UpVolumeFuel?.Invoke(_maxFuelLevel);
        }

        private void LoadData(int _level)
        {
            _maxFuelLevel = _parametrs[_level];

        }

        private void Initialize()
        {
            int maxUpdateLevels = 10;

            _maxFuelLevel = 100;

            float _stepUpFuelLevel = 20;

            for (int i = 0; i < maxUpdateLevels; i++)
            {
                _parametrs.Add(_maxFuelLevel);

                _maxFuelLevel += _stepUpFuelLevel;
            }

            _maxFuelLevel= _parametrs[0];
        }


        private void OnDisable()
        {
            _fuelPanel.UpFuel -= OnUpLevel;
        }
    }
}