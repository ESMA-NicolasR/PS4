using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static SoundManager Instance;

    private void Start()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(AudioClip sound, float volume = 1.0f)
    {
        audioSource.PlayOneShot(sound, volume);
    }
}
