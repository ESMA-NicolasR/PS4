using UnityEngine;

[CreateAssetMenu(fileName = "ResourceObjectiveData", menuName = "Data/ResourceObjectiveData")]
public class ResourceObjectiveData : ScriptableObject
{
    public SystemName systemName;
    public float time;
    public string description;
    public string winMessage;
    public string loseMessage;
}
