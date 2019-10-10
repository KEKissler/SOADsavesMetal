using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    public string currentBandMember;
    public bool Dead { get { return health < 1; } }
    public int Health {get { return health; } set { health = value; } }
    private Rigidbody2D rb;
    public float jumpHeight;
    public float speed;
    public int startingHealth;
    private int health;

    //Player Hitbox Variables
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;

    //Player State
    private int remainingJumps;
    private bool crouched;
    public bool inAir;
    private bool landing;
    private bool attacking;
    private bool blockHorizontalMovement;
    public bool moving;

    //Player Animation Variables
    private Animator playerUpperAnim;
    public Animator playerLowerAnim;
    private Animator shortRange;
    private SpriteRenderer playerSprite;
    private bool deathStarted;

    // Use this for initialization
    void Start () {
        health = startingHealth;
        moving = false;
        attacking = false;
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
        if (!Dead)
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
                    else if (currentBandMember == "Daron")
                    {
                        playerUpperAnim.Play("DaronFall");
                    }
                    else if (currentBandMember == "Serj")
                    {
                        playerUpperAnim.Play("SerjFall");
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
                else if (currentBandMember == "Daron")
                {
                    playerLowerAnim.Play("DaronFallLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerLowerAnim.Play("SerjFallLegs");
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
                    else if (currentBandMember == "Daron")
                    {
                        playerUpperAnim.Play("DaronJump");
                    }
                    else if (currentBandMember == "Serj")
                    {
                        playerUpperAnim.Play("SerjJump");
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
                else if (currentBandMember == "Daron")
                {
                    playerLowerAnim.Play("DaronJumpLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerLowerAnim.Play("SerjJumpLegs");
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
                        else if (currentBandMember == "Daron")
                        {
                            playerUpperAnim.Play("DaronCrouch");
                        }
                        else if (currentBandMember == "Serj")
                        {
                            playerUpperAnim.Play("SerjCrouch");
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
                    else if (currentBandMember == "Daron")
                    {
                        playerLowerAnim.Play("DaronCrouchLegs");
                    }
                    else if (currentBandMember == "Serj")
                    {
                        playerLowerAnim.Play("SerjCrouchLegs");
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
                    if (currentBandMember == "John")// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJumpLegs")
                    {
                        if (!attacking)
                        {
                            playerUpperAnim.Play("JohnJump");
                        }
                        playerLowerAnim.Play("JohnJumpLegs");
                        inAir = true;
                        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    }
                    else if (currentBandMember == "Shavo")// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "ShavoJumpLegs")
                    {
                        if (!attacking)
                        {
                            playerUpperAnim.Play("ShavoDash");
                        }
                        playerLowerAnim.Play("ShavoDashLegs");
                        inAir = true;
                        StartCoroutine(Dash());
                        Debug.Log(rb.velocity.x);
                        var playerRotation = gameObject.transform.rotation;
                        rb.velocity = new Vector2((playerRotation.y == 0 ? 1 : -1) * 1.5f * jumpHeight, 0.0f);
                    }
                    else if (currentBandMember == "Daron")
                    {
                        if (!attacking)
                        {
                            playerUpperAnim.Play("DaronTeleport");
                        }
                        playerLowerAnim.Play("ShavoDashLegs");
                        inAir = true;
                        StartCoroutine("Teleport");
                        if (gameObject.transform.rotation.y == 0)
                        {
                            rb.velocity = new Vector2(1.5f * jumpHeight, 0.0f);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-1.5f * jumpHeight, 0.0f);
                        }
                    }
                    else if (currentBandMember == "Serj")
                    {

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
            HandleHorizontalMovement();
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

    private void HandleHorizontalMovement()
    {
        if (blockHorizontalMovement)
        {
            return;
        }

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
                    else if (currentBandMember == "Daron")
                    {
                        playerUpperAnim.Play("DaronWalk");
                    }
                    else if (currentBandMember == "Serj")
                    {
                        playerUpperAnim.Play("SerjWalk");
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
                else if (currentBandMember == "Daron")
                {
                    playerLowerAnim.Play("DaronWalkLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerLowerAnim.Play("SerjWalkLegs");
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
                    else if (currentBandMember == "Daron")
                    {
                        playerUpperAnim.Play("DaronWalk");
                    }
                    else if (currentBandMember == "Serj")
                    {
                        playerUpperAnim.Play("SerjWalk");
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
                else if (currentBandMember == "Daron")
                {
                    playerLowerAnim.Play("DaronWalkLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerLowerAnim.Play("SerjWalkLegs");
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
                else if (currentBandMember == "Daron")
                {
                    playerUpperAnim.Play("DaronIdle");
                    playerLowerAnim.Play("DaronIdleLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerUpperAnim.Play("SerjIdle");
                    playerLowerAnim.Play("SerjIdleLegs");
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
                else if (currentBandMember == "Daron")
                {
                    playerLowerAnim.Play("DaronLandLegs");
                }
                else if (currentBandMember == "Serj")
                {
                    playerLowerAnim.Play("SerjLandLegs");
                }

            }
            rb.velocity = new Vector2(0, rb.velocity.y);
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
            else if (currentBandMember == "Daron")
            {
                playerLowerAnim.Play("DaronLandLegs");
            }
            else if (currentBandMember == "Serj")
            {
                playerLowerAnim.Play("SerjLandLegs");
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
        if (currentBandMember == "John")
        {
            playerUpperAnim.Play("JohnShort");
        }
        else if (currentBandMember == "Shavo")
        {
            playerUpperAnim.Play("ShavoShort");
        }
        else if (currentBandMember == "Daron")
        {
            playerUpperAnim.Play("DaronShort");
        }
        else if (currentBandMember == "Serj")
        {
            playerUpperAnim.Play("SerjShort");
        }
        if (!moving && !crouched && !inAir)
        {
            if (currentBandMember == "John")
            {
                playerLowerAnim.Play("JohnShortLegs");
            }
            else if (currentBandMember == "Shavo")
            {
                playerLowerAnim.Play("ShavoShortLegs");
            }
            else if (currentBandMember == "Daron")
            {
                playerLowerAnim.Play("DaronAttackLegs");
            }
            else if (currentBandMember == "Serj")
            {
                playerLowerAnim.Play("SerjShortLegs");
            }
        }
        if (currentBandMember == "John")
        {
            shortRange.Play("SoundWave");
            yield return new WaitForSeconds(1.0f);
            shortRange.Play("BaseSound");
            attacking = false;
        }
        else if (currentBandMember == "Shavo")
        {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }
        else if (currentBandMember == "Daron")
        {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }
        else if (currentBandMember == "Serj")
        {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }

    }

    public IEnumerator Dash()
    {
        blockHorizontalMovement = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.125f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0f, 0.0f);
        blockHorizontalMovement = false;

    }

    public IEnumerator Teleport()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.125f);
        upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = true ;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0.0f, 0.0f);
    }

    public IEnumerator longRangeCooldown()
    {
        //GameObject projectile = Instantiate(stick, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
        //projectile.GetComponent<DrumStick>().SendMessage("Fire");
        if (currentBandMember == "Shavo")
        {
            playerUpperAnim.Play("ShavoLong");
            if (!moving && !crouched && !inAir)
            {
                playerLowerAnim.Play("ShavoLongLegs");
            }
        }
        else if (currentBandMember == "Daron")
        {
            playerUpperAnim.Play("DaronLong");
            if (!moving && !crouched && !inAir)
            {
                playerLowerAnim.Play("DaronAttackLegs");
            }
        }
        else if (currentBandMember == "Serj")
        {
            playerUpperAnim.Play("SerjLong");
            if (!moving && !crouched && !inAir)
            {
                playerLowerAnim.Play("SerjLongLegs");
            }
        }
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
        else if (currentBandMember == "Daron")
        {
            playerUpperAnim.Play("DaronDeath");
            playerLowerAnim.Play("ShavoDashLegs");
        }
        else if (currentBandMember == "Serj")
        {
            playerUpperAnim.Play("SerjDeath");
            playerLowerAnim.Play("ShavoDashLegs");
        }
        yield return new WaitForSeconds(1.0f);
    }
}
