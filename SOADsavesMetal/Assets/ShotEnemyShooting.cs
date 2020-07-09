﻿using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyShooting : MonoBehaviour
{
    [SerializeField]
    int numberOfShots = 1;
    [SerializeField]
    float longwaitTime = 2;
    [SerializeField]
    float shortWaitTime = .5f;
    [SerializeField]
    GameObject shot;
    [SerializeField]
    public ProjectileSpeed ProjectileSpeed;
    [SerializeField]
    public ProjectileType ProjectileType;
    float timeLastShot;
    int shotSent;
    float waitTime;

    [FMODUnity.EventRef]
    public string minionProjectile;

    public EventInstance minionInstance;

    // Start is called before the first frame update
    void Start()
    {
        minionInstance = FMODUnity.RuntimeManager.CreateInstance(minionProjectile);
        timeLastShot = Time.time;
        shotSent = 0;
        waitTime = longwaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeLastShot > waitTime)
        {
            shotSent++;
            Shoot();
            timeLastShot = Time.time;
            waitTime = shortWaitTime;
            
        }

        if(shotSent >= numberOfShots)
        {
            waitTime = longwaitTime;
            shotSent = 0;
        }
    }

    private void Shoot()
    {
        GameObject fireballObject = Instantiate(shot, transform.position, Quaternion.identity);
        if (fireballObject.GetComponent<Projectile>() != null)
        {
            minionInstance.start();
            //fireballObject.GetComponent<Projectile>().Configure(gameObject, ProjectileType, ProjectileSpeed, 0f);
        }
    }
}
