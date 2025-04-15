using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeverScript : MonoBehaviour
{
    private bool _selected;
    [SerializeField]
    private Transform _hand;
    private float _handClamped = 2;
    private bool _isUp;
    [SerializeField]
    private Slider _gauge;

    private void Start()
    {
        _isUp = true;
    }
    private void Update()
    {
        if (_handClamped < 1f && _isUp == true)
        {
            _isUp = false;
            _gauge.value += 10f;
            if (_gauge.value > 99)
            {
                print("clear");
            }
        }
        if (_handClamped > 3.5f &&  _isUp == false)
        {
            _isUp = true;
        }
    }
    private void FixedUpdate()
    {
        if (_selected == true && Input.GetButton("Fire1")) //penible, faut enlever le maintien
        {
            _handClamped = _hand.position.y;
            _handClamped = Mathf.Clamp(_handClamped, 0.5f, 4f);
            transform.position = new Vector3(-5.73f, _handClamped, 0);
        }
        _gauge.value -= 0.1f;
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