using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    protected static SoundManager Instance;

    public void PlaySound(AudioClip sound, float volume = 1.0f)
    {
        audioSource.PlayOneShot(sound, volume);
    }
}
