using System;
using Random = UnityEngine.Random;

public class RessourceAsteroides : ResourceSystem
{
    public int minAsteroides; 
    public override void Break()
    {
        SetValue(SanitizeValue(Random.Range(minValue+minAsteroides, maxValue)));
    }
}
