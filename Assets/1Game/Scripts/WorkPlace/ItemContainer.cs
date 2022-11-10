using UnityEngine;
using UnityEngine.Events;
namespace Service

{
public class ItemContainer : MonoBehaviour
{
    private AirZone _airZone;
    private WorkPlace _workPlace;
    private FuelChanger fuelChanger;
    private bool isThereFuel;

    private void Awake()
    {
        isThereFuel = true;
        fuelChanger = GetComponentInParent<FuelChanger>();
        _airZone = GetComponentInChildren<AirZone>();
        _airZone.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        fuelChanger.ChangeFuel += OnChangeFuel;
    }

    private void OnDisable()
    {
        fuelChanger.ChangeFuel -= OnChangeFuel;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WorkPlace workPlace)& isThereFuel==true)
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
        if (fuelLevel==0)
        {
            isThereFuel = false;
            _airZone.gameObject.SetActive(false);
        }
        else
        {
            isThereFuel = true;
        }
    }
}
}
