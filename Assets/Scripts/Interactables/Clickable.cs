using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;
    private LayerMask defaultLayer;
    private LayerMask interactableLayer;
    private LayerMask highlightLayer;
    protected bool _canBeUsed;

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
        if(_canBeUsed)
            EnableHighlight();
    }
    
    protected virtual void OnMouseExit()
    {
        if(_canBeUsed)
            DisableHighlight();
    }

    public void EnableHighlight()
    {
        gameObject.layer = highlightLayer;
    }

    public void DisableHighlight()
    { 
        gameObject.layer = _canBeUsed ? interactableLayer : defaultLayer;
    }
}
