using UnityEngine;
using UnityEngine.Events;

public class CaseBehavior : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider _collider;

    public Color baseColor;
    public UnityEvent OnClick;

    protected bool _canBeUsed;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = baseColor;
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

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            print("lol");
        }
    }
    
    private void OnMouseExit()
    {

    }
}
