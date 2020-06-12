using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool firstLoad;
    public bool firstPlay;
    //public bool[] characterUnlocks;
    public bool shavoUnlock;
    public bool daronUnlock;
    public bool serjUnlock;
    public int controlScheme;

    public SaveData ()
    {
        firstLoad = StaticData.firstLoad;
        firstPlay = StaticData.firstPlay;
        //characterUnlocks = StaticData.characterUnlocks;
        shavoUnlock = StaticData.shavoUnlock;
        daronUnlock = StaticData.daronUnlock;
        serjUnlock = StaticData.serjUnlock;
        controlScheme = StaticData.controlScheme;
    }
}
