using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnClick;
    private LayerMask defaultLayer;
    private LayerMask interactableLayer;
    private LayerMask highlightLayer;
    public bool canBeUsed;
    public Focusable focusParent;
    protected virtual CursorType cursorType => CursorType.Finger;

    public static int AnalyticsTotalClicks;

    protected virtual void Awake()
    {
        canBeUsed = true;
        defaultLayer = LayerMask.NameToLayer("Default");
        interactableLayer = LayerMask.NameToLayer("Interactable");
        highlightLayer = LayerMask.NameToLayer("Highlight");
        // Disable so we can re-enable with first station
        Disable();
    }

    protected virtual void Start()
    {
        
    }

    public virtual void Disable()
    {
        canBeUsed = false;
        gameObject.layer = defaultLayer;
    }

    public virtual void Enable()
    {
        canBeUsed = true;
        gameObject.layer = interactableLayer;
    }

    private void OnMouseDown()
    {
        if (HasActiveParent())
        {
            focusParent.GainFocus();
            if (focusParent.TryGetComponent<Book>(out Book book))
            {
                book.StartBook();
            }
        }
        else if (canBeUsed)
        {
            OnClick?.Invoke();
            Interact();
        }
        // Analytics
        AnalyticsTotalClicks++;
    }

    protected virtual void Interact()
    {
        Debug.Log("Interact");
    }

    protected virtual void OnMouseEnter()
    {
        if (HasActiveParent())
        {
            focusParent.EnableHighlight();
        }
        else if(canBeUsed)
            EnableHighlight();
    }
    
    protected virtual void OnMouseExit()
    {
        if (HasActiveParent())
        {
            focusParent.DisableHighlight();
        }
        else if(canBeUsed)
            DisableHighlight();
    }

    public virtual void EnableHighlight()
    {
        gameObject.layer = highlightLayer;
        CursorManager.Instance.ChangeCursor(cursorType);
        
    }

    public virtual void DisableHighlight()
    { 
        gameObject.layer = canBeUsed ? interactableLayer : defaultLayer;
        CursorManager.Instance.ChangeCursor(CursorType.Open);
    }

    private bool HasActiveParent()
    {
        return focusParent != null && focusParent.canBeUsed;
    }
}
