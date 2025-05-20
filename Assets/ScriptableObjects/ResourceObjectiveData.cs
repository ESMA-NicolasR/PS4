using UnityEngine;

[CreateAssetMenu(fileName = "ResourceObjectiveData", menuName = "Data/ResourceObjectiveData")]
public class ResourceObjectiveData : ScriptableObject
{
    public SystemName systemName;
    public int targetValue;
    public int breakValue;
    public float time;
    public string description;
    public string winMessage;
    public string loseMessage;

    public virtual void BreakSystem(ResourceSystem resourceSystem)
    {
        resourceSystem.Break(targetValue, breakValue);
    }

    public virtual void End(ResourceSystem resourceSystem)
    {
        Debug.Log("Objective ended");
    }
}
