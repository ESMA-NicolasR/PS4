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
    public Light mainLight;
    public float mainLightMaxIntensity;
    
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
        mainLight.intensity = 0f;
        startSystem.Break(startObjective.targetValue, startObjective.breakValue);
        probeRefVolume.lightingScenario = scenarioLightsOff;
    }

    private void OnSystemChangeValue()
    {
        // Blend light scenario
        float ratio = Mathf.InverseLerp(startSystem.minValue, startSystem.maxValue, startSystem.currentValue);
        probeRefVolume.BlendLightingScenario(scenarioLightsOn, ratio);
        mainLight.intensity = Mathf.Lerp(0f, mainLightMaxIntensity, ratio);
        // Check if introduction is complete
        if (startSystem.IsFixed())
        {
            missionManager.EnableDay();
            startSystem.OnChangeValue -= OnSystemChangeValue;
            mainLight.enabled = true;
            this.enabled = false;
        }
    }
}
