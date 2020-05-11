using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad_Audio : MonoBehaviour
{
    #region FMODEvents
    [FMODUnity.EventRef]
    public string UIBack;
    [FMODUnity.EventRef]
    public string UIMove;
    [FMODUnity.EventRef]
    public string UISelect;
    [FMODUnity.EventRef]
    public string UIError;
    [FMODUnity.EventRef]
    public string UISelectStage;
    #endregion

    public void PlayBackSound()
    {
        PlayAudioEvent(UIBack);
    }
    public void PlayMoveSound()
    {
        PlayAudioEvent(UIMove);
    }
    public void PlayErrorSound()
    {
        PlayAudioEvent(UIError);
    }
    public void PlaySelectStage()
    {
        PlayAudioEvent(UISelectStage);
    }
    public void PlaySelectSound()
    {
        PlayAudioEvent(UISelect);
    }

    public void PlayAudioEvent(string fmodEvent)
    {
        EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }
}
