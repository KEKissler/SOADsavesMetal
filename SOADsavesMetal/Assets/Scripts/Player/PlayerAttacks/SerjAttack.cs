using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerjAttack : PlayerAttack
{
    GameObject boss;

	private bool attacking;
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    // Should target nearby enemy if in range, otherwise shoots straight forward
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_WINDUP = 0.25f;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.26f;
    private const float SHORT_ATTACK_COOLDOWN = 0.01f;
    
    // Long range attack
    public GameObject musicNote;
    private const float LONG_ATTACK_WINDUP = 0.33f;
    private const float LONG_ATTACK_COOLDOWN = 0.47f;

    // Super attack
    public GameObject cymbal;
    private const float SUPER_LENGTH = 3.7f;
    private const float TIME_BETWEEN_SHOTS_HIGH = 0.11f;
    private const float TIME_BETWEEN_SHOTS_LOW = 0.016f;
    private const int FINAL_BURST_SIZE = 1;
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

    public override IEnumerator AttackShort()
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
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_HOLD_DURATION)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }
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

    public override IEnumerator AttackLong()
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
    public override IEnumerator AttackSuper()
    {
        attacking = true;

        curr_time_between_shots = TIME_BETWEEN_SHOTS_HIGH;
        attackTimer = 0.0f;
        shotTimer = 0.0f;
        while(attackTimer < SUPER_LENGTH)
        {
            shotTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            
            float frac = attackTimer/SUPER_LENGTH;
            curr_time_between_shots = (1-frac) * TIME_BETWEEN_SHOTS_HIGH + frac * TIME_BETWEEN_SHOTS_LOW;

            if(shotTimer > curr_time_between_shots)
            {
                shotTimer %= curr_time_between_shots;
                // Debug.Log(shotTimer);
                GameObject cymbalClone = Instantiate(cymbal, transform.position, transform.parent.rotation);
            }
            yield return null;
        }

        for(int i=0; i<FINAL_BURST_SIZE; ++i)
        {
            GameObject cymbalClone = Instantiate(cymbal, transform.position, transform.parent.rotation);
            cymbalClone.transform.localScale *= new Vector2(1.2f, 1.2f);
        }

        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_COOLDOWN)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        attacking = false;
    }
}
