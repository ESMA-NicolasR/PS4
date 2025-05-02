using UnityEngine;

public class CursorMoveCamera : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject playerHead;
    [Header("Constraints")]
    public float maxHorizontalRotation;
    public float maxVerticalRotation;
    public bool canMove;
    [Header("Tweaking")]
    [Tooltip("Damp time when looking around")]
    public float dampTime;
    [Tooltip("How much we move depending on the distance from center of screen")]
    public AnimationCurve deadZoneCurve;
    // Internal animation variables
    private Vector3 _rotation;
    private Vector3 _speed;
    // Internal variables
    private Vector3 _currentMousePosition;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (canMove)
            _currentMousePosition = Input.mousePosition;
        // Deduce target angle from mouse position
        Vector2 mouseRelativePosition = new Vector2(_currentMousePosition.x / Screen.width, _currentMousePosition.y/Screen.height);
        var distance = Vector2.Distance(mouseRelativePosition, new Vector2(0.5f, 0.5f)); // (0.5, 0.5) is middle of the screen
        var deadZoneFactor = deadZoneCurve.Evaluate(distance / 0.5f); // 0.5 is the max distance to middle of screen
        var targetXRotation = Mathf.Lerp(maxVerticalRotation, -maxVerticalRotation, mouseRelativePosition.y)*deadZoneFactor;
        var targetYRotation = Mathf.Lerp(-maxHorizontalRotation, maxHorizontalRotation, mouseRelativePosition.x)*deadZoneFactor;
        
        // Smoothly rotate with damping
        RotateTowards(new Vector3(targetXRotation, targetYRotation));
    }

    private void RotateTowards(Vector3 target)
    {
        // Smoothly rotate with damping
        _rotation = Vector3.SmoothDamp(_rotation, target, ref _speed, dampTime);
        playerHead.transform.localRotation = Quaternion.Euler(_rotation);
    }

    public void ResetCamera()
    {
        playerHead.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rotation = Vector3.zero;
        _speed = Vector3.zero;
    }
}
