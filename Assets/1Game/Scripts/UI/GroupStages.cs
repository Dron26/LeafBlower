using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class GroupStages : MonoBehaviour
{
     private List<UIStage> _stages=new();

    private void Awake()
    {
        InitializeStages();
    }

    private List<UIStage> GetStages()
    {

        List<UIStage> _tempStage = new();

        for (int i = 0; i < _stages.Count; i++)
        {
            _tempStage.Add(_stages[i]);
        }

        return _tempStage;
    }

    private void InitializeStages()
    {
        foreach (UIStage stage in transform.GetComponentsInChildren<UIStage>())
        {
            _stages.Add(stage);
        }
    }
}
}
