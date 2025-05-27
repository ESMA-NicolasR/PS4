using UnityEngine;

public class MissionIntroduction : MonoBehaviour
{
    public ResourceSystemNumber startSystem;
    public ResourceObjectiveData startObjective;
    public MissionManager missionManager;
    UnityEngine.Rendering.ProbeReferenceVolume probeRefVolume;
    [Min(1)] public int numberOfCellsBlendedPerFrame = 10;
    public string scenarioLightsOff;
    public string scenarioLightsOn;
    
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
        probeRefVolume = UnityEngine.Rendering.ProbeReferenceVolume.instance;
        probeRefVolume.numberOfCellsBlendedPerFrame = numberOfCellsBlendedPerFrame;
        
        // Prepare light scenario
        startSystem.Break(startObjective.targetValue, startObjective.breakValue);
        probeRefVolume.lightingScenario = scenarioLightsOff;
    }

    private void OnSystemChangeValue()
    {
        // Blend light scenario
        probeRefVolume.BlendLightingScenario(scenarioLightsOn, Mathf.InverseLerp(startSystem.minValue, startSystem.maxValue, startSystem.currentValue));
        
        // Check if introduction is complete
        if (startSystem.IsFixed())
        {
            missionManager.EnableDay();
            startSystem.OnChangeValue -= OnSystemChangeValue;
            this.enabled = false;
        }
    }
}
