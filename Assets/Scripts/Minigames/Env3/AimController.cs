using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AimController : MonoBehaviour
{
    [SerializeField]
    private ButtonScript _right, _left, _up, _down;
    private float _horizontal, _vertical;

    private void Start()
    {
        StartCoroutine("Move");
    }
    private void FixedUpdate()
    {
        if (_right.IsPressed() || Input.GetAxis("Horizontal") > 0.25f)
        {
            _horizontal = 1;
        }
        else if (_left.IsPressed() || Input.GetAxis("Horizontal") < -0.25f)
        {
            _horizontal = -1;
        }
        else _horizontal = 0;

        if (_up.IsPressed() || Input.GetAxis("Vertical") > 0.25f)
        {
            _vertical = 1;
        }
        else if (_down.IsPressed() || Input.GetAxis("Vertical") < -0.25f)
        {
            _vertical = -1;
        }
        else _vertical = 0;
    }

    private IEnumerator Move()
    {
        transform.position = transform.position + new Vector3(_horizontal, _vertical, 0) * 1f;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("Move");
    }
}
