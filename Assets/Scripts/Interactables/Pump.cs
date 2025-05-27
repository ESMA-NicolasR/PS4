using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Pump : Draggable
{
    public Transform lowEnd, highEnd;
    public GameObject pump;
    public float amplitudeWeight;
    public float dragDownMultiplier;
    public int valueStrength;
    [SerializeField] private float _timeCheckContinueSound;

    protected override CursorType cursorType => CursorType.UpDown;
    
    public float progress;
    private float _lastProgress;
    private float _accumulatedScore;
    private float _time;
    private bool _isPlayingSound = false;
    
    public ResourceSystemNumber resourceSystem;

    protected override void Start()
    {
        pump.transform.position = lowEnd.position;
        progress = 0f;
    }

    protected override void Drag(Vector2 delta)
    {
        _lastProgress = progress;
        if (delta.y < 0)
        {
            delta *= dragDownMultiplier;
        }
        progress = Mathf.Clamp01(progress + delta.y / Screen.height);
        UpdateScore(progress - _lastProgress);
        pump.transform.position = Vector3.Lerp(lowEnd.position, highEnd.position, progress);
    }

    private void UpdateScore(float delta)
    {
        // When we pump down, increase the score
        if (delta < 0)
        {
            _accumulatedScore -= delta * amplitudeWeight;
            PlayNoise();
        }
        // If we pumped enough, use score to change system value
        if (_accumulatedScore >= 1.0f)
        {
            var integerPart = Mathf.FloorToInt(_accumulatedScore);
            resourceSystem.ChangeValue(valueStrength*integerPart);
            // Keep the fractional part
            _accumulatedScore -= integerPart;
        }
    }
    private void PlayNoise()
    {
        _time = Time.time;
        if (!_isPlayingSound)
        {
            _isPlayingSound = true;
            feedbackSound?.PlayMySound();
            StartCoroutine(CheckIfSoundIsStillPlaying());
        }
    }

    private IEnumerator CheckIfSoundIsStillPlaying()
    {
        while (_isPlayingSound)
        {
            yield return new WaitForSeconds(_timeCheckContinueSound);
            if (Time.time > _time+_timeCheckContinueSound)
            {
                _isPlayingSound = false;
                feedbackSound?.StopAllSounds();
            }
        }
    }
}
