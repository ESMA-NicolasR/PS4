using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameAsteroids : ResourceSystem
{
    public int minAsteroids;
    public GameObject cursor;
    public GameObject screen;
    public float cursorX, cursorY;
    public float cursorMaxY, cursorMaxX;
    public Asteroid asteroid;
    public float cursorStep;
    public int asteroidCount;
    public float timeToSpawnAsteroids;

    private void Start()
    {
        cursorMaxX = (15*screen.GetComponent<Renderer>().bounds.size.x)-cursorStep;
        cursorMaxY = (20*screen.GetComponent<Renderer>().bounds.size.y)-cursorStep;
        //cursor.transform.localPosition += new Vector3(0, cursorMaxY*cursorStep, 0);//si point de pivot en bas
    }
    
    public override void ChangeValue(int delta)
    {
        if (delta == 1)//left
        {
            if (cursorX-1 > -cursorMaxX)
            {
                cursorX -= 1;
                cursor.transform.localPosition += new Vector3(-cursorStep, 0, 0);
            }
        }
        else if (delta == 2)//right
        {
            if (cursorX+1 < cursorMaxX)
            {
                cursorX += 1;
                cursor.transform.localPosition += new Vector3(cursorStep, 0, 0);
            }
        }
        else if (delta == 3)//up
        {
            if (cursorY+1 < cursorMaxY)
            {
                cursorY += 1;
                cursor.transform.localPosition += new Vector3(0, cursorStep, 0);
            }
        }
        else if (delta == 4)//down
        {
            if (cursorY-1 > -cursorMaxY)
            {
                cursorY -= 1;
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
        var newAsteroid = Instantiate(asteroid, screen.transform, false);
        newAsteroid.transform.localPosition = asteroidPosition;
        asteroidCount += 1;
    }

    public void ChangeAsteroidCount(int newValue)
    {
        asteroidCount -= newValue;
        SetValue(asteroidCount);
    }
}