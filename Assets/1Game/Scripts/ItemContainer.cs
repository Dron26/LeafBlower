using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    private AirZone _airZone;
    private WorkPlace _workPlace;



    private void Awake()
    {
        _airZone = GetComponentInChildren<AirZone>();
        _airZone.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WorkPlace workPlace))
        {
            _airZone.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExite(Collider other)
    {
        if (other.TryGetComponent(out WorkPlace workPlace))
        {
            _airZone.gameObject.SetActive(false);
        }
    }




}
