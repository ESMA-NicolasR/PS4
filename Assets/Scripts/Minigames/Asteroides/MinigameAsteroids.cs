using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameAsteroids : MiniGame
{
    public ResourceSystemAsteroids resourceSystem;
    public GameObject cursor;
    public GameObject screen, screenOffset;
    public float inset;
    [HideInInspector]
    public float cursorMaxY, cursorMaxX;
    public GameObject asteroidPrefab;
    [HideInInspector]
    public float cursorStep;
    private Vector2 _lowerBounds, _higherBounds;

    private void Start()
    {
        cursor.SetActive(false);
        
        var extents = screen.GetComponent<MeshFilter>().mesh.bounds.extents;
        cursorMaxX = extents.x - inset;
        cursorMaxY = extents.y - inset;
        
        cursorStep = cursor.GetComponent<SpriteRenderer>().sprite.bounds.size.x * cursor.transform.localScale.x;
        _lowerBounds = new Vector2(-cursorMaxX + cursorStep, -cursorMaxY + cursorStep);
        _higherBounds = new Vector2(cursorMaxX - cursorStep, cursorMaxY - cursorStep);
    }

    public void MoveCursorUp()
    {
        MoveCursor(AsteroidCursorMovement.Up);
    }
    
    public void MoveCursorDown()
    {
        MoveCursor(AsteroidCursorMovement.Down);
    }
    
    public void MoveCursorLeft()
    {
        MoveCursor(AsteroidCursorMovement.Left);
    }
    
    public void MoveCursorRight()
    {
        MoveCursor(AsteroidCursorMovement.Right);
    }
    
    private void MoveCursor(AsteroidCursorMovement movement)
    {
        // Convert movement to direction
        Vector3 direction = Vector3.zero;
        switch (movement)
        {
            case AsteroidCursorMovement.Up:
                direction = Vector3.up;
                break;
            case AsteroidCursorMovement.Down:
                direction = Vector3.down;
                break;
            case AsteroidCursorMovement.Left:
                direction = Vector3.left;
                break;
            case AsteroidCursorMovement.Right:
                direction = Vector3.right;
                break;
        }
        // Move the cursor
        cursor.transform.localPosition += direction * cursorStep;
        // Clamp the cursor to the bounds
        cursor.transform.localPosition = Vector2.Min( // Higher bounds
            Vector2.Max( // Lower bounds
                cursor.transform.localPosition,
                _lowerBounds)
            ,_higherBounds
        );
        // Snap the cursor to a cursor step increment
        cursor.transform.localPosition = new Vector2(
            Mathf.Round(cursor.transform.localPosition.x / cursorStep) * cursorStep,
            Mathf.Round(cursor.transform.localPosition.y / cursorStep) * cursorStep
        );
    }
    
    public void PlayScenario(AsteroidScenarioData scenario)
    {
        StartCoroutine(SpawnAsteroids(scenario.spawnList));
    }

    private IEnumerator SpawnAsteroids(List<AsteroidSpawnData> spawnList)
    {
        resourceSystem.SetValue(spawnList.Count);
        foreach (AsteroidSpawnData spawn in spawnList)
        {
            yield return new WaitForSeconds(spawn.spawnDelay);
            var newAsteroid = Instantiate(asteroidPrefab, screenOffset.transform, false).GetComponent<Asteroid>();
            newAsteroid.Init(this, spawn);
        }
    }

    public void DestroyAsteroid()
    {
        resourceSystem.ChangeValue(-1);
    }

    private void OnValidate()
    {
        if (asteroidPrefab == null)
        {
            throw new ArgumentNullException("AsteroidPrefab", $"MinigameAsteroids {gameObject.name} must have an Asteroid Prefab component");
        }
        if (asteroidPrefab.GetComponent<Asteroid>() == null)
        {
            throw new ArgumentException($"MinigameAsteroids {gameObject.name} must have an AsteroidPrefab component with an Asteroid script attached", "AsteroidPrefab");
        }
        if (cursor != null && cursor.GetComponent<SpriteRenderer>() == null)
        {
            throw new ArgumentException($"MinigameAsteroids {gameObject.name} must have a cursor with a SpriteRender attached", "Cursor");
        }
    }

    public override void TurnOff()
    {
        cursor.SetActive(false);
    }
    
    public override void TurnOn(Focusable focusable)
    {
        if (focusable.GetComponentInParent<MiniGame>() != null && canPlay == true)
        {
            Debug.Log(GetComponent<MinigameNetwork>() == true);
            if (GetComponent<MinigameNetwork>() == true)
            {
                print("huh");
                GetComponent<MinigameNetwork>().PlayScenario(networkScenarioData);
                screenCheck.SetActive(false);
                canPlay = false;
            }
            else if (GetComponent<MinigameAsteroids>() == true)
            {
                GetComponent<MinigameAsteroids>().PlayScenario(asteroidsScenarioData);
                GetComponent<MinigameAsteroids>().cursor.SetActive(true);
                screenCheck.SetActive(false);
                canPlay = false;
            }
        }
        cursor.SetActive(true);
    }
}