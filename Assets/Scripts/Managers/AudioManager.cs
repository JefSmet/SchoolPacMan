using QuestMan.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;

        [Range(0.1f, 3f)]
        public float pitch = 1f;

        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        foreach (Sound s in sounds)
        {
            s.source = source;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
