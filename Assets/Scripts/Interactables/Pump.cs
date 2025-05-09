using UnityEngine;
using UnityEngine.Serialization;

public class Pump : Draggable
{
    public Transform lowEnd, highEnd;
    public GameObject pump;
    public float amplitudeWeight;
    public float dragDownMultiplier;
    public int valueStrength;

    private float _progress;
    private float _lastProgress;
    private float _accumulatedScore;
    
    public ResourceSystemInteger resourceSystem;

    protected override void Start()
    {
        pump.transform.position = lowEnd.position;
        _progress = 0f;
    }

    protected override void Drag(Vector2 delta)
    {
        //base.Drag(delta);
        _lastProgress = _progress;
        if (delta.y < 0)
        {
            delta *= dragDownMultiplier;
        }
        _progress = Mathf.Clamp01(_progress + delta.y / Screen.height);
        UpdateScore(_progress - _lastProgress);
        pump.transform.position = Vector3.Lerp(lowEnd.position, highEnd.position, _progress);
    }

    private void UpdateScore(float delta)
    {
        // When we pump down, increase the score
        if (delta < 0)
        {
            _accumulatedScore -= delta * amplitudeWeight;
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
}
