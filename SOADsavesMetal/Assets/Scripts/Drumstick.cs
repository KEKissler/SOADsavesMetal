using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour {
 
	private Rigidbody2D rb;
	private const float DEFAULT_VELOCITY = 33.4f;
	private const float LIFESPAN = 0.65f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

		float angle = Mathf.Deg2Rad*transform.rotation.y*-180f;

		rb.velocity = new Vector2(DEFAULT_VELOCITY*Mathf.Cos(angle), DEFAULT_VELOCITY*Mathf.Sin(angle));
		Destroy(gameObject, LIFESPAN);
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
		if(col.gameObject.tag == "Boss")	col.gameObject.SendMessage("hit", 17);
	}
}
