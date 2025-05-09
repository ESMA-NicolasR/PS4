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
}
