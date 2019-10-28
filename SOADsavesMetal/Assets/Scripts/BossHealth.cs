using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Phase
{
    int startHealth;
    string name;
}

public class BossHealth : MonoBehaviour
{
    // Public
    public static int startingHP = 2000;
    public float damageMultiplier = 1;

    // Should be accessible but not modifiable directly
    private int HP = startingHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(int damage)
    {
        HP -= (int)(damage * damageMultiplier + 0.5f);
        Debug.Log(HP);
    }

    public void changeMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    public int getHP()
    {
        return HP;
    }

    public float getHPPercentage()
    {
        return (float)HP/(float)startingHP * 100f;
    }
}
