using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cymbal : PlayerProjectile {

    private const float DEFAULT_VELOCITY = 38.5f;
    private const int VELOCITY_VARIATION = 6;
    private System.Random rng = new System.Random();

    // Use this for initialization
    void Start () {
        damage = 16;
        rb = GetComponent<Rigidbody2D>();
        float angle = Mathf.Deg2Rad*transform.rotation.y*-180f;

        rb.velocity = new Vector2(
            DEFAULT_VELOCITY*Mathf.Cos(angle)+rng.Next(2*VELOCITY_VARIATION+1)-(float)VELOCITY_VARIATION,
            DEFAULT_VELOCITY*Mathf.Sin(angle)-3.5f+6.8f*(float)rng.NextDouble()+6.8f*(float)rng.NextDouble());
        speed = rb.velocity.magnitude;

        DestroyAfter(26f / speed);
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
