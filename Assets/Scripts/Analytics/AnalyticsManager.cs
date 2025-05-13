using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    private List<AnalyticsObjectiveData> _dataList;
    private string _savePath;

    private float _timeGameStarted;
    private float _timeObjectiveStarted;
    private int _nbClicksBeforeObjective;
    private float _timeDraggingBeforeObjective;
    private int _nbMovementsBeforeObjective;
    private float _timeTravelingBeforeObjective;
    
    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        _dataList = new List<AnalyticsObjectiveData>();
        _savePath = $"{Application.persistentDataPath}/Playtest-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";
    }

    private void OnEnable()
    {
        MissionGiver.AnalyticsObjectiveStarted += OnObjectiveStarted;
        MissionGiver.AnalyticsObjectiveFinished += OnObjectiveFinished;
    }

    private void OnDisable()
    {
        MissionGiver.AnalyticsObjectiveStarted -= OnObjectiveStarted;
        MissionGiver.AnalyticsObjectiveFinished -= OnObjectiveFinished;
    }

    private void OnObjectiveStarted()
    {
        _timeObjectiveStarted = Time.time;
        _nbClicksBeforeObjective = Clickable.AnalyticsTotalClicks;
        _timeDraggingBeforeObjective = Draggable.AnalyticsTotalTimeDragging;
        _nbMovementsBeforeObjective = PlayerTravel.AnalyticsTotalTravels;
        _timeTravelingBeforeObjective = PlayerTravel.AnalyticsTotalTimeTraveling;
    }

    private void OnObjectiveFinished(AnalyticsObjectiveData data)
    {
        data.timeToComplete = Time.time - _timeObjectiveStarted;
        data.nbClicks = Clickable.AnalyticsTotalClicks - _nbClicksBeforeObjective;
        data.timeSpentDragging = Draggable.AnalyticsTotalTimeDragging - _timeDraggingBeforeObjective;
        data.nbTravels = PlayerTravel.AnalyticsTotalTravels - _nbMovementsBeforeObjective;
        data.timeSpentTraveling = PlayerTravel.AnalyticsTotalTimeTraveling - _timeTravelingBeforeObjective;
        _dataList.Add(data);
    }

    public void WriteAnalytics()
    {
        // Generate overall data
        AnalyticsObjectiveData totalData = new AnalyticsObjectiveData("Total", true);
        totalData.timeToComplete = Time.time - _timeGameStarted;
        totalData.nbClicks = Clickable.AnalyticsTotalClicks;
        totalData.timeSpentDragging = Draggable.AnalyticsTotalTimeDragging;
        totalData.nbTravels = PlayerTravel.AnalyticsTotalTravels;
        totalData.timeSpentTraveling = PlayerTravel.AnalyticsTotalTimeTraveling;
        // Add it to other data
        _dataList.Add(totalData);
        // Write data
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("systemName;success;timeToComplete;nbClicks;timeSpentDragging;nbTravels;timeSpentTraveling");
        foreach (var data in _dataList)
        {
            sb.AppendLine($"{data.systemName};{data.success};{data.timeToComplete:F2};{data.nbClicks:N0};{data.timeSpentDragging:00.00};{data.nbTravels:N0};{data.timeSpentTraveling:00.00}");
        }
        File.AppendAllText(_savePath, sb.ToString());
        Debug.Log($"Analytics written to {_savePath}");
    }
}

[Serializable]
public struct AnalyticsObjectiveData
{
    public string systemName;
    public bool success;
    public float timeToComplete;
    public int nbClicks;
    public float timeSpentDragging;
    public int nbTravels;
    public float timeSpentTraveling;

    public AnalyticsObjectiveData(string _systemName, bool _success)
    {
        systemName = _systemName;
        success = _success;
        timeToComplete = 0f;
        nbClicks = 0;
        timeSpentDragging = 0f;
        nbTravels = 0;
        timeSpentTraveling = 0f;
    }
}