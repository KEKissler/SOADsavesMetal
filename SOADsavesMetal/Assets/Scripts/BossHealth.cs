using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    // Public
    public int startingHP = 20000;

    // Should be accessible but not modifiable directly
    int HP;

    // Start is called before the first frame update
    void Start()
    {
        HP = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }
}
