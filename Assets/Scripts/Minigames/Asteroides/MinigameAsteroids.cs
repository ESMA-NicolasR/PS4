using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameAsteroids : ResourceSystemNumber
{
    public int minAsteroids;
    public GameObject cursor;
    public GameObject screen, screenOffset;
    public float cursorMaxY, cursorMaxX;
    public Asteroid asteroid;
    public float cursorStep;
    public int asteroidCount;
    public float timeToSpawnAsteroids;

    private void Start()
    {
        var extents = screen.GetComponent<MeshFilter>().mesh.bounds.extents;
        cursorMaxX = extents.x * screen.transform.localScale.x - cursorStep;
        cursorMaxY = extents.y * screen.transform.localScale.y - cursorStep;
    }

    public void MoveCursor(int direction)
    {
        if (direction == 1)//left
        {
            if (cursor.transform.localPosition.x-cursorStep > -cursorMaxX)
            {
                cursor.transform.localPosition += new Vector3(-cursorStep, 0, 0);
            }
        }
        else if (direction == 2)//right
        {
            if (cursor.transform.localPosition.x+cursorStep < cursorMaxX)
            {
                cursor.transform.localPosition += new Vector3(cursorStep, 0, 0);
            }
        }
        else if (direction == 3)//up
        {
            if (cursor.transform.localPosition.y+cursorStep < cursorMaxY)
            {
                cursor.transform.localPosition += new Vector3(0, cursorStep, 0);
            }
        }
        else if (direction == 4)//down
        {
            if (cursor.transform.localPosition.y-cursorStep > -cursorMaxY)
            {
                cursor.transform.localPosition += new Vector3(0, -cursorStep, 0);
            }
        }
    }
    
    public override void Break()
    {
        SetValue(SanitizeValue(Random.Range(minValue+minAsteroids, maxValue)));
        StartCoroutine(SpawnAsteroids(0));
        for (int i = 0; i < currentValue; i++)
        {
            StartCoroutine(SpawnAsteroids(Random.Range(0.1f, timeToSpawnAsteroids)));
        }
        SetValue(asteroidCount);
    }

    private IEnumerator SpawnAsteroids(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 asteroidPosition;
        asteroidPosition.z = cursor.transform.localPosition.z;
        asteroidPosition.x = cursorStep*Random.Range(-cursorMaxX, cursorMaxX);
        asteroidPosition.y = cursorStep*Random.Range(-cursorMaxY, cursorMaxY);
        var newAsteroid = Instantiate(asteroid, screenOffset.transform, false);
        newAsteroid.transform.localPosition = asteroidPosition;
        asteroidCount += 1;
    }

    public void ChangeAsteroidCount(int newValue)
    {
        asteroidCount -= newValue;
        SetValue(asteroidCount);
    }
}