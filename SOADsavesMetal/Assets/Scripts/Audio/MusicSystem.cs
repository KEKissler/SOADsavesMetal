using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This HAS to be singleton so that we carry the same instance across multiple scenes.
public class MusicSystem : MonoBehaviour
{
    public static MusicSystem instance;

    //Keep the bank loaded throughout all scenes
    [FMODUnity.BankRef]
    public string musicBank;

    [FMODUnity.EventRef]
    public string[] musicEvents;

    public FMOD.Studio.EventInstance currentMusic;

    //only use track select index on scenes that are level scenes
    public int trackSelectIndex = 0;
    public int currentMusicIndex;

    private void OnDestroy()
    {
        StopMusic();
        if (FMODUnity.RuntimeManager.HasBankLoaded(musicBank))
        {
            FMODUnity.RuntimeManager.UnloadBank(musicBank);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        currentMusicIndex = 0;
        currentMusic = FMODUnity.RuntimeManager.CreateInstance(musicEvents[currentMusicIndex]);
    }
    private void Start()
    {
        
    }

    public void ChangeMusic(int index)
    {
        StopMusic();
        currentMusicIndex = index;
        StartMusic();
    }

    public void StopMusic()
    {
        if (currentMusic.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE state;
            FMOD.RESULT res = currentMusic.getPlaybackState(out state);
            if (res == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                currentMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }
    public void StartMusic()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.RESULT res = currentMusic.getPlaybackState(out state);
        if (res == FMOD.RESULT.OK && (state == FMOD.Studio.PLAYBACK_STATE.STOPPED))
        {
            currentMusic = FMODUnity.RuntimeManager.CreateInstance(musicEvents[currentMusicIndex]);
            currentMusic.start();
        }
    }
    
}
