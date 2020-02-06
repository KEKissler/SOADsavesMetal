﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : PlayerProjectile {
 
	// Use this for initialization
	void Start () {
        damage = 26;
        speed = 23.4f;
		rb = GetComponent<Rigidbody2D>();

        float angle = Mathf.Deg2Rad * transform.rotation.y * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

		DestroyWhenOffScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
