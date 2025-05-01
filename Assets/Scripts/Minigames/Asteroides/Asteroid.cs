using System;
using UnityEngine;
using System.Collections;


public class Asteroid : MonoBehaviour
{
    public MinigameAsteroids minigameAsteroids;
    public float timeToBeDestroyed;
    public float timeToDestroy;

    private void OnEnable()
    {
        MinigameAsteroids.OnMoveCursor += OnMoveCursor;
        minigameAsteroids = GameObject.Find("Asteroides").GetComponent<MinigameAsteroids>();
        StartCoroutine(DestroyShip());
    }

    private void OnDisable()
    {
        MinigameAsteroids.OnMoveCursor -= OnMoveCursor;
    }
    
    private void OnMoveCursor(Transform cursorTransform)
    {
        if (transform.position == cursorTransform.position)
        {
            minigameAsteroids.ChangeAsteroidCount(1);
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
}

//scale = 0.25 : il fait une case de plus