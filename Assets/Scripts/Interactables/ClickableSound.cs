using UnityEngine;

public class ClickableSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    
    public void PlayMySound(float volume = 0.5f)
    {
        SoundManager.Instance.PlaySound(audioClip, volume);
    }
}
