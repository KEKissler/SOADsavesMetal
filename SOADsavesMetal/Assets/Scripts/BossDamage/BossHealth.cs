using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Instructions to use the BossHealth script:
 * 
 * Attach this script to the root object of the boss.
 * For each hittable segment of the boss, tag that segment with "BossHittable" and give it the script "BossHit".
 * Change the damage multiplier on BossHit if desired.
 * 
 * The damage multiplier in this script is a global multiplier that affects all parts.
 * DamageMultiplier in BossHit only affects that particular component.
 */

public struct Phase
{
    int startHealth;
    string name;
}

public class BossHealth : MonoBehaviour
{
    // Public
    public float startingHP = 2000f;
    public float damageMultiplier = 1f;
    public BossAttackManager<AgasPhase> agasAttackManager;
    public BossAttackManager<TsovinarPhase> tsovinarAttackManager;
    //public BossAttackManager<NhangPhase> nhangAttackManager;

    // Should be accessible but not modifiable directly
    private float HP;

    // Start is called before the first frame update
    void Start()
    {
        BossAttackManager<AgasPhase> temp1 = gameObject.GetComponentInChildren<AgasAttackManager>();
        if(temp1)
        {
            agasAttackManager = temp1;
        }
        BossAttackManager<TsovinarPhase> temp2 = gameObject.GetComponentInChildren<TsovinarAttackManager>();
        if (temp2)
        {
            tsovinarAttackManager = temp2;
        }
        HP = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitF(float damage)
    {
        HP -= damage * damageMultiplier;
        if(agasAttackManager)
        {
            agasAttackManager.bossHit();
        }
        else if (tsovinarAttackManager)
        {
            tsovinarAttackManager.bossHit();
        }
        Debug.Log(HP);
    }

    public void changeMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    public int getHP()
    {
        return (int)(HP + 0.5f);
    }

    public float getHPPercentage()
    {
        return (float)HP/(float)startingHP * 100f;
    }
}
