using UnityEngine;

public class FinishPointCart : MonoBehaviour
{
    private WorkPlacesSwitcher _workPlacesSwitcher;

    private float _distance;
        private void Awake()
    {
        _workPlacesSwitcher = GetComponentInParent<WorkPlacesSwitcher>();
    }

    private void OnEnable()
    {
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }

    private void OnChangeWorkPlace(Vector3 currentPoint)
    {

        _distance=Vector3.Distance(currentPoint,transform.position);

        transform.position = new Vector3(transform.position.x + _distance, transform.position.y,transform.position.z );
    }

    private void OnDisable()
    {
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }
}