﻿using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHandMovement : MonoBehaviour
{
    [SerializeField]
    GameObject AttackObject;
    [SerializeField]
    float speed = .1f;
    [SerializeField]
    float attackTime = .5f;
    [SerializeField]
    float attackPos = 5f;
    [SerializeField]
    bool goUp = false;
    [SerializeField]
    bool attack = false;
    bool goDown = false;
    bool damageDone = false;
    float time = 0;
    float DeathTime = 10;

    [FMODUnity.EventRef]
    public string pushHandEvent;

    //save on instances rather than constantly creating them
    public EventInstance pushHandInstance;

    [FMODUnity.EventRef]
    public string crushEvent;
    //save on instances rather than constantly creating them
    public EventInstance crushInstance;

    // Start is called before the first frame update
    void Start()
    {
        pushHandInstance = FMODUnity.RuntimeManager.CreateInstance(pushHandEvent);
        crushInstance = FMODUnity.RuntimeManager.CreateInstance(crushEvent);
        AttackObject.SetActive(true);
        time = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(goDown == true)
        {
            //AttackObject.SetActive(false);
            transform.position = transform.position + new Vector3(0, -speed * Time.deltaTime, 0);
            AttackObject.GetComponent<SandHandAttack>().setDamage(false);
        }
        else if (goUp && Time.time - time > attackTime)
        {
            //AttackObject.SetActive(false);
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if(transform.position.y > attackPos && attack == true)
        {
            if (goUp == true)
            {
                time = Time.time;
                //start playing attack animation
                gameObject.GetComponent<Animator>().SetBool("Crush", true);

            }
            goUp = false;
            
            

            if(Time.time - time >= attackTime){
                AttackObject.GetComponent<SandHandAttack>().setDamage(true);
                damageDone = true;
                time = Time.time;
            }
            else if(Time.time - time >= attackTime/2 && damageDone)
            {
                attack = false; goDown = true;
                time = Time.time;
                gameObject.GetComponent<Animator>().SetBool("Crush", false);
            }

        }
        if(Time.time - time > DeathTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void goUpNow(Transform player, float Y_offset)
    {
        gameObject.transform.position = new Vector3(player.position.x, Y_offset, 0);
        goUp = true;
        attack = true;
        time = 0;
        pushHandInstance.start();
    }

    public void PlayCrush()
    {
        crushInstance.start();
    }
}
