using QuestMan.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{

    public AudioSource audioSource;
    private Transform ambientMusicTransform;
    public AudioSource ambientMusic;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ambientMusicTransform = transform.Find("Ambient music");
        ambientMusic= ambientMusicTransform.GetComponent<AudioSource>();
    }

    public void StopAmbientMusic()
    {
        ambientMusic.Stop();
    }

    public void PlayAmbientMusic()
    {
        if (ambientMusic != null)
        {
            ambientMusic.Play();
        }
        else
        {
            Debug.LogError("ambientMusic is niet geïnitialiseerd!");
        }
    }
}
