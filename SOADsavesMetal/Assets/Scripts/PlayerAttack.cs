using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	
	private bool attacking;
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.35f;
    private const float SHORT_ATTACK_COOLDOWN = 0.1f;
    
    // Long range attack
    public GameObject drumstick;
    private const float LONG_ATTACK_COOLDOWN = 0.047f;

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

    IEnumerator AttackShort()
    {
        attacking = true;

        // Windup period
        attackTimer = 0.0f;
        while(attackTimer < 0.4f)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        // Attack
        shortRangeHitbox.transform.localScale = new Vector3(0.01f,
            shortRangeHitbox.transform.localScale.y, shortRangeHitbox.transform.localScale.z);
        shortRangeHitbox.SetActive(true);
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_HOLD_DURATION)
        {
        	attackTimer += Time.deltaTime;
            shortRangeHitbox.transform.localScale = new Vector3(Mathf.Lerp(0.01f, 3.2f, attackTimer/SHORT_ATTACK_HOLD_DURATION),
                shortRangeHitbox.transform.localScale.y, shortRangeHitbox.transform.localScale.z);
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

    IEnumerator AttackLong()
    {
    	attacking = true;

        

    	// Create projectile (drumstick)
    	GameObject dsClone = Instantiate(drumstick, transform.position, transform.parent.rotation);

        // Cooldown period
        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_COOLDOWN)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }

    	attacking = false;
    }

    IEnumerator AttackSuper()
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
            cymbalClone.transform.localScale *= new Vector2(1.25f, 1.25f);
        }

        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_COOLDOWN)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        attacking = false;
    }

    // Create variable get methods here

}
