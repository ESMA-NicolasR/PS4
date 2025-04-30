using UnityEngine;

public class CursorMoveCamera : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject playerCamera;
    public GameObject playerHead;
    [Header("Constraints")]
    public float maxHorizontalRotation;
    public float maxVerticalRotation;
    public bool canMove;
    [Header("Tweaking")]
    public float dampTime;
    public AnimationCurve deadZoneCurve;
    // Internal
    private Vector3 _rotation;
    private Vector3 _speed;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (!canMove) return;
        // Deduce target angle from mouse position
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseRelativePosition = new Vector2(mouseScreenPosition.x / Screen.width, mouseScreenPosition.y/Screen.height);
        var distance = Vector2.Distance(mouseRelativePosition, new Vector2(0.5f, 0.5f)); // (0.5, 0.5) is middle of the screen
        var deadZoneFactor = deadZoneCurve.Evaluate(distance / 0.5f); // 0.5 is the max distance to middle of screen
        var targetXRotation = Mathf.Lerp(maxVerticalRotation, -maxVerticalRotation, mouseRelativePosition.y)*deadZoneFactor;
        var targetYRotation = Mathf.Lerp(-maxHorizontalRotation, maxHorizontalRotation, mouseRelativePosition.x)*deadZoneFactor;
        
        // Smoothly rotate with damping
        _rotation = Vector3.SmoothDamp(_rotation, new Vector3(targetXRotation, targetYRotation), ref _speed, dampTime);
        playerHead.transform.localRotation = Quaternion.Euler(_rotation);
    }

    public void ResetCamera()
    {
        playerHead.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rotation = Vector3.zero;
        _speed = Vector3.zero;
    }
}
