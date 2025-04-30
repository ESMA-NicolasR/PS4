using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameAsteroids : ResourceSystem
{
    public int minAsteroids;
    public GameObject cursor;
    public float cursorX;
    public float cursorY;
    public override void ChangeValue(int delta)
    {
        if (delta == 1)//left
        {
            cursor.transform.localPosition += new Vector3(-0.1f, 0, 0);
        }
        else if (delta == 2)//right
        {
            cursor.transform.localPosition += new Vector3(0.1f, 0, 0);
        }
        else if (delta == 3)//up
        {
            cursor.transform.localPosition += new Vector3(0, 0.1f, 0);
        }
        else if (delta == 4)//down
        {
            cursor.transform.localPosition += new Vector3(0, -0.1f, 0);
        }
    }
    public override void Break()
    {
        SetValue(SanitizeValue(Random.Range(minValue+minAsteroids, maxValue)));
    }
}
