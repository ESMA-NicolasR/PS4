using UnityEngine;

public class Cord : Draggable
{
    public Transform highPoint, lowPoint;
    private float _progress = 1f;
    public float speedReturning;
    private bool _isReturning;
    

    protected override void Interact()
    {
        base.Interact();
        _isReturning = false;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        _isReturning = true;
    }

    protected override void Update()
    {
        base.Update();
        if(_isReturning)
            _progress += speedReturning * Time.deltaTime;
        
        _progress = Mathf.Clamp01(_progress);
        
        // Limiter le mouvement entre la position haute et la position basse
        float newY = Mathf.Lerp(lowPoint.position.y, highPoint.position.y, _progress);
        
        // Appliquer la nouvelle position
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
        return;
        /*if (isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, 3f * Time.deltaTime);
        }*/
    }

    protected override void Drag(Vector2 delta)
    {
        // On ne prend en compte que le mouvement vertical (Y)
        _progress += delta.y/Screen.height;
        if (_progress <= 0.1f)
        {
            Debug.Log("TchouTchou");
        }
        
    }
}