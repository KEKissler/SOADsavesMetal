using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawnerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    
    public void Spawn()
    {
        Instantiate(toSpawn, gameObject.transform.position, gameObject.transform.rotation);
    }
}
