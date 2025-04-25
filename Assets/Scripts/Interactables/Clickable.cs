using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public Material baseMaterial;
    public Material selectedMaterial;
    public UnityEvent OnClick;

    protected bool _canBeUsed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _canBeUsed = true;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = baseMaterial;
    }

    private void OnMouseDown()
    {
        if (_canBeUsed)
        {
            OnClick?.Invoke();
            Interact();
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Interact");
    }

    private void OnMouseEnter()
    {
        _meshRenderer.material = selectedMaterial;
    }
    
    private void OnMouseExit()
    {
        _meshRenderer.material = baseMaterial;
    }
}
