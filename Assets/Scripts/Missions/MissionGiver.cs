using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionGiver : MonoBehaviour
{
    public List<ResourceObjective> possibleObjectives;
    public TextMeshPro text;
    private bool _isStarted;
    private int _progressionIndex;
    private ResourceObjective _currentObjective;
    private int _nbSuccess;

    public static event Action AnalyticsObjectiveStarted;
    public static event Action<AnalyticsObjectiveData> AnalyticsObjectiveFinished;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isStarted = false;
        _progressionIndex = 0;
    }

    public void StartMission()
    {
        if (_isStarted)
        {
            CheckObjectiveIsDone();
        }
        else if (_progressionIndex >= possibleObjectives.Count)
        {
            FinishGame();
        }
        else
        {
            StartObjective();
        }
    }

    private void StartObjective()
    {
        _currentObjective = possibleObjectives[_progressionIndex];
        _currentObjective.CreateObjective();
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

            text.text = $"Mission completed, congratulations ! Press the button to get a new one.";
            _nbSuccess++;
        }
        else
        {
            text.text = $"Mission failed... Press the button to get a new one.";
        }
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        
        // Check ending
        if(_progressionIndex >= possibleObjectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbSuccess/possibleObjectives.Count):F2}%, thanks !.";
        }
    }

    private void FinishGame()
    {
        AnalyticsManager.Instance.WriteAnalytics();
        SceneManager.LoadScene("EndingScene");
    }
    
}
