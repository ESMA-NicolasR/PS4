using UnityEngine;

public class PlayerMoveCamera : MonoBehaviour
{
    public GameObject playerCamera;

    public float rotateSpeed;
    public float maxVerticalRotation;
    public float maxHorizontalRotation;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // Get mouse movement
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        xRotation += rotateSpeed * Time.deltaTime * -mouseMovement.y;
        yRotation += rotateSpeed * Time.deltaTime * mouseMovement.x;
        // Restrict rotation
        xRotation = Mathf.Clamp(xRotation, -maxVerticalRotation, maxVerticalRotation);
        yRotation = Mathf.Clamp(yRotation, -maxHorizontalRotation, maxHorizontalRotation);
        // Apply rotation
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
