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
    public GameObject superObject;

    // Copied from JohnAttack
    // Amount to lower the spawn location of projectiles when Daron is crouching
    public const float WE_DONT_LIKE_EYE_LASER_DRUMSTICKS = -0.84f;
    
	// Use this for initialization
	void Start () {
        ps = GameObject.FindWithTag("Player").GetComponent<Player>();
        superObject.SetActive(false);
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

    	Instantiate(stringBreak, transform.parent.position +
            new Vector3(0.7f * getDirection(transform.parent), yReductionWhenCrouched, 0),
            transform.parent.rotation);
        Instantiate(stringBreak, transform.parent.position +
            new Vector3(0.7f * getDirection(transform.parent), yReductionWhenCrouched, 0),
            transform.parent.rotation * Quaternion.Euler(0, 0, 40f));
        Instantiate(stringBreak, transform.parent.position +
            new Vector3(0.7f * getDirection(transform.parent), yReductionWhenCrouched, 0),
            transform.parent.rotation * Quaternion.Euler(0, 0, -40f));

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
        superObject.SetActive(true);
        superObject.SendMessage("refreshHit");
        superObject.transform.position = ps.transform.position;

        for (int i=0; i<16; ++i)
        {
            attackTimer = 0f;
            while (attackTimer < 0.125f)
            {
                attackTimer += Time.deltaTime;
                yield return null;
            }
            transform.position += transform.right * 1.25f;
        }

        superObject.transform.position = ps.transform.position;
        superObject.SetActive(false);

        yield return null;
    }
}
