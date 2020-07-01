using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerjAttack : PlayerAttack
{
    GameObject boss;

	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    // Should target nearby enemy if in range, otherwise shoots straight forward
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_WINDUP = 0.04f;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.4f;
    private const float SHORT_ATTACK_COOLDOWN = 0.01f;
    public const float MIC_FORWARD_OFFSET = 0.08f;
    
    // Long range attack
    public GameObject longMicrophone;
    private const float LONG_ATTACK_WINDUP = 0.33f;
    private const float LONG_ATTACK_COOLDOWN = 0.47f;

    // Super attack
    public GameObject table;
    public GameObject bigTable;

    // Yes
    public const float WE_DONT_LIKE_EYE_LASER_MICROPHONES = -0.84f;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.FindWithTag("Boss");
        shortRangeHitbox.SetActive(false);
	}

	// Update is called once per frame
	void Update ()
	{

	}

    public override IEnumerator AttackShort()
    {

        yield return new WaitForSeconds(SHORT_ATTACK_WINDUP);

        shortRangeHitbox.SetActive(true);
        yield return new WaitForSeconds(SHORT_ATTACK_HOLD_DURATION);
        shortRangeHitbox.SendMessage("refreshHit");
        shortRangeHitbox.SetActive(false);
    }

    public override IEnumerator AttackLong()
    {
        yield return new WaitForSeconds(0.1f);
        float drumstickYReductionWhenCrouched = 0f;
        //if (ps.isCrouching()) drumstickYReductionWhenCrouched = WE_DONT_LIKE_EYE_LASER_MICROPHONES;

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
        yield return new WaitForSeconds(0.35f);

        float startX = -15f, startY = 11f;

        while (startX < 10f)
        {
            GameObject curTable = Instantiate(table);
            curTable.transform.position = new Vector3(startX, startY, 0f);
            startX += 4.9f;
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.15f);

        GameObject bTable = Instantiate(bigTable);
        bTable.transform.position = new Vector3(-9f, 28f, 0f);
    }
}
