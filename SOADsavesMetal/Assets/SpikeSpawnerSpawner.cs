﻿using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawnerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    [FMODUnity.EventRef]
    public string spawnEvent;

    public void Spawn()
    {
        Instantiate(toSpawn, gameObject.transform.position, gameObject.transform.rotation);
        EventInstance breakInstance = FMODUnity.RuntimeManager.CreateInstance(spawnEvent);
        breakInstance.start();
    }
}
