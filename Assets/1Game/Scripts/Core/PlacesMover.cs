using System.Collections.Generic;
using _1Game.Scripts.Empty;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class PlacesMover : MonoBehaviour
    {
        private UpgradePlace _upgradePlace;
        private ParkPlace _parkPlace;
        private FuelPlace _fuelPlace;
        private ExitPlace _exitPlace;
        private CharacterStartPlace _characterPlace;
        private Character _character;
        private StageController _stageController;
        private WorkPlacesSwitcher _switcher;
        private List<WorkPlace> _workPlaces = new List<WorkPlace>();
        private bool _isFirstSet;

        private void Awake()
        {
            _stageController = GetComponentInParent<StageController>();
            _upgradePlace = GetComponentInChildren<UpgradePlace>();
            _parkPlace = GetComponentInChildren<ParkPlace>();
            _fuelPlace = GetComponentInChildren<FuelPlace>();
            _exitPlace = GetComponentInChildren<ExitPlace>();
            _characterPlace = GetComponentInChildren<CharacterStartPlace>();
            _character = GetComponentInChildren<Character>();
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
            _isFirstSet = true;
        }

        private void OnDisable()
        {
            if (_switcher != null)
            {
                _switcher.ChangeWorkPlace -= OnChangePlace;
            }
        }

        private void OnChangePlace(GameObject insideControllers)
        {
            Vector3 currentPoint = insideControllers.GetComponentInChildren<UpgradePlacePoint>().transform.position;
            _upgradePlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<ParkPlacePoint>().transform.position;
            _parkPlace.transform.position = currentPoint;
            currentPoint = insideControllers.GetComponentInChildren<FuelPlacePoint>().transform.position;
            _fuelPlace.transform.position = currentPoint;

            if (_isFirstSet == true)
            {
                SetExitPlace(insideControllers);
            }
        }

        private void OnSetStage(GameObject stage)
        {
            _switcher = stage.GetComponent<WorkPlacesSwitcher>();
            _switcher.ChangeWorkPlace += OnChangePlace;
            SetCharacterPlace();
        }

        private void SetExitPlace(GameObject insideControllers)
        {
            Stage stage = insideControllers.GetComponentInParent<Stage>();
            Vector3 currentPoint = stage.GetComponentInChildren<ExitPlacePoint>().transform.position;
            _exitPlace.transform.position = currentPoint;
            _isFirstSet = false;
        }

        private void SetCharacterPlace()
        {
            _character.transform.position = _characterPlace.transform.position;
        }
    }
}