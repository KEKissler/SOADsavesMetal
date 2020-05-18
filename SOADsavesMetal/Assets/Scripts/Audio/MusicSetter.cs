using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script sets scene specific variables on start to the MusicSystem
public class MusicSetter : MonoBehaviour
{
    public int defaultMusicIndex;
    public bool isBattleScene;

    // Start is called before the first frame update
    void Start()
    {
        if (!isBattleScene)
        {
            StartMusic();
        }
    }

    public void StartMusic()
    {
        if(!isBattleScene)
        {
            MusicSystem.instance.currentMusicIndex = defaultMusicIndex;
            MusicSystem.instance.StartMusic();
        }
        else
        {
            if(MusicSystem.instance.trackSelectIndex == 0) //default
            {
                MusicSystem.instance.ChangeMusic(defaultMusicIndex);
            }
            else
            {
                MusicSystem.instance.ChangeMusic(MusicSystem.instance.trackSelectIndex);
            }
        }
    }
    public void StopMusic()
    {
        MusicSystem.instance.StopMusic();
    }
    public void QueueBattleMusic(int track)
    {
        MusicSystem.instance.trackSelectIndex = track;
    }
}
