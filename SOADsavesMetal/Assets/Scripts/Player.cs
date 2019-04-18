using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    public string currentBandMember;
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
    public bool moving;

    //Player Animation Variables
    private Animator playerUpperAnim;
    public Animator playerLowerAnim;
    private Animator shortRange;
    private SpriteRenderer playerSprite;
    private bool deathStarted;

    // Use this for initialization
    void Start () {
        moving = false;
        attacking = false;
        dead = false;
        deathStarted = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerUpperAnim = gameObject.GetComponent<Animator>();
        shortRange = shortRangeHitbox.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        remainingJumps = 1;
        inAir = false;
        crouched = false;
        if(currentBandMember == "")
        {
            currentBandMember = "John";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            //Falling and Jumping Animations
            if (rb.velocity.y < -0.5f)
            {
                landing = true;
                if (!attacking)
                {
                    if (currentBandMember == "John")
                    {
                        playerUpperAnim.Play("JohnFall");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerUpperAnim.Play("ShavoFall");
                    }
                }
                if (currentBandMember == "John")
                {
                    playerLowerAnim.Play("JohnFallLegs");
                }
                else if (currentBandMember == "Shavo")
                {
                    playerLowerAnim.Play("ShavoFallLegs");
                }
            }
            if (rb.velocity.y > 0.5)
            {
                if (!attacking)
                {
                    if (currentBandMember == "John")
                    {
                        playerUpperAnim.Play("JohnJump");
                    }
                    else if(currentBandMember == "Shavo")
                    {
                        playerUpperAnim.Play("ShavoJump");
                    }
                }
                if (currentBandMember == "John")
                {
                    playerLowerAnim.Play("JohnJumpLegs");
                }
                else if (currentBandMember == "Shavo")
                {
                    playerLowerAnim.Play("ShavoJumpLegs");
                }
                
            }

            //Crouching
            if (Input.GetKey(KeyCode.DownArrow) && !inAir && !attacking)
            {
                if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
                {
                    crouched = true;
                    if (!attacking)
                    {
                        if (currentBandMember == "John")
                        {
                            playerUpperAnim.Play("JohnCrouch");
                        }
                        else if (currentBandMember == "Shavo")
                        {
                            playerUpperAnim.Play("ShavoCrouch");
                        }
                    }
                    if (currentBandMember == "John")
                    {
                        playerLowerAnim.Play("JohnCrouchLegs");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerLowerAnim.Play("ShavoCrouchLegs");
                    }
                    upperBodyHitbox.SetActive(false);
                }
                else
                {
                    crouched = false;
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
                if (inAir)
                {
                    if (currentBandMember == "John" && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJumpLegs")
                    {
                        if (!attacking)
                        {
                            playerUpperAnim.Play("JohnJump");
                        }
                        playerLowerAnim.Play("JohnJumpLegs");
                        inAir = true;
                        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    }
                    else if (currentBandMember == "Shavo" && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "ShavoJumpLegs")
                    {
                        if (!attacking)
                        {
                            playerUpperAnim.Play("ShavoDash");
                        }
                        playerLowerAnim.Play("ShavoDashLegs");
                        inAir = true;
                        StartCoroutine("Dash");
                        if (gameObject.transform.rotation.y == 0)
                        {
                            rb.velocity = new Vector2(1.5f * jumpHeight, 0.0f);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-1.5f * jumpHeight, 0.0f);
                        }
                    }
                }
                else
                {
                    inAir = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                }
                //playerUpperAnim.Play("JohnJump2");
            }

            //Attacks
            //Z: Short Range Attack    X: Long Range Attack    C: Super Attack
            if(Input.GetKeyDown(KeyCode.Z) && !crouched && !attacking)
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
                moving = true;
                if (gameObject.transform.rotation.y != 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
                if (!inAir)
                {
                    if (!attacking)
                    {
                        if (currentBandMember == "John")
                        {
                            playerUpperAnim.Play("JohnWalk");
                        }
                        else if (currentBandMember == "Shavo")
                        {
                            playerUpperAnim.Play("ShavoWalk");
                        }
                    }
                    if (currentBandMember == "John")
                    {
                        playerLowerAnim.Play("JohnWalkLegs");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerLowerAnim.Play("ShavoWalkLegs");
                    }
                }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                moving = true;
                if (gameObject.transform.rotation.y == 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
                if (!inAir)
                {
                    if (!attacking)
                    {
                        if (currentBandMember == "John")
                        {
                            playerUpperAnim.Play("JohnWalk");
                        }
                        else if (currentBandMember == "Shavo")
                        {
                            playerUpperAnim.Play("ShavoWalk");
                        }
                    }
                    if (currentBandMember == "John")
                    {
                        playerLowerAnim.Play("JohnWalkLegs");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerLowerAnim.Play("ShavoWalkLegs");
                    }
                }
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (!inAir)
            {
                moving = false;
                if (!crouched && !attacking)
                {
                    if (currentBandMember == "John")
                    {
                        playerUpperAnim.Play("JohnIdle");
                        playerLowerAnim.Play("JohnIdleLegs");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerUpperAnim.Play("ShavoIdle");
                        playerLowerAnim.Play("ShavoIdleLegs");
                    }
                }
                else if (attacking)
                {
                    if (currentBandMember == "John")
                    {
                        playerLowerAnim.Play("JohnLandLegs");
                    }
                    else if (currentBandMember == "Shavo")
                    {
                        playerLowerAnim.Play("ShavoLandLegs");
                    }
                    
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

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor")
        {
            if (currentBandMember == "John")
            {
                playerLowerAnim.Play("JohnLandLegs");
            }
            else if (currentBandMember == "Shavo")
            {
                playerLowerAnim.Play("ShavoLandLegs");
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
        if (crouched)
        {
            //playerSprite.sprite.pivot.Set
            playerUpperAnim.pivotPosition.Set(0.49f, 0.83f, 0.0f);
        }
        playerUpperAnim.Play("JohnShort");
        if (!moving && !crouched && !inAir)
        {
            playerLowerAnim.Play("JohnShortLegs");
        }
        shortRange.Play("SoundWave");
        yield return new WaitForSeconds(1.0f);
        shortRange.Play("BaseSound");
        attacking = false;
    }

    public IEnumerator Dash()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.125f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0.0f, 0.0f);


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
        if (currentBandMember == "John")
        {
            playerUpperAnim.Play("JohnDeath");
            playerLowerAnim.Play("ShavoDashLegs");
        }
        else if (currentBandMember == "Shavo")
        {
            playerUpperAnim.Play("ShavoDeath");
            playerLowerAnim.Play("ShavoDashLegs");
        }
        yield return new WaitForSeconds(1.0f);
    }
}
