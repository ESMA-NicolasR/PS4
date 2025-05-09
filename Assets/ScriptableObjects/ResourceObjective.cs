using UnityEngine;

[CreateAssetMenu(fileName = "ResourceObjective", menuName = "Data/ResourceObjective")]
public class ResourceObjective<T, U> : ScriptableObject where U : ResourceSystem<T>
{
    public U resourceSystem;
    public T target;
    public T breakValue;
    public float time;
    public string description;
    public string winMessage;
    public string loseMessage;
}
