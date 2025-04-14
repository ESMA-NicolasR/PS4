using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    private bool _selected;
    private bool _pressed = false;

    private void Update()
    {
        if (_selected == true && Input.GetButton("Fire1")) //penible, faut enlever le maintien
        {
            _pressed = true;
        }
        else _pressed = false;
    }

    void OnMouseEnter()
    {
        _selected = true;
    }

    void OnMouseExit()
    {
        _selected = false;
    }

    public bool IsPressed()
    {
        return _pressed;
    }
}
