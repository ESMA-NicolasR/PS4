using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector3 basePos;
    public bool isDragged;
    private Vector3 _mouseStartPos;

    public float maxRaycastDistance;
    public LayerMask priseLayer;
    private Collider _collider;

    public Prise priseScript;
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Prise>(out var prise))
        {
            priseScript = prise;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Prise>(out var prise))
        {
            priseScript = null;
        }
    }


    private void OnMouseDown()
    {
        isDragged = true;
        _mouseStartPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (priseScript != null)
        {
            priseScript.inPrise = false;
        }
    }
    private void OnMouseUp()
    {
        isDragged = false;
        ResetPosition();
        
        if (priseScript != null && (!priseScript.inPrise))
        {
            transform.position = priseScript.Hook.transform.position;
            priseScript.inPrise = true;
        }
        
        /*
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, priseLayer, QueryTriggerInteraction.Collide)) {
            Transform objectHit = hit.transform;
            Debug.Log("Touché");
            var prise = hit.transform.GetComponent<Prise>();
            transform.position = prise.Hook.transform.position;
            Debug.Log("Aggripé");
            // Do something with the object that was hit by the raycast.
        }*/
    }

    private void Start()
    {
        basePos = transform.position;
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isDragged)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mouseStartPos);
            // = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        gameObject.transform.position = basePos;
        isDragged = false;
        _mouseStartPos = Vector3.zero;
    }

}
