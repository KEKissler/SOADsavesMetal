using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnAttack : PlayerAttack {

	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.28f;
    
    // Long range attack
    public GameObject drumstick;
    private const float LONG_ATTACK_COOLDOWN = 0.111f;
    public const float DRUMSTICK_FORWARD_OFFSET = 0.08f;

    // Super attack
    public GameObject cymbal;
    private const float SUPER_LENGTH = 3f;
    private const float TIME_BETWEEN_SHOTS_HIGH = 0.12f;
    private const float TIME_BETWEEN_SHOTS_LOW = 0.03f;
    private const int FINAL_BURST_SIZE = 3;
    private float curr_time_between_shots;
    private float shotTimer = 0.0f;

    // Amount to lower the spawn location of projectiles when John is crouching
    public const float WE_DONT_LIKE_EYE_LASER_DRUMSTICKS = -0.84f;

	// Use this for initialization
	void Start () {
        shortRangeHitbox.SetActive(false);
	}

    /*
	// Update is called once per frame
	void Update ()
	{
		if(!attacking && !playerScript.Dead)
		{
			if(Input.GetKeyDown(KeyCode.Z))
			{
				StartCoroutine(AttackShort());
			}
			else if(Input.GetKeyDown(KeyCode.X))
			{
				StartCoroutine(AttackLong());
			}
            else if(Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(AttackSuper());
            }
		}
	}
    */

    public override IEnumerator AttackShort()
    {
        // Windup period
        shortRangeHitbox.transform.localScale = new Vector3(0.01f,
            shortRangeHitbox.transform.localScale.y, shortRangeHitbox.transform.localScale.z);
        shortRangeHitbox.transform.localPosition = new Vector2(0.8f, 0.18f);
        shortRangeHitbox.SetActive(true);
        shortRangeHitbox.SendMessage("refreshHit");
        yield return null;

        while (ps.blockAttackProgress) yield return null;

        // Attack
        shortRangeHitbox.transform.localPosition = new Vector2(1.137f, 0.17f);
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_HOLD_DURATION)
        {
        	attackTimer += Time.deltaTime;
            shortRangeHitbox.transform.localScale = new Vector3(Mathf.Lerp(0.02f, 3.2f, attackTimer/SHORT_ATTACK_HOLD_DURATION),
                shortRangeHitbox.transform.localScale.y, shortRangeHitbox.transform.localScale.z);
        	yield return null;
        }
        shortRangeHitbox.SetActive(false);
    }

    public override IEnumerator AttackLong()
    {
        float drumstickYReductionWhenCrouched = 0f;
        //if (ps.isCrouching()) drumstickYReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_DRUMSTICKS;

    	// Create projectile (drumstick)
    	GameObject dsClone = Instantiate(drumstick, transform.position +
            new Vector3(DRUMSTICK_FORWARD_OFFSET, drumstickYReductionWhenCrouched, 0),
            transform.parent.rotation);

        yield return null;
    }

    public override IEnumerator AttackSuper()
    {
        float drumstickYReductionWhenCrouched = 0f;

        curr_time_between_shots = TIME_BETWEEN_SHOTS_HIGH;
        attackTimer = 0.0f;
        shotTimer = 0.0f;
        while(attackTimer < SUPER_LENGTH)
        {
            //if (ps.isCrouching()) drumstickYReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_DRUMSTICKS;
            shotTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            
            float frac = attackTimer/SUPER_LENGTH;
            curr_time_between_shots = (1-frac) * TIME_BETWEEN_SHOTS_HIGH + frac * TIME_BETWEEN_SHOTS_LOW;

            if(shotTimer > curr_time_between_shots)
            {
                shotTimer %= curr_time_between_shots;
                // Debug.Log(shotTimer);
                GameObject cymbalClone = Instantiate(cymbal, transform.position + 
                    new Vector3(DRUMSTICK_FORWARD_OFFSET, drumstickYReductionWhenCrouched, 0), transform.parent.rotation);
            }
            yield return null;
        }

        for(int i=0; i<FINAL_BURST_SIZE; ++i)
        {
            GameObject cymbalClone = Instantiate(cymbal, transform.position + 
                new Vector3(DRUMSTICK_FORWARD_OFFSET, drumstickYReductionWhenCrouched, 0), transform.parent.rotation);
            cymbalClone.transform.localScale *= new Vector2(1.2f, 1.2f);
        }
    }

    // Create variable get methods here

}
