using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    private MinigameAsteroids _minigameAsteroids;
    public float timeToBeDestroyed;
    public float timeToDestroy;
    private Vector2 _direction;
    private float _moveSpeed;
    private float _growSpeed;
    private Coroutine destroyingCoroutine;
    private float _baseExtent;

    private void Start()
    {
        StartCoroutine(DestroyShip());
    }

    public void Init(MinigameAsteroids minigame, AsteroidSpawnData spawn)
    {
        _minigameAsteroids = minigame;
        transform.localPosition = spawn.position * _minigameAsteroids.cursorStep; // z is always 0
        _direction = spawn.direction.normalized;
        _moveSpeed = spawn.moveSpeed * _minigameAsteroids.cursorStep;
        _growSpeed = spawn.growSpeed;
        transform.localScale = Vector3.one * spawn.scale;
        _baseExtent = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
    }
    
    private void FixedUpdate()
    {
        Move();
        Grow();
    }

    private void Move()
    {
        float extent = _baseExtent * transform.localScale.x;
        transform.Translate(_moveSpeed * Time.fixedDeltaTime * _direction, Space.Self);
        // Bounce off the limits
        if (_direction.x > 0 && transform.localPosition.x + extent > _minigameAsteroids.cursorMaxX)
        {
            _direction = Vector2.Reflect(_direction, Vector2.left);
        }
        else if (_direction.x < 0 && transform.localPosition.x -extent < -_minigameAsteroids.cursorMaxX)
        {
            _direction = Vector2.Reflect(_direction, Vector2.right);
        }
        else if (_direction.y > 0 && transform.localPosition.y + extent > _minigameAsteroids.cursorMaxY)
        {
            _direction = Vector2.Reflect(_direction, Vector2.down);
        }
        else if (_direction.y < 0 && transform.localPosition.y - extent < -_minigameAsteroids.cursorMaxY)
        {
            _direction = Vector2.Reflect(_direction, Vector2.up);
        }
    }

    private void Grow()
    {
        transform.localScale += (Vector3)(_growSpeed * Time.fixedDeltaTime * Vector2.one); // Ignore z scale
    }

    private IEnumerator DestroyAsteroid()
    {
        yield return new WaitForSeconds(timeToBeDestroyed);
        _minigameAsteroids.DestroyAsteroid();
        Destroy(gameObject);
    }

    private IEnumerator DestroyShip()
    {
        yield return new WaitForSeconds(timeToDestroy);
        print("kaboom");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CursorAsteroid"))
            destroyingCoroutine = StartCoroutine(DestroyAsteroid());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CursorAsteroid") && destroyingCoroutine != null)
        {
            StopCoroutine(destroyingCoroutine);
        }
    }
}