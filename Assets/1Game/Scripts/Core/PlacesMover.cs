using UnityEngine;
using Service;
using System.Collections.Generic;

namespace Core
{
    public class PlacesMover : MonoBehaviour
    {
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private ParkPlace _parkPlace;
        [SerializeField] private FuelPlace _fuelPlace;


        private WorkPlacesSwitcher _switcher;
        private Vector3 _parkPoint;
        private Vector3 _upgradePoint;
        private Vector3 _fuelPoint;

        private UpgradePlace _currentUpgradePlace;
        private ParkPlace _currentParkPlace;
        private FuelPlace _currentFuelPlace;



        private InsideController _inside;
        private List<WorkPlace> _workPlaces;

        private void Awake()
        {
            _switcher = GetComponent<WorkPlacesSwitcher>();
        }

        private void OnEnable()
        {
            _switcher.ChangeWorkPlace += OnChangePlace;
        }
        private void OnDisable()
        {
            _switcher.ChangeWorkPlace -= OnChangePlace;
        }

        private void Start()
        {
            Initialize();
            CreatePlace();
        }
        private void Initialize()
        {
            List<WorkPlace> _tempPlaces;

            _tempPlaces = _switcher.GetWorkPlaces();

            for (int i = 0; i < _tempPlaces.Count; i++)
            {
                _workPlaces.Add(_tempPlaces[i]);
            }
        }

        private void CreatePlace()
        {
            int number = 0;

            for (int i = 0; i < _workPlaces.Count; i++)
            {
                _inside = _workPlaces[i].GetComponentInChildren<InsideController>();

                if (_inside._numberPlace == number)
                {
                    _upgradePoint = _workPlaces[i].GetComponentInChildren<UpgradePlacePoint>().transform.position;
                    _currentUpgradePlace = Instantiate(_upgradePlace, _upgradePoint, Quaternion.identity);

                    _parkPoint = _workPlaces[i].GetComponentInChildren<ParkPlacePoint>().transform.position;
                    _currentParkPlace = Instantiate(_parkPlace, _parkPoint, Quaternion.identity);

                    _fuelPoint = _workPlaces[i].GetComponentInChildren<FuelPlacePoint>().transform.position;
                    _currentFuelPlace = Instantiate(_fuelPlace, _fuelPoint, Quaternion.identity);              
                }
            }
        }

        private void OnChangePlace(GameObject insideControllers)
        {
            Vector3 currentPoint = insideControllers.GetComponentInChildren<UpgradePlacePoint>().transform.position;
            _currentUpgradePlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;
            _currentParkPlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<FuelPlacePoint>().transform.position;
            _currentFuelPlace.transform.position = currentPoint;
        }
    }
}
