using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;
    private LayerMask defaultLayer;
    private LayerMask interactableLayer;
    private LayerMask highlightLayer;
    protected bool _canBeUsed;
    public Focusable focusParent;

    protected virtual void Awake()
    {
        _canBeUsed = true;
        defaultLayer = LayerMask.NameToLayer("Default");
        interactableLayer = LayerMask.NameToLayer("Interactable");
        highlightLayer = LayerMask.NameToLayer("Highlight");
        // Disable so we can re-enable with first station
        Disable();
    }

    protected virtual void Start()
    {
    }

    public void Disable()
    {
        _canBeUsed = false;
        gameObject.layer = defaultLayer;
    }

    public void Enable()
    {
        _canBeUsed = true;
        gameObject.layer = interactableLayer;
    }

    private void OnMouseDown()
    {
        // Determine if we let the parent handle this
        if (focusParent != null && !focusParent.isFocused)
        {
            focusParent.GainFocus();
        }
        else if (_canBeUsed)
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
        // Determine if we let the parent handle this
        if (focusParent != null && !focusParent.isFocused)
        {
            focusParent.EnableHighlight();
        }
        else if(_canBeUsed)
            EnableHighlight();
    }
    
    protected virtual void OnMouseExit()
    {
        // Determine if we let the parent handle this
        if (focusParent != null && !focusParent.isFocused)
        {
            focusParent.DisableHighlight();
        }
        else if(_canBeUsed)
            DisableHighlight();
    }

    public virtual void EnableHighlight()
    {
        gameObject.layer = highlightLayer;
    }

    public virtual void DisableHighlight()
    { 
        gameObject.layer = _canBeUsed ? interactableLayer : defaultLayer;
    }
}
