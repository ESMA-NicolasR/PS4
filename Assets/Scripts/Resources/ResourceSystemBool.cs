using UnityEngine;

public class ResourceSystemBool : ResourceSystem<bool>
{
    public override void Break()
    {
        // One in two chances
        targetValue = Random.value > 0.5f;
        currentValue = !targetValue;
    }
}
