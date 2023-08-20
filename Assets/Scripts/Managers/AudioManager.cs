using QuestMan.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{

    public AudioSource sfx;
    private Transform ambientMusicTransform;
    public AudioSource ambientMusic;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        ambientMusicTransform = transform.Find("Ambient music");
        ambientMusic= ambientMusicTransform.GetComponent<AudioSource>();
    }

    public void StopAmbientMusic()
    {
        ambientMusic.Pause();
    }

    public void PlayAmbientMusic()
    {
        if (ambientMusic != null&& !ambientMusic.isPlaying)
        {
            ambientMusic.Play();
        }
    }

    public void SetVolumeSFX(float value)
    {
        sfx.volume = value;
    }

    public void SetVolumeMusic(float value)
    {
        ambientMusic.volume = value;
    }
}
