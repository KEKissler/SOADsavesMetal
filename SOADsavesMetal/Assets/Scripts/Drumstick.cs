using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour {

	private Rigidbody2D rb;
	private const float DEFAULT_VELOCITY = 45f;
	private const float LIFESPAN = 0.4f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(DEFAULT_VELOCITY, 0f);
		Destroy(gameObject, LIFESPAN);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		col.gameObject.SendMessage("hit");
	}
}
