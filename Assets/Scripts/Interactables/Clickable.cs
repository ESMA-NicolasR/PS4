using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public Material baseMaterial;
    public Material selectedMaterial;
    public UnityEvent OnClick;

    protected bool _canBeUsed;

    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = baseMaterial;
        _collider = GetComponent<Collider>();
        _canBeUsed = true;
        // Disable so we can re-enable with first station
        Disable();
    }

    protected virtual void Start()
    {
    }

    public void Disable()
    {
        _collider.enabled = false;
    }

    public void Enable()
    {
        _collider.enabled = true;
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

    protected virtual void OnMouseEnter()
    {
        DisplaySelected();
    }
    
    protected virtual void OnMouseExit()
    {
        DisplayNotSelected();
    }

    private void DisplaySelected()
    {
        _meshRenderer.material = selectedMaterial;
    }
    
    private void DisplayNotSelected()
    {
        _meshRenderer.material = baseMaterial;
    }
}
