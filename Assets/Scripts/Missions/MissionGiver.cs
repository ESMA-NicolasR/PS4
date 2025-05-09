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
    private float _timeofStart;

    private void Start()
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
        _namesToSystems[_currentObjective.systemName].Break(_currentObjective.targetValue, _currentObjective.breakValue);
        // Start the mission
        _isStarted = true;
        _timeofStart = Time.time;
        text.text = _currentObjective.description +"\nPush the button when it's done.";
    }
    
    private void CheckObjectiveIsDone()
    {
        if (_namesToSystems[_currentObjective.systemName].IsFixed())
        {
            text.text = _currentObjective.winMessage;
            _nbSuccess++;
        }
        else
        {
            text.text = _currentObjective.loseMessage;
        }
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        Debug.Log($"Mission finished in {(Time.time - _timeofStart):F2} seconds");
        if(_progressionIndex >= objectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbSuccess/objectives.Count):F2}%, thanks !";
        }
    }

    private void FinishGame()
    {
        SceneManager.LoadScene("EndingScene");
    }
    
}
