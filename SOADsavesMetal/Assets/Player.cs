using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    private Rigidbody2D rb;
    public float jumpHeight;
    public float speed;

    //Player Hitbox Variables
    public GameObject upperBodyHitbox;

    //Player Movement State
    private int remainingJumps;
    private bool crouched;
    private bool inAir;
    private bool landing;

    //Player Animation Variables
    private Animator playerAnim;
    private SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        remainingJumps = 1;
        inAir = false;
        crouched = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.y < -0.5f)
        {
            landing = true;
            playerAnim.Play("JohnFall");
        }
        if(rb.velocity.y >0.5)
        {
            playerAnim.Play("JohnJump");
        }

        //if(inAir)

        if (Input.GetKey(KeyCode.DownArrow) && !inAir)
        { 
            if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                crouched = true;
                playerAnim.Play("JohnCrouch");
                upperBodyHitbox.SetActive(false);
            }
            
        }
        else
        {
            crouched = false;
            upperBodyHitbox.SetActive(true);
        }

        if(!crouched && Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            remainingJumps -= 1;
            if (inAir && playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJump")
            {
                playerAnim.Play("JohnJump");
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            playerAnim.Play("JohnJump2");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (playerSprite.flipX == true)
            {
                playerSprite.flipX = false;
            }
            if (!inAir) {
                playerAnim.Play("JohnWalk");
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (playerSprite.flipX == false)
            {
                playerSprite.flipX = true;
            }
            if (!inAir)
            {
                playerAnim.Play("JohnWalk");
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if(!inAir)
        {
            if (!crouched)
            {
                playerAnim.Play("JohnIdle");
            }
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.collider.tag == "Floor")
        {
            inAir = false;
            landing = false;
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
