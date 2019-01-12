using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cymbal : MonoBehaviour {

    private Rigidbody2D rb;
    private const float DEFAULT_VELOCITY = 40f;
    private const int VELOCITY_VARIATION = 5;
    private const float LIFESPAN = 0.5f;
    private System.Random rng = new System.Random();

    // Testing code for reflectors
    private bool reflected = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(DEFAULT_VELOCITY+rng.Next(2*VELOCITY_VARIATION+1)-(float)VELOCITY_VARIATION, 1.7f+2*(float)rng.NextDouble());
        Destroy(gameObject, LIFESPAN);
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(!(gameObject.tag == "Projectile"))
        col.gameObject.SendMessage("hit");
    }

    void reflect()
    {
        if(!reflected)
        {
            reflected = true;
            
        }
    }
}
