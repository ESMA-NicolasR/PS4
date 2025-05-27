using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public MissionTimer missionTimer;
    public MissionGiver missionGiver;
    public List<ResourceObjectiveData> objectives;
    private Dictionary<SystemName, ResourceSystem> _namesToSystems;
    public MissionText missionText;
    private bool _isObjectiveStarted;
    private bool _isDayStarted;
    private int _progressionIndex;
    private ResourceObjectiveData _currentObjective;
    private int _nbSuccess;
    [SerializeField] private float _timeBetweenMissions;
    private int _totalHumans, _totalMoney;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioClip _winSound;
    private SoundManager _soundManager;

    public static event Action AnalyticsObjectiveStarted;
    public static event Action<AnalyticsObjectiveData> AnalyticsObjectiveFinished;

    private void OnEnable()
    {
        MissionTimer.OnMissionTimerExpire += OnMissionTimerExpire;
        MissionButton.OnMissionAccepted += StartMission;
    }

    private void OnDisable()
    {
        MissionTimer.OnMissionTimerExpire -= OnMissionTimerExpire;
        MissionButton.OnMissionAccepted -= StartMission;
    }

    void Start()
    {
        _soundManager = GetComponent<SoundManager>();
        _namesToSystems = new Dictionary<SystemName, ResourceSystem>();
        var resourceSystems = GetComponentsInChildren<ResourceSystem>();
        foreach (var resourceSystem in resourceSystems)
        {
            _namesToSystems[resourceSystem.systemName] = resourceSystem;
        }
    }

    public void CheckInMission()
    {
        if (!_isDayStarted)
        {
            StartDay();
        }
        else if (_isObjectiveStarted)
        {
            CheckObjectiveIsDone();
        }
        else if (_progressionIndex >= objectives.Count)
        {
            FindFirstObjectByType<LastStation>().GoToLastStation();
        }
        else
        {
            Debug.Log("Just wait");
        }
    }

    private void StartDay()
    {
        _isDayStarted = true;
        StartCoroutine(TriggerMissionCo());
        missionText.DisplayText("Awaiting orders...");
    }

    private IEnumerator TriggerMissionCo()
    {
        yield return new WaitForSeconds(_timeBetweenMissions);
        missionGiver.StartSignal();
    }
    
    private void StartMission()
    {
        // Read the objective
        _currentObjective = objectives[_progressionIndex];
        // Break the system accordingly
        _currentObjective.BreakSystem(_namesToSystems[_currentObjective.systemName]);
        // Start the mission
        _isObjectiveStarted = true;
        missionText.DisplayText(_currentObjective.description +" Pull the cord when it's done.");
        missionTimer.StartTimer(_currentObjective.time);
        // Analytics
        AnalyticsObjectiveStarted?.Invoke();
    }
    
    private void CheckObjectiveIsDone()
    {
        bool isSuccess = _namesToSystems[_currentObjective.systemName].IsFixed();
        
        // Analytics
        AnalyticsObjectiveData data = new AnalyticsObjectiveData(_currentObjective.systemName.ToString(), isSuccess);
        AnalyticsObjectiveFinished?.Invoke(data);
        
        // Rewards
        if (isSuccess)
        {
            missionText.DisplayText(_currentObjective.winMessage +" Awaiting new orders...");
            _nbSuccess++;
            Debug.Log($"Mission {_currentObjective.name} won");
            _soundManager.PlaySound(_winSound);
        }
        else
        {
            missionText.DisplayText(_currentObjective.loseMessage +" Awaiting new orders...");
            Debug.Log($"Mission {_currentObjective.name} failed");
            _soundManager.PlaySound(_failSound);
        }
        
        // Clear current objective
        missionTimer.StopTimer();
        _isObjectiveStarted = false;
        _progressionIndex++;
        _currentObjective.End(_namesToSystems[_currentObjective.systemName]);
        _currentObjective = null;
        
        // Check ending
        if(_progressionIndex >= objectives.Count)
        {
            missionText.DisplayText($"Your day has\ncome to an end.\nYou can now relax\nin the couch.\nValidate by pulling\nthe handle. \n");
        }
        else
        { // Next objective
            StartCoroutine(TriggerMissionCo());
        }
    }

    public void FinishGame()
    {
        AnalyticsManager.Instance.WriteAnalytics();
        SceneManager.LoadScene("EndingScene");
    }

    private void OnMissionTimerExpire()
    {
        missionTimer.StopTimer();
        if (_isObjectiveStarted)
        {
            CheckInMission();
        }
    }
}
