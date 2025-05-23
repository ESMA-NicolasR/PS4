public class ResourceDisplayTextCoordinates : ResourceDisplayText<ResourceSystemCoordinates>
{
    protected override string GetText()
    {
        return $"Send coordinates [{_resourceSystem.xSystem.currentValue},{_resourceSystem.ySystem.currentValue},{_resourceSystem.zSystem.currentValue}] to {_resourceSystem.recipientSystem.GetCurrentValueAsText()}";
    }
}
