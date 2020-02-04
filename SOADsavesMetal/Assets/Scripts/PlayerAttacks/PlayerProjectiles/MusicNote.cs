using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : PlayerProjectile
{
    public float playerRotationY;
    // Start is called before the first frame update
    void Start()
    {
        damage = 40;
        speed = 6f;
        rb = GetComponent<Rigidbody2D>();

        float angle = Mathf.Deg2Rad * playerRotationY * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        DestroyWhenOffScreen();
    }
}
