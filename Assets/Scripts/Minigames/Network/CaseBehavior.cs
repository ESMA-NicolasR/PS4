using UnityEngine;
using UnityEngine.Serialization;

public class CaseBehavior : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider _collider;

    public bool endCase = false, startCase = false;
    public Color baseColor;
    public MinigameNetwork minigameNetwork;

    protected void OnEnable()
    {
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

    public void ChangeColor(Color newColor)
    {
        _spriteRenderer.color = newColor;
    }

    public bool IsColor(Color other)
    {
        return _spriteRenderer.color == other;
    }

    public Color GetColor()
    {
        return _spriteRenderer.color;
    }

    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {

    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {

        }
    }
}
