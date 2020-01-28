using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : PlayerProjectile {
 
	public bool rotateWithPlayer = true;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        setDamage(26);
        setVelocity(23.4f);
        setDirection(Direction.Default);
		rb = GetComponent<Rigidbody2D>();
		if(!rotateWithPlayer)	transform.rotation = Quaternion.identity;

		switch(direction)
		{
			case Direction.Default:
				float angle = Mathf.Deg2Rad*transform.rotation.y*-180f;
				rb.velocity = new Vector2(velocity*Mathf.Cos(angle), velocity*Mathf.Sin(angle));
				break;
			case Direction.Right:
				rb.velocity = new Vector2(velocity, 0f);
				break;
			case Direction.Left:
				rb.velocity = new Vector2(-velocity, 0f);
				break;
		}
		
		DestroyWhenOffScreen();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// void OnTriggerStay2D(Collider2D col)
	// {
	// 	if(col.gameObject.name == "TheWorstEnemyImaginable")
	// 		col.gameObject.SendMessage("hit");
	// }

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "BossHittable")	col.gameObject.SendMessage("hit", damage);
	}
}
