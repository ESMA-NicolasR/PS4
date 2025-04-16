using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class JunctionScript : MonoBehaviour
{
    public bool up, right, down, left;

    private bool _selected, _boolKeeper;

    void FixedUpdate()
    {

    }

    private void Update()
    {
        if (_selected == true && Input.GetButtonDown("Fire1")) //penible, faut enlever le maintien
        {
            transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 90);
            _boolKeeper = right;
            right = up;
            up = left;
            left = down;
            down = _boolKeeper;
        }
    }

    void OnMouseEnter()
    {
        _selected = true;
    }

    void OnMouseExit()
    {
        _selected = false;
    }
}
