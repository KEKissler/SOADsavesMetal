using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This HAS to be singleton so that we carry the same instance across multiple scenes.
public class MusicSystem : MonoBehaviour
{
    public static MusicSystem instance;

    //I am doing a real bad here because I really should be using a diff class to store these
    //These values will store the settings from the panel
    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;

    public FMOD.Studio.Bus Music;
    public FMOD.Studio.Bus SFX;
    public FMOD.Studio.Bus Master;

    [Header("Music System Settings")]
    //Keep the bank loaded throughout all scenes
    [FMODUnity.BankRef]
    public string musicBank;

    [FMODUnity.EventRef]
    public string[] musicEvents;

    public FMOD.Studio.EventInstance currentMusic;

    //only use track select index on scenes that are level scenes
    public int trackSelectIndex = 0;
    public int currentMusicIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        FMODUnity.RuntimeManager.LoadBank(musicBank);
        currentMusicIndex = 0;
        MasterVolume = 1f;
        MusicVolume = .5f;
        SFXVolume = .5f;
        Music = FMODUnity.RuntimeManager.GetBus("bus:/MASTER/MUS");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/MASTER/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/MASTER");
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void ChangeMusic(int index)
    {
        if (currentMusicIndex == index)
        {
            if (!IsCurrentMusicPlaying())
            {
                StartMusic();
            }
            return;
        }
        StopMusic();
        currentMusicIndex = index;
        StartMusic();
    }

    public void StopMusic()
    {
        if (IsCurrentMusicPlaying())
        {
            currentMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    public void StartMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.RESULT res = currentMusic.getPlaybackState(out state);
        if ((res == FMOD.RESULT.OK && (state == FMOD.Studio.PLAYBACK_STATE.STOPPED || state == FMOD.Studio.PLAYBACK_STATE.STOPPING)) || res == FMOD.RESULT.ERR_INVALID_HANDLE)
        {
            currentMusic = FMODUnity.RuntimeManager.CreateInstance(musicEvents[currentMusicIndex]);
            currentMusic.start();
        }
    }

    private bool IsCurrentMusicPlaying()
    {
        if (!currentMusic.isValid())
        {
            return false;
        }
        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.RESULT res = currentMusic.getPlaybackState(out state);
        return res == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING;
    }
    
}
