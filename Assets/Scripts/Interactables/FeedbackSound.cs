using UnityEngine;

public class FeedbackSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private float _volume = 0.5f;
    
    public void PlayMySound()
    {
        SoundManager.Instance.PlaySound(audioClip, _volume);
    }
}
