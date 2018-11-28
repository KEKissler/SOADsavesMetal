using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    private Rigidbody2D rb;
    public float jumpHeight;
    public float speed;

    //Player Movement State
    private int remainingJumps;
    private bool crouched;
    private bool inAir;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        remainingJumps = 1;
        inAir = false;
        crouched = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            crouched = true;
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else
        {
            crouched = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(!crouched && Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            remainingJumps -= 1;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if(!inAir)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.collider.tag == "Floor")
        {
            inAir = false;
            remainingJumps = 1;
        }
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.collider.tag == "Floor")
        {
            inAir = true;
        }
    }

    public void PlayerFreeze()
    {
        
    }
}
