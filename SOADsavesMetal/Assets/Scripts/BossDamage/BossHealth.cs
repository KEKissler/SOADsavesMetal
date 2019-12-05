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
    public static float startingHP = 2000f;
    public float damageMultiplier = 1f;

    // Should be accessible but not modifiable directly
    private float HP = startingHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(float damage)
    {
        HP -= damage * damageMultiplier;
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
