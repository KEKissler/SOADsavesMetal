using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class CurtainAudio : MonoBehaviour
{
    EventInstance curtainClose;
    EventInstance curtainOpen;

    [FMODUnity.EventRef]
    public string closeEvent;
    [FMODUnity.EventRef]
    public string openEvent;

    void Start()
    {
        curtainClose = FMODUnity.RuntimeManager.CreateInstance(closeEvent);
        curtainOpen = FMODUnity.RuntimeManager.CreateInstance(openEvent);
    }

    public void CloseCurtains()
    {
        curtainClose.start();
    }
    public void OpenCurtains()
    {
        curtainOpen.start();
    }
}
