using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Cord : Draggable
{
    public Transform highPoint, lowPoint;
    private float _progress = 1f;
    [SerializeField] private float _speedReturning;
    [SerializeField] private float _triggerThreshold;
    private bool _isReturning;
    private bool _canTrigger;
    public UnityEvent OnTrigger;
    
    
    protected override void Interact()
    {
        base.Interact();
        _isReturning = false;
        _canTrigger = true;
        feedbackSound?.PlayMySound();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        _isReturning = true;
    }

    protected override void Update()
    {
        base.Update();
        
        // Automatically go up
        if(_isReturning)
            _progress += _speedReturning * Time.deltaTime;
        
        // Move according to progress
        _progress = Mathf.Clamp01(_progress);
        
        // Move between highPoint and LowPoint
        float newY = Mathf.Lerp(lowPoint.position.y, highPoint.position.y, _progress);
        
        // Apply new position
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }

    protected override void Drag(Vector2 delta)
    {
        // Only take into account vertical movement (Y axis)
        _progress += delta.y/Screen.height;
        if (_progress <= _triggerThreshold && _canTrigger)
        {
            OnTrigger?.Invoke();
            _canTrigger = false;
        }
    }
}