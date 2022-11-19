using UnityEngine;
using Service;
using System.Collections.Generic;

namespace Core
{
    public class PlacesMover : MonoBehaviour
    {
        private UpgradePlace _upgradePlace;
        private ParkPlace _parkPlace;
        private FuelPlace _fuelPlace;
        private ExitPlace _exitPlace;

        private StageController _stageController;
        private WorkPlacesSwitcher _switcher;

        private Vector3 _parkPoint;
        private Vector3 _upgradePoint;
        private Vector3 _fuelPoint;
        private Vector3 _exitPoint;

        private InsideController _inside;
        private List<WorkPlace> _workPlaces = new List<WorkPlace>();

        private void Awake()
        {
            _stageController = GetComponentInParent<StageController>();
            _upgradePlace = GetComponentInChildren<UpgradePlace>();
            _parkPlace = GetComponentInChildren < ParkPlace > ();
            _fuelPlace = GetComponentInChildren<FuelPlace>();
            _exitPlace = GetComponentInChildren<ExitPlace>();
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
        }

        private void OnDisable()
        {
            if (_switcher!=null)
            {
                _switcher.ChangeWorkPlace -= OnChangePlace;
                //_switcher.InitializeWorkPlace -= OnChangePlace;
            }     
        }

        //private void InitializeWorkPlaces()
        //{
        //    List<WorkPlace> _tempPlaces;

        //    _tempPlaces = _switcher.GetWorkPlaces();

        //    for (int i = 0; i < _tempPlaces.Count; i++)
        //    {
        //        _workPlaces.Add(_tempPlaces[i]);
        //    }
        //}

        //private void CreatePlace()
        //{
        //    Character character = Instantiate(_character, _character.transform.position, Quaternion.identity);
        //    character.transform.SetParent(transform);

        //    int number = 0;

        //    for (int i = 0; i < _workPlaces.Count; i++)
        //    {
        //        _inside = _workPlaces[i].GetComponentInChildren<InsideController>();

        //        if (_inside._numberPlace == number)
        //        {
        //            _upgradePoint = _workPlaces[i].GetComponentInChildren<UpgradePlacePoint>().transform.position;
        //            _currentUpgradePlace = Instantiate(_upgradePlace, _upgradePoint, Quaternion.identity);
        //            _currentUpgradePlace.transform.SetParent(transform);

        //            _parkPoint = _workPlaces[i].GetComponentInChildren<ParkPlacePoint>().transform.position;
        //            _currentParkPlace = Instantiate(_parkPlace, _parkPoint, Quaternion.identity);
        //            _currentParkPlace.transform.SetParent(transform);


        //            _fuelPoint = _workPlaces[i].GetComponentInChildren<FuelPlacePoint>().transform.position;
        //            _currentFuelPlace = Instantiate(_fuelPlace, _fuelPoint, Quaternion.identity);
        //            _currentFuelPlace.transform.SetParent(transform);
        //        }
        //    }
        //}

        private void OnChangePlace(GameObject insideControllers)
        {
            Vector3 currentPoint = insideControllers.GetComponentInChildren<UpgradePlacePoint>().transform.position;
            _upgradePlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;
            _parkPlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<FuelPlacePoint>().transform.position;
            _fuelPlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<ExitPlacePoint>().transform.position;
            _exitPlace.transform.position = currentPoint;
        }

        private void OnSetStage(GameObject stage)
        {
            _switcher = stage.GetComponent<WorkPlacesSwitcher>();
            _switcher.ChangeWorkPlace += OnChangePlace;
        }
    }
}
