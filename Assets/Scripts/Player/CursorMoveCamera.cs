using UnityEngine;

public class CursorMoveCamera : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject playerCamera;
    [Header("Constraints")]
    public float maxHorizontalRotation;
    public float maxVerticalRotation;
    public bool canMove;
    [Header("Tweaking")]
    public float dampTime;
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
        float targetXRotation = Mathf.Lerp(maxVerticalRotation, -maxVerticalRotation, mouseRelativePosition.y);
        float targetYRotation = Mathf.Lerp(-maxHorizontalRotation, maxHorizontalRotation, mouseRelativePosition.x);
        // Smoothly rotate with damping
        _rotation = Vector3.SmoothDamp(_rotation, new Vector3(targetXRotation, targetYRotation), ref _speed, dampTime);
        playerCamera.transform.localRotation = Quaternion.Euler(_rotation);
    }

    public void Reset()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
