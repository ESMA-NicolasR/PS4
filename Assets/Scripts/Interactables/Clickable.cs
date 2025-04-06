using System;
using Unity.VisualScripting;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public Material baseMaterial;
    public Material selectedMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = baseMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Down");
        Interact();
    }

    protected virtual void Interact()
    {
        Debug.Log("Interact");
    }

    private void OnMouseUp()
    {
        Debug.Log("Up");
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        _meshRenderer.material = selectedMaterial;
    }
    
    private void OnMouseExit()
    {
        Debug.Log("Exit");
        _meshRenderer.material = baseMaterial;
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
