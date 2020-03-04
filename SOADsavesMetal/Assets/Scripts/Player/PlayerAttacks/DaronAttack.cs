using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaronAttack : PlayerAttack
{
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    // Should target nearby enemy if in range, otherwise shoots straight forward
    public GameObject stringBreak;
    private const float SHORT_ATTACK_WINDUP = 0.25f;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.26f;
    
    // Long range attack
    public GameObject guitarPick;
    private const float LONG_ATTACK_WINDUP = 0.04f;

    // Super attack
    public GameObject cymbal;
    private const float SUPER_LENGTH = 3.7f;
    private const float TIME_BETWEEN_SHOTS_HIGH = 0.11f;
    private const float TIME_BETWEEN_SHOTS_LOW = 0.016f;
    private const int FINAL_BURST_SIZE = 1;
    private float curr_time_between_shots;
    private float shotTimer = 0.0f;

    // Copied from JohnAttack
    // Amount to lower the spawn location of projectiles when Daron is crouching
    public const float WE_DONT_LIKE_EYE_LASER_DRUMSTICKS = -0.84f;
    
	// Use this for initialization
	void Start () {

	}

    /*
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
    */

    public override IEnumerator AttackShort()
    {
        float yReductionWhenCrouched = 0f;
        if (ps.isCrouching()) yReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_DRUMSTICKS;

    	GameObject attackObj = Instantiate(stringBreak, transform.parent.position +
            new Vector3(0.7f * getDirection(transform.parent), yReductionWhenCrouched, 0),
            transform.parent.rotation);

        yield return null;

    }

    public override IEnumerator AttackLong()
    {
        attackTimer = 0f;
        while (attackTimer < LONG_ATTACK_WINDUP)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        float yReductionWhenCrouched = 0f;
        if (ps.isCrouching()) yReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_DRUMSTICKS;

    	GameObject pick = Instantiate(guitarPick, transform.parent.position +
            new Vector3(0.02f, yReductionWhenCrouched + 0.4f, 0),
            transform.parent.rotation);

        yield return null;
    }

    public override IEnumerator AttackSuper()
    {
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
    }
}
