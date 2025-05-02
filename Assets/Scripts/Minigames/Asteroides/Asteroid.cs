using System;
using UnityEngine;
using System.Collections;


public class Asteroid : MonoBehaviour
{
    private MinigameAsteroids _minigameAsteroids;
    public float timeToBeDestroyed;
    public float timeToDestroy;
    public float speed;
    private Vector3 _newPosition;

    private void OnEnable()
    {
        MinigameAsteroids.OnMoveCursor += OnMoveCursor;
        _minigameAsteroids = GameObject.Find("Asteroids").GetComponent<MinigameAsteroids>();
        StartCoroutine(DestroyShip());
        if (speed != 0)
        {
            Moving();
        }
    }

    private void OnDisable()
    {
        MinigameAsteroids.OnMoveCursor -= OnMoveCursor;
    }
    
    private void OnMoveCursor(Transform cursorTransform)
    {
        if (transform.position == cursorTransform.position)
        {
            _minigameAsteroids.ChangeAsteroidCount(1);
            StartCoroutine(DestroyAsteroids(cursorTransform.gameObject));
        }
    }

    private IEnumerator DestroyAsteroids(GameObject cursor)
    {
        yield return new WaitForSeconds(timeToBeDestroyed);
        if (cursor.transform.position == transform.position)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyShip()
    {
        yield return new WaitForSeconds(timeToDestroy);
        print("kaboum");
    }

    /*private IEnumerator Moving()
    {
        yield return new WaitForSeconds(1/speed*0.1f);
    }*/

    private void Moving()
    {
        Vector2.Lerp(transform.position, _newPosition, Time.deltaTime * speed);
    }
}

//scale = 0.25 : il fait une case de plus