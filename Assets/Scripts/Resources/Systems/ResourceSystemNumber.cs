using System;
using Random = UnityEngine.Random;

public class ResourceSystemNumber : ResourceSystem
{
    public int minValue;
    public int maxValue;
    public int step;
    public int minDistance;
    public int maxDistance;
    

    public override void Break()
    {
        targetValue = SanitizeValue(Random.Range(minValue+step, maxValue-step));
        int randomSign = Random.Range(0, 2) * 2 - 1;
        int randomDistance = Random.Range(step * minDistance, step * maxDistance);
        SetValue(targetValue + randomSign*randomDistance);
    }

    public override void ChangeValue(int delta)
    {
        SetValue(currentValue+delta*step);
    }

    protected override int SanitizeValue(int value)
    {
        return Math.Clamp(value - value % step, minValue, maxValue);
    }
}
