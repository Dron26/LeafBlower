using System.Collections.Generic;
using UnityEngine;

public class FinishPointCart : MonoBehaviour
{
    [SerializeField] private List<FinishPoint> _points;
    
    private WorkPlacesSwitcher _workPlacesSwitcher;

        private void Awake()
    {
  
        _workPlacesSwitcher = GetComponentInParent<WorkPlacesSwitcher>();
    }

    private void OnEnable()
    {
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }

    private void OnChangeWorkPlace(Vector3 currentPoint,int number)
    {

        transform.position = _points[number].transform.position;
    }

    private void OnDisable()
    {
        _workPlacesSwitcher.ChangeStayPoint += OnChangeWorkPlace;
    }
}