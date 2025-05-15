using System;
using Random = UnityEngine.Random;

public class ResourceSystemBool : ResourceSystem
{
    public override void Break()
    {
        // One in two chances
        targetValue = Random.value > 0.5f ? 1 : 0;
        currentValue = 1-targetValue;
    }

    public bool GetCurrentValueAsBool()
    {
        return currentValue == 1;
    }

    public void Flip()
    {
        SetValue(!GetCurrentValueAsBool());
    }

    public void SetValue(bool newValue)
    {
        SetValue(newValue?1:0);
    }

    protected override int SanitizeValue(int value)
    {
        return Math.Clamp(value, 0, 1);
    }
}
