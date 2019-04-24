using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cymbal : MonoBehaviour {

    private Rigidbody2D rb;
    private const float DEFAULT_VELOCITY = 38.5f;
    private const int VELOCITY_VARIATION = 6;
    private const float LIFESPAN = 0.58f;
    private System.Random rng = new System.Random();

    // Testing code for reflectors
    private bool reflected = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        float angle = Mathf.Deg2Rad*transform.rotation.y*-180f;

		rb.velocity = new Vector2(DEFAULT_VELOCITY*Mathf.Cos(angle), DEFAULT_VELOCITY*Mathf.Sin(angle));
        rb.velocity = new Vector2(DEFAULT_VELOCITY*Mathf.Cos(angle)+rng.Next(2*VELOCITY_VARIATION+1)-(float)VELOCITY_VARIATION,
            DEFAULT_VELOCITY*Mathf.Sin(angle)-6.5f+8.3f*(float)rng.NextDouble()+8.3f*(float)rng.NextDouble());
        Destroy(gameObject, LIFESPAN);
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        
    }

    // void OnTriggerStay2D(Collider2D col)
    // {
    //     if(col.gameObject.tag != "Projectile")
    //     {
    //         //Debug.Log(col.gameObject.name);
    //         //col.gameObject.SendMessage("hit");
    //     }
    // }

    void reflect()
    {
        if(!reflected)
        {
            reflected = true;
            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Boss")	col.gameObject.SendMessage("hit", 29);
	}
}
