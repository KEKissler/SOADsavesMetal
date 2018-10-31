using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	
	private bool attacking;
	private float currCooldown = 0.0f;
	private float currAttackTime = 0.0f;

	// Short range attack
    private GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.3f;
    private const float SHORT_ATTACK_COOLDOWN = 0.2f;
    
    // Long range attack
    private GameObject drumstick;
    private const float LONG_ATTACK_COOLDOWN = 0.35f;
    
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
		}
	}

    IEnumerator AttackShort()
    {
        attacking = true;
        shortRangeHitbox.SetActive(true);
        currAttackTime = 0.0f;
        while(currAttackTime < SHORT_ATTACK_HOLD_DURATION)
        {
        	currAttackTime += Time.deltaTime;
        	yield return null;
        }
        shortRangeHitbox.SetActive(false);

        // Cooldown period
        currCooldown = 0.0f;
        while(currCooldown < SHORT_ATTACK_COOLDOWN)
        {
        	currCooldown += Time.deltaTime;
        	yield return null;
        }

        attacking = false;
    }

    IEnumerator AttackLong()
    {
    	attacking = true;

    	// Create projectile (drumstick)

        // Cooldown period
        currCooldown = 0.0f;
        while(currCooldown < LONG_ATTACK_COOLDOWN)
        {
        	currCooldown += Time.deltaTime;
        	yield return null;
        }

    	attacking = false;
    }

    // Create variable get methods here

}
