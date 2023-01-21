using Agava.YandexGames;
using System;
using System.Collections.Generic;
using System.Linq;
using _1Game.Scripts.Core;
using _1Game.Scripts.UI;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardRecord _template;
    [SerializeField] private Transform _container;
    [SerializeField] private int _amountRecords;
[SerializeField] private ExitPanel _exitPanel;
[SerializeField] private Wallet _wallet;

private Panel _panel;
    public int AmountRecords => _amountRecords;

    private List<LeaderboardEntryResponse> _entries = new List<LeaderboardEntryResponse>();
    private readonly List<LeaderboardRecord> _records = new List<LeaderboardRecord>();
    private LeaderboardEntryResponse _player;

    private void Start()
    {
        UpdateData();
        _panel=GetComponentInChildren<Panel>();
        _panel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _exitPanel.SetNextLevel += UpdateData;
    }
    
    private void OnDisable()
    {
        _exitPanel.SetNextLevel -= UpdateData;
    }


    public void Init(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        for (int i = 0; i < _amountRecords; i++)
        {
            if (leaderboardGetEntriesResponse.entries.Length <= i)
                break;

            LeaderboardRecord record = Instantiate(_template, _container);
            LeaderboardEntryResponse entity = leaderboardGetEntriesResponse.entries[i];
            record.UpdateData(entity);
            _records.Add(record);
            _entries.Add(entity);

            if (leaderboardGetEntriesResponse.userRank == entity.rank)
                _player = entity;
        }
    }

    private void UpdateData()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _player.score += Convert.ToInt32(_wallet.Money);

        Agava.YandexGames.Leaderboard.SetScore(LeaderboardConstants.Name, _player.score);

        UpdateViews();
    }

    private void UpdateViews()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        _entries = _entries.OrderByDescending(entry => entry.score).ToList();

        for (int i = 0; i < _records.Count; i++)
            _records[i].UpdateData(_entries[i], i + 1);
    }
}
