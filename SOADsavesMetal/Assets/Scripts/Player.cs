﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Default Player Variables
    public string currentBandMember;
    public bool Dead { get { return health < 1; } }
    public int Health {get { return health; } set { health = value; } }
    private Rigidbody2D rb;
    public float jumpHeight;
    public int startingHealth;
    private int health;
    public float maxGroundSpeed, groundAccel, groundDecel, groundFrictionDecel;
    public float maxAirSpeed, airAccel, airDecel, airFrictionDecel;

    //Player Hitbox Variables
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;

    //Player State
    private int remainingJumps;
    private bool crouched;
    public bool inAir;
    // private bool landing;
    private bool attacking;
    private bool blockHorizontalMovement;
    public bool moving;

    //Player Animation Variables
    private Animator playerUpperAnim;
    public Animator playerLowerAnim;
    private Animator shortRange;
    private SpriteRenderer playerSprite;
    private bool deathStarted;

    private string GetAnimName(string animSuffix) {
        return currentBandMember + animSuffix;
    }

    // Handles a common use case regarding playing body and leg animations.
    private void PlayAnims(string animSuffix) {
        if (!attacking) {
            playerUpperAnim.Play(GetAnimName(animSuffix));
        }
        playerLowerAnim.Play(GetAnimName(animSuffix + "Legs"));
    }

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

	void Update () {
        if (!Dead) {
            #region Friction
            float speedReductionThisFrame;
            if (inAir)
                speedReductionThisFrame = Time.deltaTime * airFrictionDecel;
            else
                speedReductionThisFrame = Time.deltaTime * groundFrictionDecel;
            if (Mathf.Abs(rb.velocity.x) > speedReductionThisFrame)
            {
                rb.velocity += new Vector2(-1 * Mathf.Sign(rb.velocity.x) * speedReductionThisFrame, 0);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            #endregion Friction

            #region Falling and jumping animations
            if(!blockHorizontalMovement)  // Or any other special condition is in effect
            {
                if (rb.velocity.y < -0.5f)
                {
                    // landing = true;
                    PlayAnims("Fall");
                }
                if (rb.velocity.y > 0.5)
                {
                    PlayAnims("Jump");

                }
            }
            #endregion Falling and jumping animations

            #region Crouching
            if (Input.GetKey(KeyCode.DownArrow) && !inAir && !attacking) {
                if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))) {
                    crouched = true;
                    PlayAnims("Crouch");
                    upperBodyHitbox.SetActive(false);
                }
                else {
                    crouched = false;
                }

            }
            else {
                crouched = false;
                upperBodyHitbox.SetActive(true);
            }
            #endregion Crouching

            #region Jump and double jump
            if (!crouched && Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0) {
                remainingJumps -= 1;
                if (inAir) {
                    PlayAnims("Jump");
                    inAir = true;

                    if (currentBandMember == "John") {// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJumpLegs")
                        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    }
                    else if (currentBandMember == "Shavo") {// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "ShavoJumpLegs")
                        StartCoroutine(Dash());
                    }
                    else if (currentBandMember == "Daron") {
                        StartCoroutine("Teleport");
                        if (gameObject.transform.rotation.y == 0) {
                            rb.velocity = new Vector2(1.5f * jumpHeight, 0.0f);
                        }
                        else {
                            rb.velocity = new Vector2(-1.5f * jumpHeight, 0.0f);
                        }
                    }
                    else if (currentBandMember == "Serj") {

                    }
                }
                else {
                    inAir = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                }
                //playerUpperAnim.Play("JohnJump2");
            }
            #endregion Jump and double jump

            #region Attacks
            //Z: Short Range Attack    X: Long Range Attack    C: Super Attack
            if (Input.GetKeyDown(KeyCode.Z) && !crouched && !attacking) {
                StartCoroutine("shortRangeCooldown");
            }
            else if(Input.GetKeyDown(KeyCode.X) && !attacking) {
                attacking = true;
                StartCoroutine("longRangeCooldown");
            }
            else if(Input.GetKeyDown(KeyCode.C)) {
                StartCoroutine("superCooldown");
            }
            #endregion Attacks

            HandleHorizontalMovement();
        }
        else {
            //Death animation
            if (!deathStarted) {
                StartCoroutine("Kill");
                deathStarted = true;
            }
        }
    }

    private void HandleHorizontalMovement() {
        if (blockHorizontalMovement) {
            return;
        }

        float oldSpeed = rb.velocity.x; // Grounded can only occur against flat surfaces below player, speed should only be in x dir
        float input = Input.GetAxisRaw("Horizontal");
        if(input != 0f) {
            moving = true;
            float accel, decel, maxSpeed;
            // Handle direction-specific code
            if (input > 0) {
                // Face right if needed
                if (gameObject.transform.rotation.y != 0) {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
            }
            else {
                // Face left if needed
                if (gameObject.transform.rotation.y == 0) {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
            }

            // Set parameters for movement
            if (!inAir && !Dead)
            {
                // Ground movement
                PlayAnims("Walk");
                accel = groundAccel;
                decel = groundDecel;
                maxSpeed = maxGroundSpeed;
            }
            else
            {
                // Air movement
                accel = airAccel;
                decel = airDecel;
                maxSpeed = maxAirSpeed;
            }
            
            // Moving in direction of current velocity or when player was not moving before
            if ((Mathf.Sign(oldSpeed) == Mathf.Sign(input)) || Mathf.Approximately(oldSpeed, 0))
            {
                // If applying max accel would not put speed above target limit
                if (Mathf.Abs(oldSpeed + (input * accel * Time.deltaTime)) < maxSpeed)
                {
                    rb.velocity = new Vector3(oldSpeed + (input * accel * Time.deltaTime), rb.velocity.y, 0);
                }
                // Would go beyond limit
                else
                {
                    // Set velocity to either the targetWalkSpeed or leave it untouched if player was already traveling faster
                    if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                    {
                        // Debug.Log("Set to target walk speed!");
                        rb.velocity = new Vector2(Mathf.Sign(oldSpeed) * maxSpeed, rb.velocity.y);
                    }
                }
            }
            // Fighting velocity
            else
            {
                //no check needed, losing speed and minimum is zero.
                rb.velocity = new Vector2(oldSpeed + (input * decel * Time.deltaTime), rb.velocity.y);
            }

        }
        else if (!inAir && !Dead) {
            // moving = false;
            if (!crouched && !attacking && !Dead) {
                playerUpperAnim.Play(GetAnimName("Idle"));
                playerLowerAnim.Play(GetAnimName("IdleLegs"));
            }
            else if (attacking) {
                playerLowerAnim.Play(GetAnimName("LandLegs"));
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.tag == "Floor" && !Dead) {
            playerLowerAnim.Play(GetAnimName("LandLegs"));
        }
    }

    public void OnCollisionStay2D(Collision2D coll) {
        if(coll.collider.tag == "Floor") {
            inAir = false;
            // landing = false;
            remainingJumps = 1;
        }
    }

    public void OnCollisionExit2D(Collision2D coll) {
        if(coll.collider.tag == "Floor") {
            inAir = true;
        }
    }

    public void PlayerFreeze() {
        
    }

    public IEnumerator shortRangeCooldown() {
        attacking = true;
        if (crouched) {
            //playerSprite.sprite.pivot.Set
            playerUpperAnim.pivotPosition.Set(0.49f, 0.83f, 0.0f);
        }
        playerUpperAnim.Play(GetAnimName("Short"));
        if (!moving && !crouched && !inAir && !Dead) {
            playerLowerAnim.Play(GetAnimName("ShortLegs"));
        }
        if (currentBandMember == "John") {
            shortRange.Play("SoundWave");
            yield return new WaitForSeconds(1.0f);
            shortRange.Play("BaseSound");
            attacking = false;
        }
        else if (currentBandMember == "Shavo") {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }
        else if (currentBandMember == "Daron") {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }
        else if (currentBandMember == "Serj") {
            yield return new WaitForSeconds(0.5f);
            attacking = false;
        }

    }

    public IEnumerator Dash() {
        float dashPower = 18f;
        blockHorizontalMovement = true;
        PlayAnims("Dash");
        rb.velocity = new Vector2(0, 0.1f);
        yield return new WaitForSeconds(0.12f);
        var playerRotation = gameObject.transform.rotation;
        rb.velocity = new Vector2((playerRotation.y == 0 ? 1 : -1) * 1.6f * dashPower, 0.4f * dashPower);
        yield return new WaitForSeconds(0.22f);
        rb.velocity *= 0.2f;
        blockHorizontalMovement = false;

    }

    public IEnumerator blockMovement(float duration)
    {
        blockHorizontalMovement = true;
        float timer = 0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        blockHorizontalMovement = false;
    }

    public IEnumerator Teleport() {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.125f);
        upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = true ;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0.0f, 0.0f);
    }

    public IEnumerator longRangeCooldown() {
        //GameObject projectile = Instantiate(stick, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
        //projectile.GetComponent<DrumStick>().SendMessage("Fire");
        if (currentBandMember == "Shavo") {
            playerUpperAnim.Play("ShavoLong");
            if (!moving && !crouched && !inAir) {
                playerLowerAnim.Play("ShavoLongLegs");
            }
        }
        else if (currentBandMember == "Daron") {
            playerUpperAnim.Play("DaronLong");
            if (!moving && !crouched && !inAir) {
                playerLowerAnim.Play("DaronAttackLegs");
            }
        }
        else if (currentBandMember == "Serj") {
            playerUpperAnim.Play("SerjLong");
            if (!moving && !crouched && !inAir) {
                playerLowerAnim.Play("SerjLongLegs");
            }
        }
        yield return new WaitForSeconds(1.0f);
        attacking = false;
    }

    public IEnumerator superCooldown() {
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Kill() {
        playerUpperAnim.Play(GetAnimName("Death"));
        playerLowerAnim.Play("ShavoDashLegs");
        yield return new WaitForSeconds(1.0f);
    }
}
