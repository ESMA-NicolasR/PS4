using UnityEngine;

public class MissionIntroduction : MonoBehaviour
{
    public ResourceSystemNumber startSystem;
    public ResourceObjectiveData startObjective;
    public MissionManager missionManager;
    
    private void OnEnable()
    {
        startSystem.OnChangeValue += OnSystemChangeValue;
    }

    private void OnDisable()
    {
        startSystem.OnChangeValue -= OnSystemChangeValue;
    }

    private void Start()
    {
        startSystem.Break(startObjective.targetValue, startObjective.breakValue);
    }

    private void OnSystemChangeValue()
    {
        // Check if introduction is complete
        if (startSystem.IsFixed())
        {
            missionManager.EnableDay();
            startSystem.OnChangeValue -= OnSystemChangeValue;
            this.enabled = false;
        }
    }
}
