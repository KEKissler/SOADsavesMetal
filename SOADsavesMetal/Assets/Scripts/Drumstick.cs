using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {Default, Right, Down, Left, Up};

public class Drumstick : MonoBehaviour {
 
	public float velocity = 23.4f;
	public Direction direction = Direction.Default;
	public bool rotateWithPlayer = true;
	public int damage = 26;

	private Rigidbody2D rb;
	private const float LIFESPAN = 0.75f;

	// Use this for initialization
	void Start () {
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
			case Direction.Down:
				rb.velocity = new Vector2(0f, -velocity);
				break;
			case Direction.Left:
				rb.velocity = new Vector2(-velocity, 0f);
				break;
			case Direction.Up:
				rb.velocity = new Vector2(0f, velocity);
				break;		
		}
		
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
		if(col.gameObject.tag == "Boss")	col.gameObject.SendMessage("hit", damage);
	}
}
