using UnityEngine;

public class ClickableSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    
    public void PlayMySound()
    {
        SoundManager.Instance.PlaySound(audioClip);
    }
}
