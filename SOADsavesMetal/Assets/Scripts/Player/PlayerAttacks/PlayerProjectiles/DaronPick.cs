using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaronPick : PlayerProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 24f;
		rb = GetComponent<Rigidbody2D>();

        float angle = Mathf.Deg2Rad * transform.rotation.y * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        DestroyAfter(25f / speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
