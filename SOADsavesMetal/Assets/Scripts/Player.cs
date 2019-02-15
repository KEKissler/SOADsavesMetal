using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    public bool dead;
    private Rigidbody2D rb;
    public float jumpHeight;
    public float speed;

    //Player Hitbox Variables
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;

    //Player State
    private int remainingJumps;
    private bool crouched;
    private bool inAir;
    private bool landing;
    private bool attacking;

    //Player Animation Variables
    private Animator playerAnim;
    private Animator shortRange;
    private SpriteRenderer playerSprite;
    private bool deathStarted;

    // Use this for initialization
    void Start () {
        attacking = false;
        dead = false;
        deathStarted = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponent<Animator>();
        shortRange = shortRangeHitbox.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        remainingJumps = 1;
        inAir = false;
        crouched = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            //Falling and Jumping Animations
            if (rb.velocity.y < -0.5f)
            {
                landing = true;
                playerAnim.Play("JohnFall");
            }
            if (rb.velocity.y > 0.5)
            {
                playerAnim.Play("JohnJump");
            }

            //Crouching
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

            //Jump and Double Jump
            if (!crouched && Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
            {
                remainingJumps -= 1;
                if (inAir && playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJump")
                {
                    playerAnim.Play("JohnJump");
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                playerAnim.Play("JohnJump2");
            }

            //Attacks
            //Z: Short Range Attack    X: Long Range Attack    C: Super Attack
            if(Input.GetKeyDown(KeyCode.Z) && !attacking && !inAir)
            {
                StartCoroutine("shortRangeCooldown");
            }
            else if(Input.GetKeyDown(KeyCode.X) && !attacking)
            {
                attacking = true;
                StartCoroutine("longRangeCooldown");
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine("superCooldown");
            }

            //Movement
            //Left and Right Arrow Keys: Movement in respective directions
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (gameObject.transform.rotation.y != 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
                if (!inAir)
                {
                    playerAnim.Play("JohnWalk");
                }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (gameObject.transform.rotation.y == 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
                if (!inAir)
                {
                    playerAnim.Play("JohnWalk");
                }
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (!inAir)
            {
                if (!crouched && !attacking)
                {
                    playerAnim.Play("JohnIdle");
                }
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

        }
        else
        {
            //Death Animation
            if (!deathStarted)
            {
                StartCoroutine("Kill");
                deathStarted = true;
            }
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

    public IEnumerator shortRangeCooldown()
    {
        attacking = true;
        playerAnim.Play("JohnShort");
        shortRange.Play("SoundWave");
        yield return new WaitForSeconds(0.66f);
        shortRange.Play("BaseSound");
        attacking = false;
    }

    public IEnumerator longRangeCooldown()
    {
        //GameObject projectile = Instantiate(stick, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
        //projectile.GetComponent<DrumStick>().SendMessage("Fire");
        yield return new WaitForSeconds(1.0f);
        attacking = false;
    }

    public IEnumerator superCooldown()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Kill()
    {
        playerAnim.Play("JohnDeath");
        yield return new WaitForSeconds(1.0f);
    }
}
