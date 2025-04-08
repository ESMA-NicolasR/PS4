using UnityEngine;

public class AimController : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)*0.1f;
    }
}
