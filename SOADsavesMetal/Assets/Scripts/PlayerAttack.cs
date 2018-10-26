using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	// Short range attack
    private GameObject shortRangeHitbox;
    private bool attacking;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.3f;
    private float currAttackTime = 0.0f;
    private const float SHORT_ATTACK_COOLDOWN = 0.2f;
    private float currCooldown = 0.0f;

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
		if(!attacking && Input.GetKey(KeyCode.Z))
		{
			Debug.Log("z was pressed");
			StartCoroutine(AttackShort());
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


}
