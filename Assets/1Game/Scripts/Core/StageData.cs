using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Empty;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(StageController))]
public class StageData : MonoBehaviour
{
    [SerializeField] private StarPanel _starPanel;
    private List<Dictionary<int, int>> _starGroup = new List<Dictionary<int, int>>();
    private Dictionary<int, int> _firstStarGroup = new Dictionary<int, int>();
    private Dictionary<int, int> _secondStarGroup = new Dictionary<int, int>();

    public int CountFirstStages=>_countFirstStages;
    public int CountSecondStages=>_countSecondStages;
    public int CountStars => _countStars;
    public int NumberStage=>_numberStage;
    
    private StageController _stageController;
    private ExitPanelUI _exitPanel;

    private int _countFirstStages;
    private int _countSecondStages;

    private int _countStars;
    private int _numberStage;
    private int _numberGroup;

    private void Awake()
    {
        _stageController = GetComponent<StageController>();
        _exitPanel = _stageController.GetComponent<StageController>()._exitPanel;
    }

    private void OnEnable()
    {
        _exitPanel.SetNextLevel += OnSetNextLevel;
    }

    private void Start()
    {
        _countFirstStages = _stageController.CountFirstStages;
        _countSecondStages = _stageController.CountSecondStages;
        InitializeStarGroup(_countFirstStages, ref _firstStarGroup);
        InitializeStarGroup(_countSecondStages, ref _secondStarGroup);

        _starGroup.Add(_firstStarGroup);
        _starGroup.Add(_secondStarGroup);
    }

    private void OnDisable()
    {
        _exitPanel.SetNextLevel -= OnSetNextLevel;
    }

    private void OnSetNextLevel()
    {
        _numberStage = _stageController.SelectNumberStage;
        _numberGroup = _stageController.SelectNumberGroup;
        _countStars = _starPanel.CountStars;
        SetStars(_numberStage, _numberGroup, _countStars);
    }

    private void InitializeStarGroup(int countStages, ref Dictionary<int, int> starGroup)
    {
        for (int i = 0; i < countStages; i++)
        {
            starGroup.Add(i, 0);
        }
    }

    private void SetStars(int numberStage, int numberGroup, int countStars)
    {
        _starGroup[numberGroup][numberStage] = countStars;
    }

    public int GetStars(int numberStage, int numberGroup)
    {
        int countStars = _starGroup[numberGroup][numberStage];

        return countStars;
    }
}