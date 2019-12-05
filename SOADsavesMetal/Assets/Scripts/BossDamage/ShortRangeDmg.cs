﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles both parrying and short range damage against bosses.
 * These two features could probably be separated.
 * Damage against bosses is similar between short range and long range attacks, so
 * a single script could possibly handle any type of damage against bosses.
 */
public class ShortRangeDmg : MonoBehaviour {

	public int damage = 111;
	bool canHit = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Handles hitting bosses and interactive objects
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "BossHittable" && canHit) {
			col.gameObject.SendMessage("hit", damage);
			canHit = false;
		}
		else if(col.gameObject.tag == "Projectile")
			Destroy(col.gameObject);
		else if(col.gameObject.tag == "ShortRangeHittable") {
			col.gameObject.SendMessage("hit");
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "BossHittable" && canHit)
		{
			col.gameObject.SendMessage("hit", damage);
			canHit = false;
		}
		else if(col.gameObject.tag == "Projectile")
			Destroy(col.gameObject);
		else if(col.gameObject.tag == "ShortRangeHittable") {
			col.gameObject.SendMessage("hit");
		}
	}

	void refreshHit()
	{
		canHit = true;
	}
}