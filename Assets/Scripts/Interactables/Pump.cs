using UnityEngine;

public class Pump : Draggable
{
    public Transform lowEnd, highEnd;
    public GameObject pump;
    public float amplitudeWeight;
    public float increasedDragOnDown;
    public int valueStrength;

    private float _progress;
    private float _lastProgress;
    private float _lastDirection;
    private float _currentDirection;
    private float _accumulatedScore;
    
    public ResourceHandle resourceHandle;

    protected override void Start()
    {
        pump.transform.position = lowEnd.position;
        _progress = 0f;
    }

    protected override void Drag(Vector2 delta)
    {
        //base.Drag(delta);
        _lastProgress = _progress;
        _progress = Mathf.Clamp01(_progress + delta.y / Screen.height);
        UpdateScore(_progress - _lastProgress);
        pump.transform.position = Vector3.Lerp(lowEnd.position, highEnd.position, _progress);
    }

    private void UpdateScore(float delta)
    {
        if (delta < 0)
        {
            _accumulatedScore -= delta * amplitudeWeight;
        }
        if (_accumulatedScore >= 1.0f)
        {
            resourceHandle.ChangeValue(valueStrength);
            _accumulatedScore -= 1.0f;
        }
        Debug.Log(_accumulatedScore);
    }
}
