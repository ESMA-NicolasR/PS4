using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameAsteroids : ResourceSystem
{
    public int minAsteroids;
    public GameObject cursor;
    public float cursorX, cursorY;
    public int cursorMaxY, cursorMaxX, cursorMinY, cursorMinX;
    public override void ChangeValue(int delta)
    {
        if (delta == 1)//left
        {
            if (cursorX > cursorMinX)
            {
                cursorX -= 1;
                cursor.transform.localPosition += new Vector3(-0.1f, 0, 0);
            }
        }
        else if (delta == 2)//right
        {
            if (cursorX < cursorMaxX)
            {
                cursorX += 1;
                cursor.transform.localPosition += new Vector3(0.1f, 0, 0);
            }
        }
        else if (delta == 3)//up
        {
            if (cursorY < cursorMaxY)
            {
                cursorY += 1;
                cursor.transform.localPosition += new Vector3(0, 0.1f, 0);
            }
        }
        else if (delta == 4)//down
        {
            if (cursorY > cursorMinY)
            {
                cursorY -= 1;
                cursor.transform.localPosition += new Vector3(0, -0.1f, 0);
            }
        }
    }
    
    public override void SetValue(int newValue)
    {
        currentValue = SanitizeValue(newValue);
    }
    
    public override void Break()
    {
        SetValue(SanitizeValue(Random.Range(minValue+minAsteroids, maxValue)));
    }
}