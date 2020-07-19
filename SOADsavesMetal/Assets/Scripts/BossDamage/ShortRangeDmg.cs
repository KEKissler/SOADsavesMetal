using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles both parrying and short range damage against bosses.
 * These two features could probably be separated.
 * Damage against bosses is similar between short range and long range attacks, so
 * a single script could possibly handle any type of damage against bosses.
 */
public class ShortRangeDmg : MonoBehaviour {

    public Player ps;
    public bool isSuper;
    public float meterCharge = 0f;
	public int damage = 111;
	bool canHit = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Handles hitting bosses and interactive objects
	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag == "BossHittable")
        {
            if (isSuper)
            {
                float dmgMult = col.gameObject.GetComponent<BossHit>().damageMultiplier;
                int scaledDamage = damage;

                if (dmgMult > 0 && dmgMult < 1) scaledDamage = (int)(damage / (Mathf.Pow(dmgMult, 0.5f) + 0.01f));
                if (dmgMult > 0 && canHit)
                {
                    col.gameObject.SendMessage("hit", scaledDamage);
                    canHit = false;
                }
            }
            else
            {
                col.gameObject.SendMessage("hit", damage);
                float dmgMult = col.gameObject.GetComponent<BossHit>().damageMultiplier;
                //Debug.Log("yee " + dmgMult);
                canHit = false;
                ps.superMeterCharge += meterCharge * dmgMult;
            }
        }
        else if (col.gameObject.tag == "Projectile") ;
        //Destroy(col.gameObject);
        else if (col.gameObject.tag == "ShortRangeHittable")
        {
            col.gameObject.SendMessage("hit");
        }
	}

	void OnTriggerStay2D(Collider2D col)
	{
        if (col.gameObject.tag == "BossHittable" && canHit)
        {
            if (isSuper)
            {
                float dmgMult = col.gameObject.GetComponent<BossHit>().damageMultiplier;
                int scaledDamage = damage;

                if (dmgMult > 0 && dmgMult < 1) scaledDamage = (int) (damage / (Mathf.Pow(dmgMult, 0.5f) + 0.01f));
                if (dmgMult > 0 && canHit)
                {
                    col.gameObject.SendMessage("hit", scaledDamage);
                    canHit = false;
                }
            }
            else
            {
                col.gameObject.SendMessage("hit", damage);
                float dmgMult = col.gameObject.GetComponent<BossHit>().damageMultiplier;
                //Debug.Log("yee " + dmgMult);
                canHit = false;
                ps.superMeterCharge += meterCharge * dmgMult;
            }
        }
        else if (col.gameObject.tag == "Projectile") ;
        //Destroy(col.gameObject);
        else if (col.gameObject.tag == "ShortRangeHittable")
        {
            col.gameObject.SendMessage("hit");
        }
	}

    // Deprecated useless function
    int getBossHealth(Collider2D col)
    {
        return col.gameObject.GetComponent<BossHit>().healthScript.getHP();
    }

    // Useful function
	void refreshHit()
	{
		canHit = true;
	}
}
