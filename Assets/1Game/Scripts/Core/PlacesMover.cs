using UnityEngine;
using Service;
namespace Core
{
public class PlacesMover : MonoBehaviour
{
        [SerializeField] private UpgradePlace _upgradePlace;
        [SerializeField] private ParkPlace _parkPlace;
        [SerializeField] private FuelChanger _fuelChanger;


        private StageController _stageController;
        private WorkPlace _workPlace;

        private ParkPlacePoint _parkPoint;
        private UpgradePlacePoint _upgradePoint;
        private FuelPlacePoint _fuelPoint;
        private InsideController _inside;


        private void Awake()
        {
            _stageController=GetComponent<StageController>();
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
        }

        private void OnDisable()
        {
            _stageController.SetStage -= OnSetStage;
        }

        private void OnSetStage(GameObject stage)
        {
            stage.
        }

    }
}
