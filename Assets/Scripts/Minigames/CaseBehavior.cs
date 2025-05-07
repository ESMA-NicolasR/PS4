using UnityEngine;
using UnityEngine.Events;

public class CaseBehavior : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider _collider;

    public bool endCase = false;
    public Color baseColor;
    public Minigame_Network minigameNetwork;

    protected void OnEnable()
    {
        minigameNetwork = GameObject.Find("Network").GetComponent<Minigame_Network>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = baseColor;
        _collider = GetComponent<Collider>();
        // Disable so we can re-enable with first station
        Disable();
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
        minigameNetwork.CaseClicked(this);
    }
    private void OnMouseUp()
    {
        minigameNetwork.CaseUnclicked(this);
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            minigameNetwork.CaseSelected(this);
        }
    }
    
    private void OnMouseExit()
    {
        if (Input.GetMouseButton(0))
        {
            
        }
    }
}
