﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public const float MAX_SUPER_CHARGE = 100f;

    //Default Player Variables
    [Header("Player General Properties")]
    public string currentBandMember;
    public bool Dead { get { return health < 1; } }
    public int Health { get { return health; } set { health = value; } }
    public Rigidbody2D rb;
    public float jumpHeight;
    public int startingHealth;
    private int health;
    public float maxGroundSpeed, groundAccel, groundDecel, groundFrictionDecel;
    public float maxAirSpeed, airAccel, airDecel, airFrictionDecel;

    //Player Hitbox Variables
    [Header("Player Hitbox Variables")]
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;

    //Player State
    [Header("Player State")]
    public int remainingJumps;
    public bool crouched;
    public bool inAir;
    // private bool landing;
    public bool attacking;
    public float superMeterCharge; // Ranges between 0 to maxSuperCharge
    public float maxSuperCharge = 100f;
    public bool isSuperActive;
    public bool blockHorizontalMovement;
    public bool moving;
    public bool movedWhileAttacking;
    public bool deathStarted;
    public bool listeningForDoubleDownTap;

    //Attack state blocker
    public bool blockAttackProgress;

    //Scripts
    private PlayerHorizontalMovement phm;
    private PlayerJump pj;
    private PlayerAttackManager pam;

    //Scripts to identify if in countdown or paused		
    public GameplayPause gameplayPause;
    public CountDown countDown;

    //Player Animation Variables
    [Header("Player Animation Variables")]
    public Animator playerUpperAnim;
    public Animator playerLowerAnim;
    public Animator shortRange;
    private PlayerAttackAnims paa;

    public Platform[] platforms;

    public AudioSource auso;

    #region SFX Variables
    [Header("General SFX")]
    public AudioClip[] JumpSounds;
    public AudioClip[] TakeDamageSounds;
    public AudioClip DeathSound;

    [Header("John - Drums")]
    public AudioClip[] JohnDoubleJump;
    public AudioClip[] JohnShortRange;
    public AudioClip[] JohnLongRange;
    public AudioClip JohnSuper;

    [Header("Shavo - Bass")]
    public AudioClip ShavoDash;
    public AudioClip[] ShavoShortRange;
    public AudioClip ShavoLongRange;
    public AudioClip ShavoSuper;

    [Header("Daron - Guitar")]
    public AudioClip DaronTeleport;
    public AudioClip DaronShortRange;
    public AudioClip[] DaronLongRangeThrow;     // may condense to just 1 sound effect
    public AudioClip DaronLongRangeHit;         // may condense to just 1 sound effect
    public AudioClip DaronSuper;

    [Header("Serj - Vocals")]
    public AudioClip SerjWingSprout;
    public AudioClip[] SerjWingFlap;
    public AudioClip SerjWingDecay;
    public AudioClip[] SerjShortRange;
    public AudioClip[] SerjLongRange;
    public AudioClip SerjSuperLightbeam;    // will condense to just 1 sound effect with 4 variations and no parts - waiting on final super animation
    public AudioClip[] SerjSuperVocal;      // will condense to just 1 sound effect with 4 variations and no parts - waiting on final super animation
    #endregion

    public string GetAnimName(string animSuffix)
    {
        return currentBandMember + animSuffix;
    }

    // Handles a common use case regarding playing body and leg animations.
    public void PlayAnims(string animSuffix)
    {
        if (!attacking)
        {
            playerUpperAnim.Play(GetAnimName(animSuffix));
        }
        playerLowerAnim.Play(GetAnimName(animSuffix + "Legs"));
    }

    void Start()
    {
        // Initialize player state
        health = startingHealth;
        moving = false;
        attacking = false;
        deathStarted = false;
        movedWhileAttacking = false;
        isSuperActive = false;
        inAir = false;
        crouched = false;
        listeningForDoubleDownTap = false;
        remainingJumps = 1;
        superMeterCharge = 0f;

        // Player-specific attack
        blockAttackProgress = true;

        // Get components from gameobjects
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerUpperAnim = gameObject.GetComponent<Animator>();
        shortRange = shortRangeHitbox.GetComponent<Animator>();
        auso = gameObject.GetComponent<AudioSource>();

        // Configure other player scripts
        paa = GetComponentInChildren<PlayerAttackAnims>();
        phm = GetComponent<PlayerHorizontalMovement>();
        pj = GetComponent<PlayerJump>();
        pam = GetComponentInChildren<PlayerAttackManager>();
        paa.ps = this;
        phm.ps = this;
        pj.ps = this;

        // Empty check on bandmember name
        if (currentBandMember == "")
        {
            currentBandMember = "John";
        }
    }

    public AudioClip GetRandomSoundEffect(AudioClip[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public bool FacingLeft()
    {
        return transform.rotation.y != 0f;
    }

    void Update()
    {
        //stops player from being able to move if in pause or countdown
        if (!countDown.getCountDown() && !gameplayPause.getPaused())
        {
            if (!Dead)
            {
                #region Friction
                float speedReductionThisFrame;
                float frictionMultiplier = 1f;
                if (isSuperActive) frictionMultiplier = 0.55f;
                if (inAir)
                    speedReductionThisFrame = Time.deltaTime * airFrictionDecel * frictionMultiplier;
                else
                    speedReductionThisFrame = Time.deltaTime * groundFrictionDecel * frictionMultiplier;
                if (Mathf.Abs(rb.velocity.x) > speedReductionThisFrame)
                {
                    rb.velocity += new Vector2(-1 * Mathf.Sign(rb.velocity.x) * speedReductionThisFrame, 0);
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }
                #endregion Friction

                #region Super meter charge
                // Passive meter charge, maybe vary by character
                superMeterCharge += maxSuperCharge / 100f * Time.deltaTime;
                if (superMeterCharge > maxSuperCharge) superMeterCharge = maxSuperCharge;
                // Debug.Log("meter charge " + superMeterCharge);
                #endregion Super meter charge

                #region Falling and jumping animations
                if (!blockHorizontalMovement && !isSuperActive)  // Or any other special condition is in effect
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
                if (Input.GetKey(KeyCode.DownArrow) && !inAir)
                { // This line used to have !attacking
                    if (!attacking)
                    { //(!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))) {
                        crouched = true;
                        PlayAnims("Crouch");
                        upperBodyHitbox.SetActive(false);
                    }
                    else
                    {
                        crouched = false;
                    }
                    if (!listeningForDoubleDownTap)
                    {
                        listeningForDoubleDownTap = true;
                        StartCoroutine(listenForDoubleDownTap());
                    }
                }
                else
                {
                    crouched = false;
                    upperBodyHitbox.SetActive(true);
                }
                #endregion Crouching

                pj.HandleJump();

                #region Attacks
                //Z: Short Range Attack    X: Long Range Attack    C: Super Attack
                if (Input.GetKeyDown(KeyCode.Z) && !crouched && !attacking)
                {
                    auso.Stop();
                    StartCoroutine(paa.shortRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackShort());
                }
                else if (Input.GetKey(KeyCode.X) && !attacking)
                {
                    auso.Stop();
                    StartCoroutine(paa.longRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackLong());
                }
                else if (Input.GetKeyDown(KeyCode.C) && !attacking &&
                        !isSuperActive && superMeterCharge >= maxSuperCharge)
                {
                    superMeterCharge = 0f;
                    auso.Stop();
                    StartCoroutine(paa.superAttackAnims());
                    StartCoroutine(pam.pa.AttackSuper());
                }
                #endregion Attacks

                phm.HandleHorizontalMovement();
            }
            else
            {
                //Death animation
                if (!deathStarted)
                {
                    StartCoroutine("Kill");
                    deathStarted = true;
                }
            }
        }
    }

    #region Collision Detection
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor" && !Dead)
        {
            playerLowerAnim.Play(GetAnimName("LandLegs"));
        }
    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor")
        {
            inAir = false;
            // landing = false;
            remainingJumps = 1;
        }
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor")
        {
            inAir = true;
            playerLowerAnim.SetTrigger("inAir");
        }
    }
    #endregion

    // Pass the call down to the movement script
    public void blockMovement(float duration)
    {
        StartCoroutine(phm.blockMovement(duration));
    }

    public IEnumerator Kill()
    {
        playerUpperAnim.Play(GetAnimName("Death"));
        playerLowerAnim.Play("ShavoDashLegs");
        auso.PlayOneShot(DeathSound);
        yield return new WaitForSeconds(1.0f);
    }

    public bool isCrouching()
    {
        return crouched;
    }

    private IEnumerator listenForDoubleDownTap()
    {
        yield return null;
        float timer = 0f;
        while (timer < 0.30f)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Toggle platform colliders off
                for (int i = 0; i < platforms.Length; ++i)
                {
                    if (platforms[i] != null)
                        platforms[i].setColliderEnabled(false);
                }

                float timer2 = 0f;
                while (timer2 < 0.18f)
                {
                    timer2 += Time.deltaTime;
                    yield return null;
                }

                // Toggle platform colliders on
                for (int i = 0; i < platforms.Length; ++i)
                {
                    if (platforms[i] != null)
                        platforms[i].setColliderEnabled(true);
                }
                listeningForDoubleDownTap = false;
                yield break;
            }
            yield return null;
        }
        listeningForDoubleDownTap = false;
    }
}