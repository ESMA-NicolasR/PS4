using UnityEngine;
using UnityEngine.InputSystem;

public class HandScript : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.5f);
    }
}
