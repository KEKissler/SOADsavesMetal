using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoSuperRocks : PlayerProjectile
{
    private bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
        DestroyAfter(3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitBoss(Collider2D col)
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
}
