using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    public MinigameAsteroids minigameAsteroids;

    private void OnEnable()
    {
        MinigameAsteroids.OnMoveCursor += OnMoveCursor;
        minigameAsteroids = GameObject.Find("Asteroides").GetComponent<MinigameAsteroids>();
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
            Destroy(gameObject);
        }
    }
}
