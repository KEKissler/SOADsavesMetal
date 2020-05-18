using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTest;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        SFXVolumeTest = FMODUnity.RuntimeManager.CreateInstance("event:/UI/U_SoundTest");

        masterSlider.value = MusicSystem.instance.MasterVolume;
        musicSlider.value = MusicSystem.instance.MusicVolume;
        sfxSlider.value = MusicSystem.instance.SFXVolume;

    }

    // Update is called once per frame
    void Update()
    {
        MusicSystem.instance.Music.setVolume(MusicSystem.instance.MusicVolume);
        MusicSystem.instance.SFX.setVolume(MusicSystem.instance.SFXVolume);
        MusicSystem.instance.Master.setVolume(MusicSystem.instance.MasterVolume);
    }
    public void MasterVolumeLevel()
    {
        MusicSystem.instance.MasterVolume = masterSlider.value;
    }
    public void MusicVolumeLevel()
    {
        MusicSystem.instance.MusicVolume = musicSlider.value;
    }
    public void SFXVolumeLevel()
    {
        MusicSystem.instance.SFXVolume = sfxSlider.value;

        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.RESULT res = SFXVolumeTest.getPlaybackState(out state);
        if (res == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTest.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        SFXVolumeTest.start();
    }
}
