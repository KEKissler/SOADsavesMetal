using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoSuperTinyRock : MonoBehaviour
{
    public ShavoSuperRocks rockManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BossHittable")
        {
            rockManager.hitBoss(col);
        }
    }
}
