using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalk : MonoBehaviour {

	private Rigidbody2D rb;
	private float timer;
	private const float BASE_VELOCITY = 1.5f;
	private const float TIMER_MAX = 1.4f;
	private bool hasBeenHit = false;

	private const float HIT_COOLDOWN = 0.5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		timer = 0.0f;
		rb.velocity = new Vector2(-BASE_VELOCITY, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		timer += Time.deltaTime;
		if(timer > TIMER_MAX)
		{
			timer = 0.0f;
			rb.velocity = new Vector2(-1.0f * BASE_VELOCITY * Mathf.Sign(rb.velocity.x), 0.0f);
		}
	}

	void hit()
	{	
		if(!hasBeenHit)
		{
			hasBeenHit = true;
			print("I've been hit");
			StartCoroutine(hitCooldown());
		}
	}

	IEnumerator hitCooldown()
	{
		float t = 0.0f;
		while(t < HIT_COOLDOWN)
		{
			t += Time.deltaTime;
			yield return null;
		}

		hasBeenHit = false;
	}
}
