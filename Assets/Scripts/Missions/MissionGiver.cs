using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionGiver : MonoBehaviour
{
    public List<ResourceObjectiveData> objectives;
    private Dictionary<SystemName, ResourceSystem> _namesToSystems;
    public List<ResourceSystem> resourceSystems;
    public TextMeshPro text;
    private bool _isStarted;
    private int _progressionIndex;
    private ResourceObjectiveData _currentObjective;
    private int _nbSuccess;

    public static event Action AnalyticsObjectiveStarted;
    public static event Action<AnalyticsObjectiveData> AnalyticsObjectiveFinished;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _namesToSystems = new Dictionary<SystemName, ResourceSystem>();
        foreach (var resourceSystem in resourceSystems)
        {
            _namesToSystems[resourceSystem.systemName] = resourceSystem;
        }
    }

    public void CheckInMission()
    {
        if (_isStarted)
        {
            CheckObjectiveIsDone();
        }
        else if (_progressionIndex >= objectives.Count)
        {
            FinishGame();
        }
        else
        {
            StartMission();
        }
    }
    
    private void StartMission()
    {
        // Read the objective
        _currentObjective = objectives[_progressionIndex];
        // Break the system accordingly
        _currentObjective.BreakSystem(_namesToSystems[_currentObjective.systemName]);
        // Start the mission
        _isStarted = true;
        text.text = _currentObjective.GetDescription() +". Push the button when it's done.";
        AnalyticsObjectiveStarted?.Invoke();
    }
    
    private void CheckObjectiveIsDone()
    {
        bool isSuccess = _currentObjective.CheckIsCompleted();
        
        // Analytics
        AnalyticsObjectiveData data = new AnalyticsObjectiveData(_currentObjective._resourceSystem.resourceName, isSuccess);
        AnalyticsObjectiveFinished?.Invoke(data);
        
        // Next objective
        if (isSuccess)
        {
            text.text = _currentObjective.winMessage;
            _nbSuccess++;
            Debug.Log($"Mission {_currentObjective.name} won");
        }
        else
        {
            text.text = _currentObjective.loseMessage;
            Debug.Log($"Mission {_currentObjective.name} failed");
        }
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        
        // Check ending
        if(_progressionIndex >= possibleObjectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbSuccess/objectives.Count):F2}%, thanks !";
        }
    }

    private void FinishGame()
    {
        AnalyticsManager.Instance.WriteAnalytics();
        SceneManager.LoadScene("EndingScene");
    }
}
