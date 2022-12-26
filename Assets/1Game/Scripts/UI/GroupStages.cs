using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Empty;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI
{
public class GroupStages : MonoBehaviour
{
      private ChangerPanel _changerPanel;
     private StageData _stageData;
     
     private List<UIStage> _uIStages=new();
     private int _numberGroup;
     private int _countStars;

     public UnityAction <int> EndStage;
     private void OnEnable()
     {
     }

     private void Start()
    {
        InitializeStages();
    }

    private List<UIStage> GetStages()
    {
        return _uIStages.ToList();
    }

    public void Initialize(ChangerPanel changerPanel,StageData stageData,int number)
    {
        _changerPanel = changerPanel;
        _stageData = stageData;
        _numberGroup = number;
    }
    private void InitializeStages()
    {
        foreach (UIStage stage in transform.GetComponentsInChildren<UIStage>())
        {
            _uIStages.Add(stage);
        }
        
        InitializeStage();
        
        SetStars();
    }

    private void InitializeStage()
    {
        for (int i = 0; i < _uIStages.Count; i++)
        {
            _uIStages[i].Initialize(i,_numberGroup);
        }
    }
    public void SetStars()
    {
        for (int i = 0; i < _uIStages.Count; i++)
        {
            _countStars = _stageData.GetStars(i, _numberGroup);
            _uIStages[i].GetComponentInChildren<StarGroup>().SetStars(_countStars);
            UnLockStage(i, _countStars);
        }
    }

    private void UnLockStage(int numberUIStage,int countStars)
    {
        int countAllStars = 3;

        if (_countStars == countAllStars & numberUIStage + 1 < _uIStages.Count)
        {
            _uIStages[numberUIStage+1].SetLock(false);
        }
        else if (numberUIStage + 1 == _uIStages.Count)
        {
            EndStage?.Invoke(_numberGroup);
        } 
    }
}
}
