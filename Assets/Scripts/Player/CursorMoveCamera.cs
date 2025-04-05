using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class CursorMoveCamera : MonoBehaviour
{
    public GameObject playerCamera;

    public float maxHorizontalRotation;
    public float maxVerticalRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }

    void Update()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseRelativePosition = new Vector2(mouseScreenPosition.x / Screen.width, mouseScreenPosition.y/Screen.height);
        float xRotation = Mathf.Lerp(maxVerticalRotation, -maxVerticalRotation, mouseRelativePosition.y);
        float yRotation = Mathf.Lerp(-maxHorizontalRotation, maxHorizontalRotation, mouseRelativePosition.x);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
