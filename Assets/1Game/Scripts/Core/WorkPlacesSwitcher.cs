using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Service;

namespace Core
{
public class WorkPlacesSwitcher : MonoBehaviour
{
    private List<WorkPlace> _workPlaces;
    [HideInInspector] private List<InsideController> _insideControllers;
    [HideInInspector] private List<WorkPlaceImageChanger> _workPlaceImageChangers;
    [HideInInspector] private List<Vector3> _stayPoint;

    public UnityAction<Vector3,int> ChangeStayPoint;
    private int firstStart = 1;

    private void Awake()
    {
        foreach (WorkPlace place in transform.GetComponentsInChildren<WorkPlace>())
        {         
                _workPlaces.Add(place.GetComponent<WorkPlace>());
        }

           _insideControllers = new List<InsideController>();
        _workPlaceImageChangers = new List<WorkPlaceImageChanger>();
        _stayPoint = new List<Vector3>();

        for (int i = 0; i < _workPlaces.Count; i++)
        {
            InsideController insideController = new InsideController();
            WorkPlaceImageChanger workPlaceImageChange = new WorkPlaceImageChanger();
            Vector3 stayPoint = new Vector3();

            _insideControllers.Add(insideController = _workPlaces[i].GetComponentInChildren<InsideController>());
            _workPlaceImageChangers.Add(workPlaceImageChange = _workPlaces[i].GetComponent<WorkPlaceImageChanger>());
            _stayPoint.Add(stayPoint= _workPlaces[i].GetComponentInChildren<ParkPlace>().GetComponentInChildren<StayPoint>().transform.position);
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < _workPlaces.Count; i++)
        {
            _insideControllers[i].CharacterInside += OnCharacterInside;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _workPlaces.Count; i++)
        {
            FadeImage(false, i);
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < _workPlaces.Count; i++)
        {
            _insideControllers[i].CharacterInside -= OnCharacterInside;
        }
    }

    private void OnCharacterInside(bool isInside, int numberPlace)
    {
        FadeImage(isInside, numberPlace);

        if (isInside==true& firstStart==0)
        {
            ChangeStayPoint?.Invoke(_stayPoint[numberPlace], numberPlace);
        }

        firstStart=0;
    }

    private void FadeImage(bool isInside, int numberPlace)
    {
        float volume;

        if (isInside == true)
        {
            volume = 255f;
        }
        else
        {
            volume = 0;
        }

        _workPlaceImageChangers[numberPlace].ChangeImage(volume);
    }

    public List<WorkPlace> GetWorkPlaces()
    {
        List<WorkPlace> tempPlaces = new List<WorkPlace>();

        foreach (WorkPlace place in _workPlaces)
        {
            tempPlaces.Add(place);
        }

        return tempPlaces;
    }
}
}
