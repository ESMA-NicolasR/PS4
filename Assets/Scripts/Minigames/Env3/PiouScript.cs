using UnityEngine;
using UnityEngine.UIElements;

public class PiouScript : MonoBehaviour
{
    private GameObject _target;
    private Vector3 _targetVector;
    [SerializeField]
    private float _offset;
    void Start()
    {
        _target = GameObject.Find("Aim");
        _targetVector = _target.transform.position;
        Vector3 displacement = gameObject.transform.position - _targetVector;
        float angle = -Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + _offset);
    }
    void FixedUpdate()
    {
        _targetVector = _target.transform.position;
        transform.position += transform.up * Time.deltaTime * 10;
        Vector3 displacement = gameObject.transform.position - _targetVector;
        float angle = -Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + _offset);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Aim"))
        {
            Destroy(gameObject);
        }
    }
}
