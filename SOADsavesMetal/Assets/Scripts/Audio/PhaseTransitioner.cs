using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTransitioner : MonoBehaviour
{
    public MusicSetter music;

    [FMODUnity.EventRef]
    public string transitionEffect;

    FMOD.Studio.EventInstance transitionEvent;


    // Start is called before the first frame update
    void Start()
    {
        transitionEvent = FMODUnity.RuntimeManager.CreateInstance(transitionEffect);
    }
    public void PhaseTransition()
    {
        transitionEvent.start();
        music.ChangeMusic(8);
    }
}
