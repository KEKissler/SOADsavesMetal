using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text text;
    private bool countDownRunning = false;
    public AnimationClip FadeInAnim;

    //Only use a singular instance for countdown object
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string countDownTick;

    public MusicSetter music;

    void Start()
    {
        StartCoroutine("countDown");
    }

    public bool getCountDown()
    {
        return countDownRunning;
    }

    public IEnumerator countDown()
    {
        
        countDownRunning = true;
        yield return new WaitForSecondsRealtime(FadeInAnim.length);
        yield return new WaitForSecondsRealtime(.1f);
        Time.timeScale = 0f;
        text.text = "3";
        PlayWithStop(countDownTick);
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "2";
        PlayWithStop(countDownTick);
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "1";
        PlayWithStop(countDownTick);
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "Let's Rock!";
        PlayWithStop(countDownTick);
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "";
        countDownRunning = false;
        Time.timeScale = 1f;
        music.StartMusic();
        text.transform.parent.gameObject.SetActive(false);    
    }

    public void PlayWithStop(string audioEvent)
    {
        if (instance.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE state;
            instance.getPlaybackState(out state);
            if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        instance = FMODUnity.RuntimeManager.CreateInstance(audioEvent);
        instance.start();
    }

}
