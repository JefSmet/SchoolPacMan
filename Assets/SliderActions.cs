using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActions : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    private float startSfxValue;
    private float startMusicValue;    
    private void Start()
    {

        
        
        UpdateSliders();
        
    }
    public void SetSfxVolume(float value)
    {
        AudioManager.Instance.SetVolumeSFX(value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolumeMusic(value);
    }
    public void Cancel()
    {
        AudioManager.Instance.sfx.volume = startSfxValue;
        AudioManager.Instance.ambientMusic.volume = startMusicValue;
        GameManager.Instance.LoadScene("MainMenu");
    }

    public void UpdateSliders()
    {
        startSfxValue = AudioManager.Instance.sfx.volume;
        startMusicValue = AudioManager.Instance.ambientMusic.volume;
        sfxSlider.value = startSfxValue;
        musicSlider.value = startMusicValue;
    }
}
