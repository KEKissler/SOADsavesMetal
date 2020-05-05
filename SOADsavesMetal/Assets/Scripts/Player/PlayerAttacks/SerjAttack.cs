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
    public const float MIC_FORWARD_OFFSET = 0.08f;
    
    // Long range attack
    public GameObject longMicrophone;
    private const float LONG_ATTACK_WINDUP = 0.33f;
    private const float LONG_ATTACK_COOLDOWN = 0.47f;

    // Super attack
    public GameObject table;

    // Yes
    public const float WE_DONT_LIKE_EYE_LASER_MICROPHONES = -0.84f;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.FindWithTag("Boss");
        shortRangeHitbox.SetActive(false);
        ps = GameObject.FindWithTag("Player").GetComponent<Player>();
        attacking = false;
	}

	// Update is called once per frame
	void Update ()
	{

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
        yield return new WaitForSeconds(0.1f);
        float drumstickYReductionWhenCrouched = 0f;
        if (ps.isCrouching()) drumstickYReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_MICROPHONES;

    	// Create projectile (mic)
    	GameObject micClone = Instantiate(longMicrophone, transform.position +
            new Vector3(MIC_FORWARD_OFFSET, drumstickYReductionWhenCrouched, 0),
            transform.parent.rotation);
        ExplosiveMic mScript = micClone.GetComponent<ExplosiveMic>();
        mScript.playerRotationY = transform.parent.rotation.y;

        yield return null;
    }

    // Maybe super bleeds / distracts boss if it hits?
    // (Disrupts attack patterns but that would require attack patterns to be created first)
    public override IEnumerator AttackSuper()
    {
        yield return new WaitForSeconds(1f);

        float startX = -12f, startY = 13f;

        while (startX < 10f)
        {
            GameObject curTable = Instantiate(table);
            curTable.transform.position = new Vector3(startX, startY, 0f);
            startX += 5f;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
