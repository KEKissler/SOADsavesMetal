﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoAttack : MonoBehaviour
{
    GameObject boss;

	private bool attacking;
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_WINDUP = 0.25f;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.25f;
    private const float SHORT_ATTACK_COOLDOWN = 0.01f;
    
    // Long range attack
    public GameObject musicNote;
    private const float LONG_ATTACK_WINDUP = 0.33f;
    private const float LONG_ATTACK_COOLDOWN = 0.47f;

    // Super attack
    public GameObject cymbal;
    private const float SUPER_LENGTH = 3f;
    private const float TIME_BETWEEN_SHOTS_HIGH = 0.07f;
    private const float TIME_BETWEEN_SHOTS_LOW = 0.006f;
    private const int FINAL_BURST_SIZE = 70;
    private float curr_time_between_shots;
    private float shotTimer = 0.0f;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.FindWithTag("Boss");
        shortRangeHitbox.SetActive(false);
        attacking = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if(!attacking)
		{
			if(Input.GetKey(KeyCode.Z))
			{
				StartCoroutine(AttackShort());
			}
			else if(Input.GetKey(KeyCode.X))
			{
				StartCoroutine(AttackLong());
			}
            else if(Input.GetKey(KeyCode.C))
            {
                StartCoroutine(AttackSuper());
            }
		}
	}

    // Returns the enemy that should be targeted by the long range attack
    GameObject getTarget()
    {
        return boss;
    }

    IEnumerator AttackShort()
    {
        attacking = true;

        // Windup period
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_WINDUP)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        // Attack
        shortRangeHitbox.SetActive(true);
        yield return null;
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_HOLD_DURATION)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }
        yield return null;
        shortRangeHitbox.SendMessage("refreshHit");
        shortRangeHitbox.SetActive(false);

        // Cooldown period
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_COOLDOWN)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }

        attacking = false;
    }

    IEnumerator AttackLong()
    {
    	attacking = true;

        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_WINDUP)
        {
            attackTimer += Time.deltaTime;
        	yield return null;     
        }

    	// Create projectile (musicNote)
    	GameObject dsClone = Instantiate(musicNote, getTarget().transform.position + new Vector3(0, 4f, 0), transform.parent.rotation);

        // Cooldown period
        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_COOLDOWN)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }

    	attacking = false;
    }

    // Maybe super bleeds / distracts boss if it hits?
    // (Disrupts attack patterns but that would require attack patterns to be created first)
    IEnumerator AttackSuper()
    {
        yield return null;
    }
}
