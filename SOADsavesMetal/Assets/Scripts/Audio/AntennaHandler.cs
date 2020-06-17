using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaHandler : MonoBehaviour
{
    public FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string whiteNoiseEvent;

    public void Awake()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(whiteNoiseEvent);
    }

    public void SetupHandler(WhiteNoiseAttack whiteNoise)
    {
        whiteNoise.OnFold += FoldAntenna;
        whiteNoise.OnUnfold += UnFoldAntenna;
    }
    public void FoldAntenna()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void UnFoldAntenna()
    {
        instance.start();
    }
    private void OnDestroy()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.RESULT result = instance.getPlaybackState(out state);
        if (result == FMOD.RESULT.OK && state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
