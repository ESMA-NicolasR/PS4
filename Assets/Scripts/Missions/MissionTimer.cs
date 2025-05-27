using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    public GameObject timerBar;
    public Image timerBarFG;
    public TextMeshProUGUI timerText;

    public FeedbackSound feedbackHalfTime;
    public FeedbackSound feedbackLittleTime;
    [Tooltip("How much time left to play a feedback")]
    public float littleTime;

    private float _timeLeft;
    private float _timeMax;
    private bool _isTicking;
    private bool _hasTriggeredHalfTime;
    private bool _hasTriggeredLittleTime;

    public static event Action OnMissionTimerExpire;
    
    public void StartTimer(float time)
    {
        _timeLeft = _timeMax = time;
        _isTicking = true;
        timerBar.SetActive(true);
        _hasTriggeredHalfTime = false;
        _hasTriggeredLittleTime = false;
    }

    private void Start()
    {
        timerBar.SetActive(false);
    }

    private void Update()
    {
        if (_isTicking)
        {
            _timeLeft -= Time.deltaTime;
            UpdateDisplay();
            if (!_hasTriggeredHalfTime && _timeLeft <= _timeMax / 2)
            {
                feedbackHalfTime.PlayMySound();
                _hasTriggeredHalfTime = true;
            }

            if (!_hasTriggeredLittleTime && _timeLeft <= littleTime)
            {
                feedbackLittleTime.PlayMySound();
                _hasTriggeredLittleTime = true;
            }
            if (_timeLeft <= 0)
            {
                ExpireTimer();
            }
        }
    }

    private void UpdateDisplay()
    {
        // Bar foreground
        var ratio = _timeLeft / _timeMax;
        timerBarFG.fillAmount = ratio;
        timerBarFG.color = Color.Lerp(Color.red, Color.white, Mathf.Sqrt(ratio)); // Sqrt so it takes longer to display red
        // Timer text
        var minutes = Mathf.FloorToInt(_timeLeft / 60);
        var reprTime = minutes > 0 ? $"{minutes}m " : "";
        reprTime += $"{_timeLeft % 60:00}s";
        timerText.text = $"Time left : {reprTime}";
    }

    private void ExpireTimer()
    {
        OnMissionTimerExpire?.Invoke();
    }

    public void StopTimer()
    {
        _isTicking = false;
        timerBar.SetActive(false);
        _hasTriggeredHalfTime = true;
        _hasTriggeredLittleTime = true;
    }

    public bool IsTimeRemaining()
    {
        return _isTicking && _timeLeft > 0;
    }
}
