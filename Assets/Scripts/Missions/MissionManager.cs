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
    private bool _canDayStart;
    private int _progressionIndex;
    private ResourceObjectiveData _currentObjective;
    private int _nbSuccess;
    private bool _canShowWaitingText =  true;
    private ResourceObjectiveData _savedObjective;
    [SerializeField] private float _timeBetweenMissions;
    public static MissionManager Instance;

    public static event Action AnalyticsObjectiveStarted;
    public static event Action<AnalyticsObjectiveData> AnalyticsObjectiveFinished;
    
    [Header("Feedbacks")]
    [SerializeField] private FeedbackSound _feedbackWin;
    [SerializeField] private FeedbackSound _feedbackLose;
    [SerializeField] private FeedbackSound _feedbackMissionReceived;
    [SerializeField] private FeedbackSound _feedbackTryAgain;

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

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _namesToSystems = new Dictionary<SystemName, ResourceSystem>();
        var resourceSystems = GetComponentsInChildren<ResourceSystem>();
        foreach (var resourceSystem in resourceSystems)
        {
            _namesToSystems[resourceSystem.systemName] = resourceSystem;
        }
    }

    public void CheckInMission()
    {
        if (_canDayStart && !_isDayStarted)
        {
            StartDay();
        }
        if (_isObjectiveStarted)
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

    public void EnableDay()
    {
        _canDayStart = true;
        missionText.DisplayText("Pull the cord to start your day");
    }
    
    private void StartDay()
    {
        _isDayStarted = true;
        StartCoroutine(TriggerMissionCo());
        missionText.DisplayText("Awaiting calls...");
    }

    private IEnumerator TriggerMissionCo()
    {
        yield return new WaitForSeconds(_timeBetweenMissions);
        _feedbackMissionReceived.PlayMySound();
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
        missionText.DisplayText(_currentObjective.description);
        missionTimer.StartTimer(_currentObjective.time);
        // Analytics
        AnalyticsObjectiveStarted?.Invoke();
    }
    
    private void CheckObjectiveIsDone()
    {
        bool isSuccess = _namesToSystems[_currentObjective.systemName].IsFixed();
        
        // Rewards
        if (isSuccess)
        {
            missionText.DisplayText(_currentObjective.winMessage);
            _nbSuccess++;
            _feedbackWin.PlayMySound();
        }
        else
        {
            // Don't finish if there is time remaining
            if (missionTimer.IsTimeRemaining())
            {
                if (_canShowWaitingText)
                {
                    _savedObjective = _currentObjective;
                    _canShowWaitingText = false;
                    missionText.DisplayText(_currentObjective.notFinishedMessage);
                    StartCoroutine(ShowTheMissionTextAgain());
                    _feedbackTryAgain.PlayMySound();
                }
                return;
            }
            missionText.DisplayText(_currentObjective.loseMessage);
            _feedbackLose.PlayMySound();
        }
        
        // Analytics
        AnalyticsObjectiveData data = new AnalyticsObjectiveData(_currentObjective.systemName.ToString(), isSuccess);
        AnalyticsObjectiveFinished?.Invoke(data);
        
        // Clear current objective
        missionTimer.StopTimer();
        _isObjectiveStarted = false;
        _progressionIndex++;
        _currentObjective.End(_namesToSystems[_currentObjective.systemName]);
        _currentObjective = null;
        
        // Check ending
        if(_progressionIndex >= objectives.Count)
        {
            missionText.DisplayText($"Your day has\ncome to an end.\nYou can now relax\nin the couch.\nValidate by pulling\nthe cord.");
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

    private IEnumerator ShowTheMissionTextAgain()
    {
        yield return new WaitForSeconds(4f);
        if (_savedObjective == _currentObjective)
        {
            missionText.DisplayText(_currentObjective.description);
        }
        _canShowWaitingText = true;
    }
}
