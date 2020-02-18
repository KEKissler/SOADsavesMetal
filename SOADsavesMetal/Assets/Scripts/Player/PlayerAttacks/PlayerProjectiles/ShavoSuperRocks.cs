using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoSuperRocks : PlayerProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 48;
        DestroyAfter(2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
