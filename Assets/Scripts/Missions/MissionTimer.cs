using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
    public GameObject timerBar;
    public Image timerBarFG;
    public TextMeshProUGUI timerText;

    private float _timeLeft;
    private float _timeMax;
    private bool _isTicking;

    public static event Action OnMissionTimerExpire;
    
    public void StartTimer(float time)
    {
        _timeLeft = _timeMax = time;
        _isTicking = true;
        timerBar.SetActive(true);
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
            if (_timeLeft <= 0)
            {
                ExpireTimer();
            }
        }
    }

    private void UpdateDisplay()
    {
        timerBarFG.fillAmount = _timeLeft / _timeMax;
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
    }
}
