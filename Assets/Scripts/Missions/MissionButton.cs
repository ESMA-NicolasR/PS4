using System;
using UnityEngine;

public class MissionButton : MonoBehaviour
{
    private static readonly int IsActive = Animator.StringToHash("isActive");
    public MeshRenderer lightMeshRenderer;
    private bool _isActive;
    private Animator _animator;

    public static event Action OnMissionAccepted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SwitchLight()
    {
        _isActive = !_isActive;
        _animator.SetBool(IsActive, _isActive);
    }

    public void EnableEmission()
    {
        lightMeshRenderer.material.EnableKeyword("_EMISSION");
        lightMeshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }

    public void DisableEmission()
    {
        lightMeshRenderer.material.DisableKeyword("_EMISSION");
        lightMeshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
    }

    public void AcceptMission()
    {
        if(_isActive)
            OnMissionAccepted?.Invoke();
    }
}
