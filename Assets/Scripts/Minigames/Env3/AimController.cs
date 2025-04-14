using UnityEngine;
using UnityEngine.EventSystems;

public class AimController : MonoBehaviour
{
    [SerializeField]
    private ButtonScript _right, _left, _up, _down;
    private float _horizontal, _vertical;

    private void FixedUpdate()
    {
        if (_right.IsPressed())
        {
            _horizontal = 1;
        }
        else if (_left.IsPressed())
        {
            _horizontal = -1;
        }
        else
        {
            _horizontal = Input.GetAxis("Horizontal");
        }

        if (_up.IsPressed())
        {
            _vertical = 1;
        }
        else if (_down.IsPressed())
        {
            _vertical = -1;
        }
        else
        {
            _vertical = Input.GetAxis("Vertical");
        }

        transform.position = transform.position + new Vector3(_horizontal, _vertical, 0) * 0.1f;
    }
}
