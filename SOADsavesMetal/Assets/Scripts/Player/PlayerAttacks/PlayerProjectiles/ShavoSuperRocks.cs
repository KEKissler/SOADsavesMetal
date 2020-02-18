using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoSuperRocks : PlayerProjectile
{
    private bool hasHitBoss;

    // Start is called before the first frame update
    void Start()
    {
        hasHitBoss = false;
        DestroyAfter(2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitBoss(Collider2D col)
    {
        if (!hasHitBoss)
        {
            hasHitBoss = true;
            col.gameObject.SendMessage("hit", damage);
        }
    }
}
