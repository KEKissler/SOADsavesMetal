using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Default Player Variables
    [Header("Player General Properties")]
    public string currentBandMember;
    public bool Dead { get { return health < 1; } }
    public int Health { get { return health; } set { health = value; } }
    public Rigidbody2D rb;
    public float jumpHeight;
    public int startingHealth;
    private int health;
    public float curInvulnerableTime;
    public float invulnerabilityDuration = 3f;
    public float maxGroundSpeed, groundAccel, groundDecel, groundFrictionDecel;
    public float maxAirSpeed, airAccel, airDecel, airFrictionDecel;

    private SpriteRenderer sr, srLegs;

    //Player Hitbox Variables
    [Header("Player Hitbox Variables")]
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;
    private BoxCollider2D lowerBodyHitbox;
    private Vector2 lowOriginalOffset, lowOriginalSize;

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
    public bool blockNormalJumpAnims;
    public bool moving;
    public bool movedWhileAttacking;
    public bool isDead = false;
    public bool deathStarted;
    public bool listeningForDoubleDownTap;
    public bool daronListeningForParry;
    public bool serjFlightActive;

    //Attack state blocker
    public bool blockAttackProgress;

    //Scripts
    private PlayerHorizontalMovement phm;
    private PlayerJump pj;
    private PlayerAttackManager pam;

    //Scripts to identify if in countdown or paused		
    public GameplayPause gameplayPause;
    public CountDown countDown;
    public DeathMenu deathMenu;

    //Player Animation Variables
    [Header("Player Animation Variables")]
    public Animator playerUpperAnim;
    public Animator playerLowerAnim;
    public Animator shortRange;
    private PlayerAttackAnims paa;
    private Slider superBar;

    public Platform[] platforms;

    [HideInInspector]
    public float desiredVelocity;
    private bool performFriction;

    #region FMODEvents
    [Header("General Events")]
    [FMODUnity.EventRef]
    public string playerJump;
    [FMODUnity.EventRef]
    public string playerHit;
    [FMODUnity.EventRef]
    public string playerDeath;

    [Header("John Events")]
    [FMODUnity.EventRef]
    public string johnJump;
    [FMODUnity.EventRef]
    public string johnShortRange;
    [FMODUnity.EventRef]
    public string johnLongRange;
    [FMODUnity.EventRef]
    public string johnSuper;

    [Header("Shavo Events")]
    [FMODUnity.EventRef]
    public string shavoDash;
    [FMODUnity.EventRef]
    public string shavoShortRange;
    [FMODUnity.EventRef]
    public string shavoLongRange;
    [FMODUnity.EventRef]
    public string shavoSuper;

    [Header("Daron Events")]
    [FMODUnity.EventRef]
    public string daronTeleport;
    [FMODUnity.EventRef]
    public string daronShortRange;
    [FMODUnity.EventRef]
    public string daronLongRange;
    [FMODUnity.EventRef]
    public string daronSuper;

    [Header("Serj Events")]
    [FMODUnity.EventRef]
    public string serjShortRange;
    [FMODUnity.EventRef]
    public string serjLongRange;
    [FMODUnity.EventRef]
    public string serjFlyStart;
    [FMODUnity.EventRef]
    public string serjFlyMid;
    [FMODUnity.EventRef]
    public string serjFlyEnd;
    [FMODUnity.EventRef]
    public string serjSuperStart;
    [FMODUnity.EventRef]
    public string serjSuperEnd;

    #endregion

    public string GetAnimName(string animSuffix)
    {
        return currentBandMember + animSuffix;
    }

    public void SetCurrentBandMember(string newBandMember)
    {
        currentBandMember = newBandMember;
        StartCoroutine(pam.SetPlayerName());
    }

    public void PlayAudioEvent(string fmodEvent)
    {
        EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
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

    private void Awake()
    {
        desiredVelocity = 0;
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
        superBar = GameObject.Find("Super Bar").GetComponent<Slider>();
        superBar.maxValue = maxSuperCharge;
        blockNormalJumpAnims = false;
        daronListeningForParry = false;
        serjFlightActive = false;
        performFriction = false;

        sr = GetComponent<SpriteRenderer>();
        srLegs = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        // Player-specific attack
        blockAttackProgress = true;

        // Get components from gameobjects
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerUpperAnim = gameObject.GetComponent<Animator>();
        shortRange = shortRangeHitbox.GetComponent<Animator>();
        lowerBodyHitbox = gameObject.GetComponent<BoxCollider2D>();
        lowOriginalOffset = lowerBodyHitbox.offset;
        lowOriginalSize = lowerBodyHitbox.size;

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

    void Update() {
        //Debug.Log("crouched " + crouched);
        //Debug.Log("inair " + inAir);
        //stops player from being able to move if in pause or countdown

        if ((countDown == null || !countDown.getCountDown()) && (gameplayPause == null || !gameplayPause.getPaused()))
        {
            if (!Dead)
            {
                performFriction = true;

                #region Super meter charge
                // Uncomment the following line for instant meter recharge
                superMeterCharge += maxSuperCharge;
                // Passive meter charge, maybe vary by character
                superMeterCharge += maxSuperCharge / 100f * Time.deltaTime;
                if (superMeterCharge > maxSuperCharge) superMeterCharge = maxSuperCharge;
                // Debug.Log("meter charge " + superMeterCharge);
                superBar.value = superMeterCharge;
                #endregion Super meter charge

                #region Falling and jumping animations
                if (!blockHorizontalMovement && !blockNormalJumpAnims)  // Or any other special condition is in effect
                {
                    if (rb.velocity.y < -0.5f)
                    {
                        // landing = true;
                        if (!isSuperActive)
                            PlayAnims("Fall");
                        lowerBodyHitbox.offset = lowOriginalOffset;
                        lowerBodyHitbox.size = lowOriginalSize;
                    }
                    if (rb.velocity.y > 0.5)
                    {
                        if (!isSuperActive)
                            PlayAnims("Jump");
                        lowerBodyHitbox.offset = new Vector2(lowOriginalOffset.x, -0.05f);
                        lowerBodyHitbox.size = new Vector2(lowOriginalSize.x, 0.35f);
                    }
                }
                #endregion Falling and jumping animations

                #region Crouching
                if (Input.GetKey(KeyCode.DownArrow) && !inAir)
                { // This line used to have !attacking
                    crouched = true;
                    PlayAnims("Crouch");
                    upperBodyHitbox.SetActive(false);
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
                if (Input.GetKeyDown(KeyCode.Z) && !attacking)
                {
                    StartCoroutine(paa.shortRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackShort());
                }
                else if (Input.GetKey(KeyCode.X) && !attacking)
                {
                    StartCoroutine(paa.longRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackLong());
                }
                else if (Input.GetKeyDown(KeyCode.C) && !attacking &&
                        !isSuperActive && superMeterCharge >= maxSuperCharge)
                {
                    superMeterCharge = 0f;
                    StartCoroutine(paa.superAttackAnims());
                    StartCoroutine(pam.pa.AttackSuper());
                }
                #endregion Attacks

                phm.HandleHorizontalMovement();

                #region Serj flight vertical movement
                if (serjFlightActive)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, maxAirSpeed * 0.85f);
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -maxAirSpeed * 0.85f);
                    }
                }
                #endregion

                #region Invulnerability Timer Tick
                if (curInvulnerableTime > 0f)
                {
                    curInvulnerableTime -= Time.deltaTime;
                    if (curInvulnerableTime < 0f) curInvulnerableTime = 0f;
                }
                #endregion
            }
            else
            {
                //Death animation
                if (!deathStarted)
                {
                    isDead = true;
                    StartCoroutine("Kill");
                    deathStarted = true;
                }
            }
        }
        else
        {
            PlayAnims("Idle");
        }
    }

    #region Friction
    private void FixedUpdate()
    {
        if(performFriction)
        {
            float speedReductionThisFrame;
            float frictionMultiplier = 1f;
            if (isSuperActive) frictionMultiplier = 0.55f;
            else if (crouched) frictionMultiplier = 0.33f;
            if (inAir)
                speedReductionThisFrame = Time.deltaTime * airFrictionDecel * frictionMultiplier;
            else
                speedReductionThisFrame = Time.deltaTime * groundFrictionDecel * frictionMultiplier;
            if (Mathf.Abs(rb.velocity.x - desiredVelocity) > speedReductionThisFrame)
            {
                rb.velocity += new Vector2(-1 * Mathf.Sign(rb.velocity.x - desiredVelocity) * speedReductionThisFrame, 0);
            }
            else
            {
                rb.velocity = new Vector3(desiredVelocity, rb.velocity.y, 0);
            }
            
            performFriction = false;
        }
    }
    #endregion Friction

    #region Collision Detection
    public void OnCollisionEnter2D(Collision2D coll)
    {
        // Debug.Log("ground touched");
        if (coll.collider.tag == "Floor" && !Dead)
        {
            playerLowerAnim.Play(GetAnimName("LandLegs"));
            inAir = false;
            remainingJumps = 1;
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
        PlayAudioEvent(playerDeath);
        yield return new WaitForSeconds(1.0f);
        SaveSystem.SaveGame();
        yield return new WaitForSeconds(2.0f);
        deathMenu.deathMenuPop();
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

    public void DamagePlayer()
    {
        if (daronListeningForParry)
        {
            daronListeningForParry = false;
            remainingJumps += 1;
        }

        if (curInvulnerableTime <= 0f)
        {
            curInvulnerableTime = invulnerabilityDuration;
            if (!Dead) StartCoroutine(PlayerFlashOnDamage(invulnerabilityDuration));
        }
    }

    IEnumerator PlayerFlashOnDamage(float duration)
    {
        Health -= 1;
        if (Health != 0)
        {
            if (currentBandMember == "Daron")
            {
                float forgiveTime = 0.09f, t2 = 0f;
                while (t2 < forgiveTime)
                {
                    t2 += Time.deltaTime;
                    if (daronListeningForParry)
                    {
                        curInvulnerableTime += forgiveTime;
                        daronListeningForParry = false;
                        yield break;
                    }
                    yield return null;
                }
            }
            PlayAudioEvent(playerHit);

            float initialPeriod = 0.25f, finalPeriod = 0.05f;
            float curPeriod;
            float timer = 0f, tick = 0f;
            sr.enabled = false;
            srLegs.enabled = false;
            while (timer < duration)
            {
                curPeriod = Mathf.Lerp(initialPeriod, finalPeriod, timer / duration);
                timer += Time.deltaTime;
                tick += Time.deltaTime;
                if (tick > curPeriod / 2)
                {
                    sr.enabled = !sr.enabled;
                    srLegs.enabled = sr.enabled;
                    tick = 0f;
                }
                yield return null;
            }
            sr.enabled = true;
            srLegs.enabled = true;
        }
    }
}