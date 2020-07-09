using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionWingFlap : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string flapEvent;

    public EventInstance wingFlapInstance;

    private void Start()
    {
        wingFlapInstance = FMODUnity.RuntimeManager.CreateInstance(flapEvent);
    }
    public void WingFlap()
    {
        //wingFlapInstance.start();
    }
}
