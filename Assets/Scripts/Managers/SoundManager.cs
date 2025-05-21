using System;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> sounds = new List<AudioClip>();
    public List<string> soundNames = new List<string>();
    public Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;

    private void Awake()
    {
        if (sounds.Count == soundNames.Count)
        {
            for(int i = 0; i < sounds.Count; i++)
            {
                soundDictionary.Add(soundNames[i], sounds[i]);
            }
        }
        
        //pour le test
        PlaySound("Teemo Laugh");
    }

    public void PlaySound(string soundName, float volume = 1.0f)
    {
        audioSource.PlayOneShot(soundDictionary[soundName], volume);
    }
}
