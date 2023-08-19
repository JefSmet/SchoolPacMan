using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    private float startSfxValue;
    private float startMusicValue;

    void Start()
    {
        startSfxValue = AudioManager.Instance.sfx.volume;
        startMusicValue = AudioManager.Instance.ambientMusic.volume;
        sfxSlider.value = startSfxValue;
        musicSlider.value = startMusicValue;

    }
    public void Accept()
    {
        if (GameManager.Instance.CurrentScene().Equals("Settings")) 
        {
            GameManager.Instance.LoadScene("MainMenu");
        }
        else
        {
            LevelManager.Instance.HideSettings();
            LevelManager.Instance.ShowPauseMenu();
        }
    }

    public void Cancel()
    {
        AudioManager.Instance.sfx.volume = startSfxValue;
        AudioManager.Instance.ambientMusic.volume = startMusicValue;
        Accept();
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.Instance.SetVolumeSFX(value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolumeMusic(value);
    }
}
