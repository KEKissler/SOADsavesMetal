using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	
	private bool attacking;
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    private GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.3f;
    private const float SHORT_ATTACK_COOLDOWN = 0.2f;
    
    // Long range attack
    public GameObject drumstick;
    private const float LONG_ATTACK_COOLDOWN = 0.35f;

    // Super attack
    public GameObject cymbal;
    private const float SUPER_LENGTH = 3f;
    private const float TIME_BETWEEN_SHOTS = 0.045f;
    private float shotTimer = 0.0f;
    
	// Use this for initialization
	void Start () {
        shortRangeHitbox = GameObject.Find("Player/ShortRange");
        print(shortRangeHitbox);
        shortRangeHitbox.SetActive(false);
        attacking = false;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if(!attacking)
		{
			if(Input.GetKey(KeyCode.Z))
			{
				Debug.Log("z was pressed");
				StartCoroutine(AttackShort());
			}
			else if(Input.GetKey(KeyCode.X))
			{
				Debug.Log("x was pressed");
				StartCoroutine(AttackLong());
			}
            else if(Input.GetKey(KeyCode.C))
            {
                Debug.Log("c was pressed");
                StartCoroutine(AttackSuper());
            }
		}
	}

    IEnumerator AttackShort()
    {
        attacking = true;
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

    IEnumerator AttackLong()
    {
    	attacking = true;

    	// Create projectile (drumstick)
    	GameObject dsClone = Instantiate(drumstick, transform.position, transform.rotation);

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

        attackTimer = 0.0f;
        shotTimer = 0.0f;
        while(attackTimer < SUPER_LENGTH)
        {
            shotTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;

            if(shotTimer > TIME_BETWEEN_SHOTS)
            {
                shotTimer %= TIME_BETWEEN_SHOTS;
                // Debug.Log(shotTimer);
                GameObject cymbalClone = Instantiate(cymbal, transform.position, transform.rotation);
            }
            yield return null;
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
