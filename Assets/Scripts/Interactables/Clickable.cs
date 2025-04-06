using System;
using Unity.VisualScripting;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Down");
    }

    private void OnMouseUp()
    {
        Debug.Log("Up");
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }
    
    private void OnMouseExit()
    {
        Debug.Log("Exit");
    }

    private void OnMouseOver()
    {
        // Debug.Log("Over");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag");
    }
}
